/*
 * Fsm.c
 *
 *  Created on: May 5, 2021
 *      Author: им
 */
#include "Fsm.h"
#include "TFC.h"

void menu_control(uint8_t digit){
	
//	if(state == GET_DELAY_E)
//	{
//		if(interval == 0 && digit == NL) return;
//		
//		
//		if(digit == NL){
//			setPITInterval(PIT_CLK_1000 * interval - 1); // setup PIT for 1msec counting period	
//			interval = 0;
//			state = lastState;
//			menu_control(lastState);
//			
//		} else {
//			interval = interval*10 + digit;
//		}
//	}
//	
//	else// Change States 
//	{
//		if (state == POTEN_E && 0 <= digit && digit <= 10)
//			disablePITx(1);// disable ADC
//		
//		
//		if(digit == 0){
//			state = IDLE_E;
//			disablePITx(0);
//			Print(" IDLE_E");
//		}
//		else if (digit == 1){ // blink
//			state = BLINK_E;
//			enablePITx(0);
//			Print(" BLINK_E");
//		}
//		else if (digit == 2){ // get delay
//			lastState = state;
//			state = GET_DELAY_E;
//			Print(" GET_DELAY_E");
//		}
//		else if(digit == 3){ // read poteniometer
//			state = POTEN_E;
//			enableADC0();
//			enablePITx(1);
//			disablePITx(0);
//			
//		}
//        else if(digit == 4){ // clear all leds
//			state = PRINT_E;
//			disablePITx(0);// disable ADC
//			Print(" PRINT_E");
//		}
//		else if(digit == 5){ // clear all leds
//			state = IDLE_E;
//			disablePITx(0);// disable ADC
//			Print(" IDLE_E");
//		}
//		else if(digit == 6){ // sleep
//			state = SLEEP_E;
//			disablePITx(0);// disable ADC
//			Print(" SLEEP_E");
//		}
//	}
		
}

void Fsm(void){
	state = IDLE_E;
	lastState = state;
	interval = 0;
	
	while(1){
		
		switch (state){
		
		case IDLE_E:
			disablePITx(0);
			RGB_LED_OFF;
			wait(); 
			break;
		
		case GET_DELAY_E:
			wait(); 
			break;
			
		case BLINK_E:
			wait(); 
			break;
			
		case POTEN_E:
			RGB_LED_OFF;
			wait();
			break;
			
		case SLEEP_E:
			disablePITx(0);
			RGB_LED_OFF;
			stop(); // sleep
			break;
		
		}
	}
	
}

