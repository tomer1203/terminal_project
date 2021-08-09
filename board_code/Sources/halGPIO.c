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
        
		UARTprintf(UART0_BASE_PTR,menu_s);
	}
	if(PORTD_ISFR & PTD_6){
		char I_love_my_negev[20] = "I love my Negev\r\n";
		if (state == PRINT_E){
            // Print Menu
            UARTprintf(UART0_BASE_PTR,I_love_my_negev);
        } 
	}
	//Debounce or using PFE field
	while(!(GPIOD_PDIR & PTD_7) );// wait of release the button
	while(!(GPIOD_PDIR & PTD_6) );// wait of release the button
	for(i=10000 ; i>0 ; i--); //delay, button debounce
	
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

void clear_string_buffer(){
	int i = 0;
	for (i = 0;i < MAX_STRING;i++){
		string_buffer[i] = 1;
	}
	string_index = 0;
}

//-----------------------------------------------------------------
//  UART0 - ISR
//-----------------------------------------------------------------
void UART0_IRQHandler(){
	
	uint8_t Temp;
		
	if(UART0_S1 & UART_S1_RDRF_MASK){ // RX buffer is full and ready for reading
		if (UART0_D == '\n'){
			// send input string
			Print(string_buffer); // TODO: multiple options should be available
			// then reset it.
			clear_string_buffer();
		}
		// building the string buffer
		string_buffer[string_index] = UART0_D;
		
		
		
		Temp = UART0_D ;
		Temp -= '0';
		
		menu_control(Temp);
		
		
	}
	if(UART0_S1 & UART_S1_TDRE_MASK){ // TX buffer is empty and ready for sending
		
		UART0_D = Temp+'0';
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
	DelayMs(5);
	
	lcd_puts(s);
	
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
