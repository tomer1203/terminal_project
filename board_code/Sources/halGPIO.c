/*
 * halGPIO.c
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#include "TFC.h"
void ftoa(float n, char* res, int afterpoint);

//-----------------------------------------------------------------
//  PORTD - ISR = Interrupt Service Routine
//-----------------------------------------------------------------
void PORTD_IRQHandler(void){
	volatile unsigned int i;
	// check that the interrupt was for switch
	if (PORTD_ISFR & PTD_7) {
		if(!(GPIOD_PDIR & PTD_7)){
			RED_LED_TOGGLE;
			scroll_down();
		}
	}
	if(PORTD_ISFR & PTD_6){
		if(!(GPIOD_PDIR & PTD_6)){
			enter();
			BLUE_LED_TOGGLE;
		}
	}
	//Debounce or using PFE field
	while(!(GPIOD_PDIR & PTD_7) );// wait of release the button
	while(!(GPIOD_PDIR & PTD_6) );// wait of release the button
	for(i=10000 ; i>0 ; i--); //delay, button debounce
	
	print_ui();
	DelayMs(1000);
	
	PORTD_ISFR |= PTD_7;  // clear interrupt flag bit of PTD7
	PORTD_ISFR |= PTD_6;  // clear interrupt flag bit of PTD6
}


// format:
// "$[Br]9600\0"
// $ - command symbol
// Br -Baud rate configuration

//-----------------------------------------------------------------
//  DMA - ISR
//-----------------------------------------------------------------

void DMA0_IRQHandler(void)
{
	//disable_irq(INT_DMA0 - 16);
	DMA_DSR_BCR0 |= DMA_DSR_BCR_DONE_MASK;			// Clear Done Flag
	DMAMUX0_CHCFG0 &= ~DMAMUX_CHCFG_ENBL_MASK;	    // Disable DMA Channel 0
	UART0_C5 &= ~UART0_C5_RDMAE_MASK; 				// Disabling DMA using UART
	enable_irq(INT_UART0-16);						// Enable UART0 interrupt
	RED_LED_ON;
	int j;
	handleMessage();
	for (j=1000000; j>0; j--);	                    // Delay
	RED_LED_OFF;                   // Delay
}

//-----------------------------------------------------------------
//  UART0 - ISR
//-----------------------------------------------------------------
void UART0_IRQHandler(){
	uint8_t Char;
	char length[4];
	
	
	if(UART0_S1 & UART_S1_RDRF_MASK){ // RX buffer is full and ready for reading
		Char = UART0_D;
		
		// insert read char to buffer
		string_buffer[string_index] = Char;
		
		// WHEN NOT USING THE DMA THIS LINE SHOULD BE BROUHGT BACK
		//input_string_length--;

		// read the input string length
		if (string_index == 10){
			length[0] = string_buffer[7];
			length[1] = string_buffer[8];
			length[2] = string_buffer[9];
			length[3] = '\0';
			input_string_length = atoi(length); 
			// DMA CONFIGURATION!
			//DMA_DAR0 = (uint32_t)hd_file_Ptr[index];       			//destination
			DMA_DSR_BCR0 = DMA_DSR_BCR_BCR(input_string_length);       // number of bytes to transfer
			DMAMUX0_CHCFG0 |= DMAMUX_CHCFG_ENBL_MASK; 				// Enable DMA channel 
			disable_irq(INT_UART0-16);               			    // Disable UART0 interrupt
			UART0_C5 |= UART0_C5_RDMAE_MASK;          				// Enable DMA request for UART0 receiver
		}
		
		// THIS MIGHT NEED TO BE DELETED
		// if message is finished		
		if (input_string_length<=0 && string_index >= 10){
			handleMessage();
			return;
		}
		string_index++;
	}
	
}// END UART_IRQ

//////////////////////////////
//    Handle Message
//////////////////////////////
void handleMessage(){
	
	int function_return_value;
	char text[2];
	
	// CHECKSUM Check //
	if (!validate_checksum(string_buffer, input_string_length + 11)) {// string index +1
		send2pc(TYPE.STATUS, STATUS.CHECKSUM_ERROR);
		clear_string_buffer();
		return;
	}
	else {
		//send2pc(TYPE.STATUS, STATUS.OK);
	}
	
	switch (state) {
	
	case WRITING_FILE_INIT_E:
		function_return_value = write_file_init_message(string_buffer);
		if (function_return_value == 1) {
			Print("Receiving a File");
			state = WRITING_FILE;
		}
		if (function_return_value<0){
			sprintf(text,"%d",function_return_value);
			send2pc(TYPE.FILE_END, abs(function_return_value));
		}
		break;
		
	case WRITING_FILE:
		function_return_value = write_file_chunck(string_buffer, input_string_length); // string_index - 10
		if (function_return_value == 1) {
			// File written successfully 
			send2pc(TYPE.FILE_END, STATUS.OK);
			Print_two_lines("Received", "Successfully");
			state = IDLE_E;
			initialize_ui();
		}
		if (function_return_value<0){
			sprintf(text,"%d",function_return_value);
			send2pc(TYPE.FILE_END, abs(function_return_value));
		}
		break;
		
	default:
		// ACTIONS //
		
		// print message to chat
		if (is_chat_command(string_buffer)) {
			Print(strip_command(string_buffer));
		}
		// receiving a file
		else if (is_write_file_transfer_command(string_buffer)) {
			state = WRITING_FILE_INIT_E;
		}
		// change Baud rate
		else if (is_br_command(string_buffer)) {
			changeBaudrate();
		}
		break;
	}
	
	
	// when finished reading message, clean the buffer.
	clear_string_buffer();
	
} // Handle Message


///////////////////////////////////////
// Changes baudrate & sends STATUS OK
///////////////////////////////////////
void changeBaudrate(){
	char baudRate[6];
	
	baud_config = atoi(strip_command(string_buffer));
	send2pc(TYPE.BAUDRATE, strip_command(string_buffer));
	DelayMs(500);
	change_Baud_config(baud_config);
	DelayMs(500);
	send2pc(TYPE.STATUS, STATUS.OK);
	Print_two_lines("Baud Rate:", strip_command(string_buffer));
	sprintf(baudRate, "%5d", baud_config);
	main_menu[3][1][7] = baudRate[0];
	main_menu[3][1][8] = baudRate[1];
	main_menu[3][1][9] = baudRate[2];
	main_menu[3][1][10] = baudRate[3];
	main_menu[3][1][11] = baudRate[4];
}


/*
 * Print to LCD
 */
void Print(const char * s){
	
	cursor_off;
	lcd_clear();
	lcd_goto(0);
	DelayMs(10);
	
	lcd_puts(s);
	
}
void Print_two_lines(const char *s1,const char *s2){
	lcd_clear();
	lcd_goto(1);
	DelayMs(10);
	lcd_puts(s1);
	lcd_new_line;
	lcd_puts(s2);
}

//******************************************************************
// Delay usec functions
//******************************************************************
void DelayUs(unsigned int cnt){
  
	unsigned int i;
        for(i=cnt ; i>0 ; i--)
        		asm("nop"); // tha command asm("nop") takes raphly 1usec
        
	
}
//******************************************************************
// Delay msec functions
//******************************************************************
void DelayMs(unsigned int cnt){
  
	unsigned int i;
        for(i=cnt ; i>0 ; i--)
        	DelayUs(1000); // tha command asm("nop") takes raphly 1usec
        
}
//******************************************************************
