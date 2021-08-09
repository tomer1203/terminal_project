#ifndef TFC_UART_H_
#define TFC_UART_H_

#define CORE_CLOCK 48000000


void Uart0_Br_Sbr(int sysclk, int baud);
void InitUARTs();
void UART0_IRQHandler();
char uart_getchar (UART_MemMapPtr channel);
void uart_putchar (UART_MemMapPtr channel, char ch);
void UARTprintf(UART_MemMapPtr channel,const char* str);
#endif /* TFC_UART_H_ */
