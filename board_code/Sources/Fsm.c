/*
 * Fsm.c
 *
 *  Created on: May 5, 2021
 *      Author: им
 */
#include "Fsm.h"
#include "TFC.h"
void change_state(StateModes next_state){
	state = next_state;
}
void menu_control(uint8_t digit){
		
}

void Fsm(void){
	state = IDLE_E;
	lastState = state;
	interval = 0;
	
	while(1){
		
		switch (state){
		
		case IDLE_E:
			disablePITx(0);
			//RGB_LED_OFF;
			wait(); 
			break;
		
		}
	}
	
}

