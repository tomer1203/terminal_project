/*
 * uart_functions.h
 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */

#ifndef UART_FUNCTIONS_H_
#define UART_FUNCTIONS_H_

#define CORE_CLOCK            48000000
#define STATUS_OK             "0"
#define STATUS_CHECKSUM_ERROR "1"

void clear_string_buffer();
int is_chat_command(char* string);
int is_write_file_transfer_command(char* string);
int is_br_command(char* string);
int is_size_command(char* string);
int is_name_command(char* string);
char* strip_command(char* string);
void change_Baud_config(int Baud_rate);

int validate_checksum(char* string, int len);
char calc_checksum(char * string,int len);
void send2pc(char* code,char* len,const char* message);


#endif /* UART_FUNCTIONS_H_ */
