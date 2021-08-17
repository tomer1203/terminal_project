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
			state = enter();
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

//-----------------------------------------------------------------
// PIT - ISR = Interrupt Service Routine
//-----------------------------------------------------------------
void PIT_IRQHandler(){
	
	if(PIT_TFLG0 & PIT_TFLG_TIF_MASK){
		change_color();
		PIT_TFLG0 = PIT_TFLG_TIF_MASK; //clear the Pit 0 Irq flag 
	}
	
	if(PIT_TFLG1 & PIT_TFLG_TIF_MASK){
		enableADC0();
		PIT_TFLG1 = PIT_TFLG_TIF_MASK; //clear the Pit 1 Irq flag
	}
}


// format:
// "$[Br]9600\0"
// $ - command symbol
// Br -Baud rate configuration

//-----------------------------------------------------------------
//  UART0 - ISR
//-----------------------------------------------------------------
void UART0_IRQHandler(){
	uint8_t Char;
	char length[4];
	char baudRate[6];
	int function_return_value;
	if(UART0_S1 & UART_S1_RDRF_MASK){ // RX buffer is full and ready for reading
		Char = UART0_D;
		
		string_buffer[string_index] = Char;
		input_string_length--;
//		Print(string_buffer);
		// read the input string length
		if (string_index == 10){
//			strcpy_s(length,3,&string_buffer[7]);
			length[0] = string_buffer[7];
			length[1] = string_buffer[8];
			length[2] = string_buffer[9];
			length[3] = '\0';
			input_string_length = atoi(length); // the +1 is for the closing |
		}
		
		// if message is finished		
		if (input_string_length<=0 && string_index >= 11){
			// CHECKSUM Check //
			if (!validate_checksum(string_buffer, string_index + 1)) {
				send2pc("St", "001", STATUS_CHECKSUM_ERROR);
				clear_string_buffer();
				return;
			}
			else {
				send2pc("St", "001", STATUS_OK);
			}
			if (state == WRITING_FILE_INIT_E) {
				function_return_value = write_file_init_message(string_buffer)
					if (function_return_value == 1) {
						state = WRITING_FILE;
					}
			} else if (state == WRITING_FILE) {
				function_return_value = write_file_chunck(string_buffer, string_index);
				if (function_return_value == 1) {
					send2pc("Fe", "001", STATUS_OK);
				}
			} else {
				// ACTIONS //
				// change Baud rate
				if (is_chat_command(string_buffer)) {
					Print(strip_command(string_buffer));
				}
				else if (is_write_file_transfer_command(string_buffer)) {
					state = WRITING_FILE_INIT_E;
				}
				else if (is_br_command(string_buffer)) {
					baud_config = atoi(strip_command(string_buffer));
					sprintf(baudRate, "%5d", baud_config);
					main_menu[3][1][7] = baudRate[0];
					main_menu[3][1][8] = baudRate[1];
					main_menu[3][1][9] = baudRate[2];
					main_menu[3][1][10] = baudRate[3];
					main_menu[3][1][11] = baudRate[4];
					Print_two_lines("Baud Rate:", strip_command(string_buffer));
					
					send2pc("Tx", "028", "changed baud rate, status ok");
					change_Baud_config(baud_config);

					// normal chat
				}
				else if (is_chat_command(string_buffer)) {
					Print(strip_command(string_buffer));
				}
			}
			
			
			
			
			
			
			// when finished reading message, clean the buffer.
			clear_string_buffer();
			return;
		}
		string_index++;
	}
}

//-----------------------------------------------------------------
// ADC0 - ISR = Interrupt Service Routine
//-----------------------------------------------------------------
void ADC0_IRQHandler(){
	int intData;
	float data;
	char data_string[10];
	
	intData = ADC0_RA;
	data = (float)intData; // 4000
	
	data = (data*3.3)/(4095.0);
	ftoa(data, data_string, 3);
	//	uint8_t data_LSB = 0xFF & data;
	//uint8_t data_MSB = (0x0F00 & data) >> 8;
	
	//itoa( data ,data_string ,10);
	
	PrintVolt(data_string);
	//Print(data_string);
	
}


//-----------------------------------------------------------------
//  OUR RGB Serial Show function
//-----------------------------------------------------------------
void change_color(){
	
	static uint8_t counter= 0;
		
	if(counter & 0x01) BLUE_LED_ON;
	else BLUE_LED_OFF;
	
	if(counter & 0x02)RED_LED_ON;
	else RED_LED_OFF;
	
	if(counter & 0x04)GREEN_LED_ON;
	else GREEN_LED_OFF;
	
	counter++;

}


/*
 * Inits Timers
 */
void InitTimers(){
	InitPIT();
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

void PrintVolt(const char * s){
	
	cursor_off;
	lcd_clear();
	lcd_goto(0);
	lcd_puts("  Volt: ");
	DelayMs(5);
	
	lcd_puts(s);
	
	lcd_puts(" [V]");
}

//******************************************************************
// Delay usec functions
//******************************************************************
void DelayUs(unsigned int cnt){
  
	unsigned char i;
        for(i=cnt ; i>0 ; i--)
        		asm("nop"); // tha command asm("nop") takes raphly 1usec
        
	
}
//******************************************************************
// Delay msec functions
//******************************************************************
void DelayMs(unsigned int cnt){
  
	unsigned char i;
        for(i=cnt ; i>0 ; i--)
        	DelayUs(1000); // tha command asm("nop") takes raphly 1usec
        
}
//******************************************************************
