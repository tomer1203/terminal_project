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
		
		//UARTprintf(UART0_BASE_PTR,"Echo: Press a key followed with Enter!\r\n");
		char menu_s[160] = 
"Menu\r\n\
1. Blink RGB LED, color by color with delay of X[ms]\r\n\
2. Get delay time X[ms]:\r\n\
3. Potentiometer 3-digit value [v]\r\n\
4. Clear all LEDs\r\n\
5. Sleep\r\n";
        
		//UARTprintf(UART0_BASE_PTR,menu_s);
		
		RED_LED_TOGGLE;
		scroll_down();
	}
	if(PORTD_ISFR & PTD_6){
//		char I_love_my_negev[20] = "I love my Negev\r\n";
//		if (state == PRINT_E){
//            // Print Menu
//            UARTprintf(UART0_BASE_PTR,I_love_my_negev);
//        } 
		state = enter();
		BLUE_LED_TOGGLE;
	}
	//Debounce or using PFE field
	while(!(GPIOD_PDIR & PTD_7) );// wait of release the button
	while(!(GPIOD_PDIR & PTD_6) );// wait of release the button
	for(i=10000 ; i>0 ; i--); //delay, button debounce
	
	print_ui();
	DelayMs(10);
	PORTD_ISFR |= 0x00000080;  // clear interrupt flag bit of PTD7
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
	
	if(UART0_S1 & UART_S1_RDRF_MASK){ // RX buffer is full and ready for reading
		Char = UART0_D;
		
		string_buffer[string_index] = Char;
		
		// if message is finished		
		if (Char == '\0' && string_index >= 6){
			
			// CHECKSUM Check //
			if (!checksum(string_buffer)){
				UARTprintf(UART0_BASE_PTR,"$[St]3"STATUS_CHECKSUM_ERROR);
				clear_string_buffer();
				return;
			} else {
				UARTprintf(UART0_BASE_PTR,"$[St]2"STATUS_OK);
			}
			
			// ACTIONS //
			// change Baud rate
			if (is_br_command(string_buffer)){
				int baud_config = atoi(strip_command(string_buffer));
				Print_two_lines("Baud Rate:", &string_buffer[5]);
				change_Baud_config(baud_config);
				UARTprintf(UART0_BASE_PTR,"$[Tx]Gchanged baud rate, status ok\r\n");
			
			// normal chat
			} else if (is_chat_command(string_buffer)){ 
				Print(strip_command(string_buffer)); 
			}
			
			// when finished reading message, clean the buffer.
			clear_string_buffer();
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
