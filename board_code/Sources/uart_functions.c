/*
 * uart_functions.c

 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */
#include "TFC.h"
void clear_string_buffer(){
	int i = 0;
	for (i = 0;i < MAX_STRING;i++){
		string_buffer[i] = 0;
	}
	string_index = 0;
	input_string_length=0;
}
int is_chat_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='T' && string[3]=='x' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
int is_write_file_transfer_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='W' && string[3]=='f' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
int is_br_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='B' && string[3]=='r' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
int is_size_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='S' && string[3]=='z' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
int is_name_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='N' && string[3]=='a' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
int is_write_data_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='W' && string[3]=='d' && string[4]==']'){
		return 1;
	} else {
		return 0;
	}
}
char* strip_command(char* string){
	return &string[11];
}

void change_Baud_config(int Baud_rate){
	UART0_C2 &= (~UARTLP_C2_RE_MASK) & (~UARTLP_C2_TE_MASK) & (~UART_C2_RIE_MASK);
	DelayMs(10);
	Uart0_Br_Sbr(CORE_CLOCK/2/1000, Baud_rate);
	UART0_C2 = UARTLP_C2_RE_MASK | UARTLP_C2_TE_MASK | UART_C2_RIE_MASK;
	DelayMs(10);
}

int validate_checksum(char* string, int len) {
	int i = 0;
	int checksum = 0;
	char received_checksum;
	// i==5 is the checksum byte and it can be 0
	for (i=0; i < MAX_STRING && i < len; i++){
		checksum = checksum ^ string[i];
	}

	received_checksum = string[5];
	if (checksum == 1 && received_checksum == 1) {
		return 1;
	}
	return (checksum == 0);
}

char calc_checksum(char * string,int len){
	int i = 0;
	char checksum = 0;
	
	// i==5 is the checksum byte and it can be 0
	for (i=0; i < MAX_STRING && i < len; i++){
		checksum = checksum ^ string[i];
	}
	return checksum;
}

void send2pc(char* code,const char* message){
	char output[MAX_STRING] = {0}, tmp[MAX_STRING] = {0}, 
			Length[3] = {0}, Length_final[3] = {0};
	char checksum, dummy_checksum[2] = "a";
	int  length_int;
	strcpy(tmp,message);
	
	sprintf(Length,"%d",strlen(tmp));
	if (strlen(tmp) < 10){
		strcpy(Length_final, "00");
		strcat(Length_final,Length);
	} else if (strlen(tmp) < 100){
		strcpy(Length_final, "0");
		strcat(Length_final,Length);
	}else{
		strcpy(Length_final,Length);
	}

	strcpy(output,"$[");
	strcat(output,code);
	strcat(output,"]");
	strcat(output,dummy_checksum);
	strcat(output,"|");
	strcat(output,Length_final);
	strcat(output,"|");
	strcat(output,tmp);
	length_int = strlen(tmp)+11;
	checksum = calc_checksum(output,length_int);
	checksum = checksum ^ 'a'; // get rid of the dummy checksum effect
	if(checksum == 0)
		checksum = 1;
	output[5] = checksum;
	UARTprintf(UART0_BASE_PTR, output);
}
