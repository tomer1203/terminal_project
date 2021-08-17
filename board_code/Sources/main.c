/*
 * main implementation: use this 'C' sample to create your own application
 *
 */


//#include "derivative.h" /* include peripheral declarations */
# include "TFC.h"
#define MUDULO_REGISTER  0x2EE0
#define ADC_MAX_CODE     4095

unsigned int LightIntensity = MUDULO_REGISTER/2;  // Global variable

int main(void){
	
	char  s[100];
	int M;
	ClockSetup();
	InitGPIO();
	InitPIT();
	InitADCs();
	InitUARTs();
	enablePIT();
	disablePITx(1);
	initialize_ui();
	initialize_file_system();
	print_ui();
	Fsm();
	return 0;
}



