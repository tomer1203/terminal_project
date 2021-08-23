/*
 * halGPIO.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef HALGPIO_H_
#define HALGPIO_H_
// 1/2 KB
#define MAX_STRING 140
#define PACKET_SIZE 64

extern char string_buffer[MAX_STRING];
int string_index;
int input_string_length;
int msg_length;
int baud_config;

void Print(const char * s);
void Print_two_lines(const char *s1,const char *s2);
int translate_config_command(char* string);
void DelayUs(unsigned int cnt);
void DelayMs(unsigned int cnt);


#endif /* HALGPIO_H_ */
