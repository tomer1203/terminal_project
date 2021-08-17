/*
 * halGPIO.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef HALGPIO_H_
#define HALGPIO_H_
// 1/2 KB
#define MAX_STRING 524

void DelayUs(unsigned int cnt);
void DelayMs(unsigned int cnt);

char string_buffer[MAX_STRING];
int string_index;
int input_string_length;
int baud_config;

void Print_two_lines(const char *s1,const char *s2);
void InitTimers();
int translate_config_command(char* string);

void Print(const char * s);
void PrintVolt(const char * s);
void change_color();

#endif /* HALGPIO_H_ */
