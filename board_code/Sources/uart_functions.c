/*
 * uart_functions.c
 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */
void clear_string_buffer(){
	int i = 0;
	for (i = 0;i < MAX_STRING;i++){
		string_buffer[i] = 1;
	}
	string_index = 0;
}
int is_chat_command(char* string){
	if (string[0]=='$' && string[1]=='[' && string[2]=='T' && string[3]=='x' && string[4]==']'){
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

char* strip_command(char* string){
	return &string[6];
}
void change_Baud_config(int Baud_rate){
	UART0_C2 &= (~UARTLP_C2_RE_MASK) & (~UARTLP_C2_TE_MASK) & (~UART_C2_RIE_MASK);
	Uart0_Br_Sbr(CORE_CLOCK/2/1000, Baud_rate);
	UART0_C2 = UARTLP_C2_RE_MASK | UARTLP_C2_TE_MASK | UART_C2_RIE_MASK;
}

int checksum(char * string){
	int i = 0;
	int checksum = 0;
	
	// i==5 is the checksum byte and it can be 0
	for (i=0; i < MAX_STRING && ((string[i] != '\0') || (i==5)); i++){
		checksum = checksum ^ string[i];
	}
	return (checksum == 0);
}

