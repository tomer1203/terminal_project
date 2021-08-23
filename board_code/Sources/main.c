/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable
char string_buffer[MAX_STRING];

int main(void){
	
	ClockSetup();
	InitGPIO();
	InitUARTs();
	dma0_init();
	initialize_ui();
	initialize_file_system();
	print_ui();    
	disable_irq(INT_UART0-16);               			    // Disable UART0 interrupt
	DMAMUX0_CHCFG0 |= DMAMUX_CHCFG_ENBL_MASK; 				// Enable DMA channel 
	UART0_C5 |= UART0_C5_RDMAE_MASK;
	
	DMA_DSR_BCR0 |= DMA_DSR_BCR_DONE_MASK;			// Clear Done Flag
	DMAMUX0_CHCFG0 &= ~DMAMUX_CHCFG_ENBL_MASK;	    // Disable DMA Channel 0
	UART0_C5 &= ~UART0_C5_RDMAE_MASK; 				// Disabling DMA using UART
	enable_irq(INT_UART0-16);						// Enable UART0 interrupt
	Fsm();
	
	return 0;
	
}



