/*
 * user_interface.c
 *
 *  Created on: Aug 9, 2021
 *      Author: tomer
 */
# include "TFC.h"
const char back[16] = "<-Back";
const char chat_lines[5][20] =  {
		"hello\r\n",
		"world!\r\n",
		"pickachuuuuuu\r\n",
		"sample text\r\n",
		"more text\r\n"};
char main_menu[4][6][20] = {
	{// 0-main menu
	 "-chat mode",
	 "-transfer mode",
	 "-config mode"},
	{// 1-chat menu
	 "<-Back",
	 "hello",
	 "world!",
	 "pickachuuuuuu",
	 "sample text",
	 "more text"},
	{// 2-file transfer menu
	 "<-Back",
	 "Read File",
	 "Send File"},
	{// 3-Configuration menu
	 "<-Back",
	 "Brate:      "} };
void chat_action();
void read_action();

void copy_16chars(char* to, char* from) {
	int i = 0;
	for (i = 0;i < 16; i++) {
		to[i] = from[i];
	}
}
const char* getChatLine(int line){
	
	return chat_lines[line-1];
}
void initialize_ui(){
		 
	menu_select = 0;
	line_select = 0;
	menu_size   = MAIN_MENU_SIZE;
}
void print_ui(){
	switch(state){
	
	case DISPLAY_FILE_E:     // view contents of a file
		Print_two_lines(last_read_line, current_read_line);
		break;
		
	case READ_FILE_E:// view file list
		if (line_select == 0) {
			Print_two_lines(back, current_file_desc->name);
		}
		else if (line_select == menu_size-1) {
			Print_two_lines(last_file_descriptor->name, back);
		}
		else
			Print_two_lines(last_file_descriptor->name,current_file_desc->name);
		break;
		
	default: 
		Print_two_lines(main_menu[menu_select][line_select],
			main_menu[menu_select][get_next_line(line_select)]);
		break;
	}
	
}
void scroll_down(){
	switch(state){
	
	case DISPLAY_FILE_E: 
		read_line();
		copy_16chars(last_read_line, current_read_line);
		copy_16chars(current_read_line, reading_Line);
		break;
		
	case READ_FILE_E: 
		if (line_select != menu_size-1) {
			file_select = file_index_plusplus(file_select); // -1 is for back
			last_file_descriptor = current_file_desc;
			current_file_desc = file_info(file_select);
		}
		line_select = get_next_line(line_select);
		break;
		
	default:
		line_select = get_next_line(line_select);
		break;
	}
}
StateModes enter(){
	StateModes next_state = IDLE_E;

	int switched_menu = 0;
	// choose next state
	switch(state){
	
	// main menu
	case IDLE_E:
		switch(line_select){
		case 0:
			next_state = CHAT_E;
			switched_menu = 1;
			break;
		case 1:
			next_state = FILE_TRANSFER_E;
			switched_menu = 1;
			break;
		case 2:
			next_state = CONFIGURATION_E;
			switched_menu = 1;
			break;
		default:
			next_state = IDLE_E;
			switched_menu = 1;
		}
		break;
	
	// choose file transfer option(read/send)
	case FILE_TRANSFER_E:
		switch (line_select) {
		case 0:
			next_state = IDLE_E;
			switched_menu = 1;
			break;
		case 1:
			next_state = READ_FILE_E;
			file_select = file_system.first_file;
			current_file_desc = file_info(file_select);
			switched_menu = 1;
			break;
		case 2:
			next_state = SEND_FILE_E;
			switched_menu = 1;
			break;
		default:
			next_state = IDLE_E;
			switched_menu = 1;
			break;
		}
		break;
	
	// choose which file to read
	case READ_FILE_E:
		if (line_select == 0){
			next_state = FILE_TRANSFER_E;
			switched_menu = 1;
		} else {
			read_action();
			next_state = DISPLAY_FILE_E;
			switched_menu = 1;
		}
		break;
	
	// look at the pretty text
	case DISPLAY_FILE_E:
		next_state = READ_FILE_E;
		switched_menu = 1;
		break;
		
	// all other states which don't need special attention
	default: 
		if (line_select == 0){
			next_state = IDLE_E;
			switched_menu = 1;
		} else {
			next_state = state;
			
			// ACTIONS //
			if (state == CHAT_E){
				chat_action();
			}
			
			if (state == SEND_FILE_E){
				// TODO: SEND FILE 2 PC
				state = IDLE_E;
				switched_menu = 1;
			}
		}
	}
	
	// update menu selection
	switch(next_state){
	case(IDLE_E):
		menu_select=0;
		menu_size = MAIN_MENU_SIZE;
		break;
	case(CHAT_E):
		menu_select=1;
		menu_size = CHAT_MENU_SIZE;
		break;
	case(FILE_TRANSFER_E):
		menu_select=2;
		menu_size = FILE_MENU_SIZE;
		break;
	case(READ_FILE_E):
		menu_select = 4;
		menu_size = file_system.number_of_files+1;// +1 is for back
		break;
	case(CONFIGURATION_E):
		menu_select=3;
		menu_size = CONFIGURATION_MENU_SIZE;
		break;
	}
	
	// when switching to new menu, reset line number
	if (switched_menu == 1){
		line_select = 0;
	}
	state = next_state;
	return next_state;
	
} // END enter
void chat_action(){
	char Length[3];
	char Length_final[3];
	char* line = getChatLine(line_select);
	sprintf(Length,"%d",strlen(line));
	if (strlen(line) < 10){
		strcpy(Length_final, "00");
		strcat(Length_final,Length);
	} else if (strlen(line) < 100){
		strcpy(Length_final, "0");
		strcat(Length_final,Length);
	}
	send2pc("Tx",Length_final,line);
}

void read_action(){
	read_file_init(line_select - 1);
	read_line();
	copy_16chars(last_read_line, reading_Line);
	read_line();
	copy_16chars(current_read_line, reading_Line);
}

int get_next_line(int line){
	if (line >= menu_size-1){
		return 0;
	}
	return line+1;
}

