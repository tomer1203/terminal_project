/*
 * user_interface.h
 *
 *  Created on: Aug 9, 2021
 *      Author: tomer
 */

#ifndef USER_INTERFACE_H_
#define USER_INTERFACE_H_

#define MAIN_MENU_SIZE 3
#define CHAT_MENU_SIZE 6
#define FILE_MENU_SIZE 2
#define CONFIGURATION_MENU_SIZE 2

int menu_select;
int line_select;
int menu_size;

void print_ui();
void scroll_down();
StateModes enter();

#endif /* USER_INTERFACE_H_ */
