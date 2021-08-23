#include "TFC.h"
#include "mcg.h"

#define MUDULO_REGISTER  0x2EE0

// set I/O for switches and LEDs
void InitGPIO() {
	//enable Clocks to all ports - page 206, enable clock to Ports
	SIM_SCGC5 |=
			SIM_SCGC5_PORTA_MASK | SIM_SCGC5_PORTB_MASK | SIM_SCGC5_PORTC_MASK
					| SIM_SCGC5_PORTD_MASK | SIM_SCGC5_PORTE_MASK;

	//GPIO Configuration - LEDs - Output
	PORTD_PCR1 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK;  //Blue
	GPIOD_PDDR |= BLUE_LED_LOC; //Setup as output pin	
	PORTB_PCR18 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK; //Red  
	PORTB_PCR19 = PORT_PCR_MUX(1) | PORT_PCR_DSE_MASK; //Green
	GPIOB_PDDR |= RED_LED_LOC + GREEN_LED_LOC; //Setup as output pins

	RGB_LED_OFF;

	//GPIO Configuration - Pushbutton - Input
	PORTD_PCR7 = PORT_PCR_MUX(1); // assign PTD7 as GPIO
	GPIOD_PDDR &= ~PORT_LOC(7);  // PTD7 is Input
	PORTD_PCR7 |= PORT_PCR_PS_MASK | PORT_PCR_PE_MASK | PORT_PCR_PFE_MASK | PORT_PCR_IRQC(0x0a);

	PORTD_PCR6 = PORT_PCR_MUX(1); // assign PTD7 as GPIO
	GPIOD_PDDR &= ~PORT_LOC(6);  // PTD7 is Input
	PORTD_PCR6 |= PORT_PCR_PS_MASK | PORT_PCR_PE_MASK | PORT_PCR_PFE_MASK | PORT_PCR_IRQC(0x0a);

	enable_irq(INT_PORTD - 16); // Enable Interrupts 
	set_irq_priority(INT_PORTD - 16, 0);  // Interrupt priority = 0 = max

	// LCD
	PORTE_PCR3 = PORT_PCR_MUX(1); // LCD - RS
	PORTE_PCR4 = PORT_PCR_MUX(1); // LCD - R/w
	PORTE_PCR5 = PORT_PCR_MUX(1); // LCD - ENABLE

	PORTB_PCR0 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR1 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR2 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT
	PORTB_PCR3 = PORT_PCR_MUX(1); // LCD - DATA OUTPUT

	GPIOB_PDDR |= 0xF; // Setup LCD DATA pins as output

	lcd_init(); // init the LCD
}

void dma0_init(void)
{
	// Enable clocks
		SIM_SCGC6 |= SIM_SCGC6_DMAMUX_MASK;
		SIM_SCGC7 |= SIM_SCGC7_DMA_MASK;
		
		// Disable DMA Mux channel first
		DMAMUX0_CHCFG0 = 0x00;
		
		// Configure DMA
		DMA_SAR0 = (uint32_t)&UART0_D; // Read from ADC0 sample
		DMA_DAR0 = (uint32_t)&string_buffer[11];
		// number of bytes yet to be transferred for a given block - 2 bytes(16 bits)
		DMA_DSR_BCR0 = DMA_DSR_BCR_DONE_MASK;  // REMOVED THE NUMBER OF BYTES(MOVED TO THE UART INTERRUPT)
		
		DMA_DCR0 |= (DMA_DCR_EINT_MASK|		// Enable interrupt
					 DMA_DCR_ERQ_MASK |		// Enable peripheral request
					 DMA_DCR_CS_MASK  |		// Cycle Steal
					 DMA_DCR_SSIZE(1) |		// Set source size to 8 bits //CHANGED FROM 2(16)
					 DMA_DCR_DINC_MASK|		// Set increments to destination address
					 DMA_DCR_DMOD(15)  |    // Destination address modulo of 2Kb
					 DMA_DCR_DSIZE(1));		// Set destination size of 8 bits // CHANGED FROM 2(16)
					 
		
		//Config DMA Mux for UART0 operation, Enable DMA channel and source
		DMAMUX0_CHCFG0 |=  DMAMUX_CHCFG_SOURCE(2) ; //CHANGED FROM 40(ADC) ,REMOVED: DMAMUX_CHCFG_ENBL_MASK
		
		// Enable interrupt
		enable_irq(INT_DMA0 - 16);
		
}

//-----------------------------------------------------------------
// TPMx - Clock Setup
//-----------------------------------------------------------------
void ClockSetup() {

	pll_init(8000000, LOW_POWER, CRYSTAL, 4, 24, MCGOUT); //Core Clock is now at 48MHz using the 8MHZ Crystal

	//Clock Setup for the TPM requires a couple steps.
	//1st,  set the clock mux
	//See Page 124 of f the KL25 Sub-Family Reference Manual
	SIM_SOPT2 |= SIM_SOPT2_PLLFLLSEL_MASK;// We Want MCGPLLCLK/2=24MHz (See Page 196 of the KL25 Sub-Family Reference Manual
	SIM_SOPT2 &= ~(SIM_SOPT2_TPMSRC_MASK);
	SIM_SOPT2 |= SIM_SOPT2_TPMSRC(1); //We want the MCGPLLCLK/2 (See Page 196 of the KL25 Sub-Family Reference Manual
	//Enable the Clock to the TPM0 and PIT Modules
	//See Page 207 of f the KL25 Sub-Family Reference Manual
	SIM_SCGC6 |= SIM_SCGC6_TPM0_MASK + SIM_SCGC6_TPM2_MASK;
	// TPM_clock = 24MHz , PIT_clock = 48MHz

}





