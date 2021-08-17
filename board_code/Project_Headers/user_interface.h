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
#define FILE_MENU_SIZE 3
#define CONFIGURATION_MENU_SIZE 2

int menu_select;
int line_select;
int menu_size;

char last_read_line[16];
char current_read_line[16];
extern const char back[16];

File_descriptor *last_file_descriptor;
File_descriptor *current_file_desc;

void print_ui();
void scroll_down();
StateModes enter();

extern const char chat_lines[5][20];
extern char main_menu[4][6][20];

#endif /* USER_INTERFACE_H_ */
