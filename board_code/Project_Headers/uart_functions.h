/*
 * uart_functions.h
 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */

#ifndef UART_FUNCTIONS_H_
#define UART_FUNCTIONS_H_

#define STATUS_OK             "0\r\n"
#define STATUS_CHECKSUM_ERROR "1\r\n"

void clear_string_buffer();
int is_chat_command(char* string);
int is_br_command(char* string);
int is_size_command(char* string);
int is_name_command(char* string);
char* strip_command(char* string);
void change_Baud_config(int Baud_rate);

int checksum(char * string);



#endif /* UART_FUNCTIONS_H_ */
