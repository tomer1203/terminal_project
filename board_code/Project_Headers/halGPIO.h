/*
 * halGPIO.h
 *
 *  Created on: May 5, 2021
 *      Author: им
 */

#ifndef HALGPIO_H_
#define HALGPIO_H_
#define MAX_STRING 255

void DelayUs(unsigned int cnt);
void DelayMs(unsigned int cnt);

char string_buffer[MAX_STRING];
int string_index;


void InitTimers();
void Print(const char * s);
void PrintVolt(const char * s);
void change_color();

#endif /* HALGPIO_H_ */
