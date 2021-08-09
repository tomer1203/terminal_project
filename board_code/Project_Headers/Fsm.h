/*
 * Fsm.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef FSM_H_
#define FSM_H_

#include <stdint.h>

#define NL 218


int interval;

typedef enum StateModes{IDLE_E,CHAT_E,FILE_TRANSFER_E,CONFIGURATION_E} StateModes;
volatile StateModes state;

StateModes lastState;

void Fsm(void);
void menu_control(uint8_t digit);





#endif /* FSM_H_ */
