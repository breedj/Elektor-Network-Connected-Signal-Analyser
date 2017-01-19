
#include <xc.h>
#include "user.h"           
#include "p33exxxx.h"


void SetAdc1(unsigned char AD12B, unsigned char ADCS)
{

        AD1CON1bits.ADON = 0;           //make sure ADC is off for setup
        AD1CON1bits.FORM   = 0;		// Data Output Format: integer
        AD1CON3bits.ADRC = 0;		// ADC Clock is derived from Systems Clock
        AD1CON1bits.ASAM   = 1;		// ADC Sample Control: Sampling begins immediately after conversion
        AD1CON1bits.SSRC   = 2;         // Sample Clock Source: GP Timer3 starts conversion

//10bit/12bit settings
//        AD1CON3bits.ADCS = 4;		// 10 bit ADC Conversion Clock Tad=Tcy*(ADCS+1)= (1/60M)*5 = 83.3ns (spec min=75nsec)
//        AD1CON3bits.ADCS = 6;		// 12 bit ADC Conversion Clock Tad=Tcy*(ADCS+1)= (1/60M)*7 = 116.6ns (spec min=117nsec)
        AD1CON1bits.AD12B  = AD12B;     // selects 10bit/12-bit ADC operation (for 10 bit, AD12B=0, for 12bit, AD12B=1)
        AD1CON3bits.ADCS = ADCS;	//sets ADCS; ADC Conversion Clock Tad=Tcy*(ADCS+1) where Tcy = 60 MHz
        AD1CON3bits.SAMC = 3;           // Auto Sample Time = 3*Tad for 12bit (only used if autosampling, i.e., SSRC=7))
        AD1CON2bits.CHPS  = 0;          // Converts CH0
        AD1CON1bits.ADDMABM = 1; 	// DMA buffers are built in conversion order mode (not using right now)
        AD1CON2bits.SMPI    = 0;	// SMPI must be 0
        AD1CON4bits.ADDMAEN = 1; 	// all results written in ADC1BUF0

        //A/D Input Select Register
        AD1CHS0bits.CH0SA=3;		// MUXA +ve input selection (AN3) for CH0
	AD1CHS0bits.CH0NA=0;		// MUXA -ve input selection (Vref-) for CH0
        ANSELBbits.ANSB1 = 1;           // Ensure AN3/RB1 is analog for dsPIC33EP512MC502

        CM2CONbits.OPMODE = 1;      //Op Amp/Comparator Operation Mode Select bit; Operates as an Op Amp
        //CM2CON =0x8C00;             //see errata bit 11 = 1; CON = 1; OPMODE =1;
        CM2CONbits.CON = 1;     //enable op amp module

        ANSELBbits.ANSB0 = 1;       //RB0 is IN- of op amp
        ANSELAbits.ANSA0 = 1;       //RA0 is op amp output
        ANSELAbits.ANSA1 = 1;       //RA1 is IN+ of op amp

        TRISBbits.TRISB0 = 1;
        TRISAbits.TRISA0 = 1;/////////////////////////////////Goofy but this makes opamp work (should be output but that doesn't work)
        TRISAbits.TRISA1 = 1;

        IFS0bits.AD1IF = 0;			// Clear the A/D interrupt flag bit
        //IEC0bits.AD1IE = 0;			// do not Enable A/D interrupt
        IEC0bits.AD1IE = 1;			// Enable A/D interrupt
        AD1CON1bits.ADON = 1;                   // Turn on the A/D converter
 }

/*=============================================================================
When Timer3 times out, the ADC module
will stop sampling and trigger a conversion.
At that time, the conversion process starts and completes Tc=(12 or 14)*Tad periods later,
depending on 10bit or 12bit selection.
When the conversion completes, the module starts sampling again. However, since Timer3
is already on and counting, about (Ts-Tc)us later, Timer3 will expire again and trigger
next conversion.
=============================================================================*/
void SetTmr3(int Period)
{
        T3CONbits.TON = 0;      //turn timer off
        TMR3 = 0x0000;          //reset count
        PR3 = Period;           //period = (Fosc/2/Fs)-1 = (60MHz/Fs)-1
        IFS0bits.T3IF = 0;
        IEC0bits.T3IE = 0;      //not using interrupt
        T3CONbits.TON = 1;      //start timer
}

/*=============================================================================
 DDS Tiimer1
=============================================================================*/

void SetTmr1(int Period)
{
        T1CONbits.TON = 0;      //turn timer off
        TMR1 = 0x0000;          //reset count
        PR1 = Period;           //DDS updat rate = (Fosc/2/Period)-1 = (60MHz/Period)-1
        //IPC0bits.T1IP = 0x01;   // Sets ADC interrupt prioity higher than DDS
        //IPC0bits.T1IP = 0x05;   // Set DDS Timer 1 Interrupt Priority Level higher than ADC; reset default is level 4 for ADC
                                //want ADC lower prioroty lower than T1 so signal generator steps happen on time smoothly
                                //ADC sampling still starts exactly at T3 timeout; ADC interrup just stores last ADC sample
        IFS0bits.T1IF = 0;
        IEC0bits.T1IE = 1;      //enable interrupt
        T1CONbits.TON = 1;      //start timer
}

/*================================================================================
_ADC1Interrupt()
=================================================================================*/
void __attribute__((interrupt, no_auto_psv)) _AD1Interrupt (void)
{
        extern unsigned int N, MainBufferIndex,MainBufferFull;
        extern unsigned int MainBuffer[];

//        LATBbits.LATB14 = 0;        //clear impulse bit after one sample period
        MainBuffer[MainBufferIndex] = ADC1BUF0;
        MainBufferIndex ++;
	//mLED_2_Toggle()                         // debugging aid
	//IFS0bits.AD1IF = 0;			// Clear the A/D interrupt flag bit
//        if(MainBufferIndex == N/2)
//            LATBbits.LATB14 = 1;  //set impluse bit for one sample period
        if(MainBufferIndex >= N) //check to see if you have collected N samples
        {
            MainBufferIndex = 0;
            MainBufferFull = 1; //flag to say data ready to send to PC
            IEC0bits.AD1IE = 0;			//disEnable A/D interrupt

        }
        IFS0bits.AD1IF = 0;			// Clear the A/D interrupt flag bit
}







