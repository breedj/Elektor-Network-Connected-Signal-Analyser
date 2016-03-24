/******************************************************************************/
/* Files to Include                                                           */
/******************************************************************************/
#include <xc.h>
#include <stdint.h>        /* Includes uint16_t definition*/
#include <stdio.h>
#include <stdbool.h>       /* Includes true/false definition                  */
#include <string.h>
#include "user.h"
#include "delay.h"
#include "socket.h"        //ethernet stuff
#include "dhcp.h"
#include <math.h>
#include <stdlib.h>


_FOSCSEL(FNOSC_FRC & PWMLOCK_OFF);      //starts up using built in osc; release PWM locking feature
_FOSC(FCKSM_CSECMD & OSCIOFNC_OFF & POSCMD_XT); //if using X'tal primary oscillator as primary source
_FWDT(FWDTEN_OFF);

/**********************************************
 * DHCP Shared Buffer Definition for LOOPBACK TEST and DHCP Timer *
 **********************************************/
#define DATA_BUF_SIZE   1024               ////////get rid of this later!!!!!!!!!!!!!!!!!!!!!!
uint8_t gDATABUF[DATA_BUF_SIZE];
volatile uint32_t msTicks;                  /* counts 1ms timeTicks used DHCP timer function*/
uint32_t prevTick;

#define SOCK_TCPS 0
#define SOCK_DHCP 1
#define MY_MAX_DHCP_RETRY 3
#define Rx_BUF_SIZE   64
#define MainBuffersize 21000
#define Port 4000
#define PI 3.1415926536
#define sendDatatoPC 1
#define startGenerator 2
#define stopGenerators 3
#define startNoiseGenerator 4
#define startImpulseGenerator 5
#define changeFcyc 6

#define _MAIN_DEBUG_


uint8_t RxBuff[Rx_BUF_SIZE];             //receives commands and parameter data sent from PC
char tbuff[32],C;                        //buffer and constant used for lcd display; get rid of later///////////////////////////debugging use
unsigned int temp[1024];                 //could eliminate later by modifying send, wiznsend to handle (eds) pointer
uint8_t txsize[8] = {2,2,2,2,2,2,2,2};  //wiz5500 socket sizes
uint8_t rxsize[8] = {2,2,2,2,2,2,2,2};
__eds__ unsigned int MainBuffer[MainBuffersize] __attribute__((eds));
unsigned int BytesRemaining = 0, BytesSent = 0;   //used in ethernet data sends
unsigned int N = 8192;                   //initial FFT size
unsigned long Fs = 504201, Fcyc;               //initial sampling frequency for 12 bit mode so PR3 is exact integer
uint8_t *ptr;
unsigned int MainBufferIndex, MainBufferFull, SamplePeriod;
unsigned char AD12B = 1, ADCS = 6;       //init to 12bit ADC and TAD = (1/60M)(ADCS+1)=116.7nsec
unsigned char N1 = 2;                    //PLL VCO out = M/N1*N2 *8Mhz; Assuming N2 = 2, Fcyc = PLLVco/2
unsigned int M = 60;                    //inits to Fcyc = 60 MHz
uint8_t SIN8[256];                      //used by signal generator to store one cycle of either sine, square or triangle wave
uint32_t DDSp;      //DDS Phase value
uint32_t DDSd;      //DDS Phase delta
//long double tempy;
long double tempy, PWMrate,GenFreq;     //used with signal generator
bool NoiseSignalFlag = false;                 //false means signal generator requested; true means noise generator requested
int HeartBeat;

// Initial Network Configuration  //
wiz_NetInfo gWIZNETINFO = { .mac = {0x00, 0x08, 0xdc,0x00, 0xab, 0xcd},
                            .ip = {192,168, 0, 123},                   //first three fields set by router type
                            .sn = {255,255,255,0},
                            .gw = {192, 168, 0, 1},
                            .dns = {0,0,0,0},
                            .dhcp = NETINFO_STATIC };

//Prototypes
void ConfigureOscillator(unsigned int, unsigned char);
void platform_init(void);
void InitSPI2(void);
void  wizchip_select(void);
void  wizchip_deselect(void);
void  wizchip_write(uint8_t wb);
uint8_t wizchip_read();
void network_init(void);
int32_t tcps(uint8_t, uint8_t * , uint16_t);		// TCP Loop
void SetAdc1(unsigned char, unsigned char);
void SetTmr3(int);
void SetTmr1(int);
void SetTmr5(unsigned int); //DHCP
void my_ip_assign(void);    //DHCP
void my_ip_conflict(void);  //DHCP
void CommandPoll(void);

int16_t main(void)
{
    static uint8_t tmp; //static here to avoide compliler warning re: needing extended memory

    uint8_t my_dhcp_retry = 0;  //DHCP
    uint32_t led_msTick = 1000; //DHCP
    SetTmr5(60000);     //DHCP 1 ms interrupt interval and start timer5
    //int32_t ret = 0;    //DHCP

    ConfigureOscillator(M, N1);      //set up for 60 MIPs
    platform_init();                 //Initialize lcd, analog pins, switches, leds
    srand(1);                        //seed random number generator for noise generator
    
    mLED_1_On();
    /* reset W5500 */
    WIZCS = 1;
    WIZRESET = 0;
    Delay(Delay_5mS_Cnt);   //spec asks for 500 usec
    WIZRESET = 1;
    Delay(Delay_250mS_Cnt);    //spec ask for 150 msec
    #ifdef _MAIN_DEBUG_
        printf("WIZ5500 is reset\r\n");
    #endif
   // while(!WIZRDY);         // Wait until Wiznet ready

    InitSPI2();              //set up SPI2 for communication with WIZ5500
   
    // Declare W5500 driver SPI Functions
    reg_wizchip_cs_cbfunc(wizchip_select, wizchip_deselect);
    reg_wizchip_spi_cbfunc(wizchip_read, wizchip_write);

    //set up socket sizes in wiznet5500
    wizchip_init( txsize, rxsize );
    mLED_3_On();
     
    //Wait for physical link
     do
    {
       if(ctlwizchip(CW_GET_PHYLINK, (void*)&tmp) == -1)
       {
           #ifdef _MAIN_DEBUG_
                printf("Problem with physical link detect\r\n");
           #endif
       }
    }while(tmp == PHY_LINK_OFF);
    #ifdef _MAIN_DEBUG_
        printf("Physical link found\r\n");
    #endif

    mLED_1_On();
    mLED_3_Off();

    switch((sw2<<1) | sw1)                  //set static IP address based on router type (sw1=RB14; sw2 = RB13)
    {
        case 0:
            gWIZNETINFO.ip[0] = 192; gWIZNETINFO.ip[1] = 168; gWIZNETINFO.ip[2] = 1; gWIZNETINFO.ip[3] = 123;   //RB13 = 0; RB14 = 0
            gWIZNETINFO.gw[0] = 192; gWIZNETINFO.gw[1] = 168; gWIZNETINFO.gw[2] = 1; gWIZNETINFO.gw[3] = 1;
            network_init();
            while(1) CommandPoll();     //Main loop when using Static IP 192.168.0.123
            break;
        case 1:
            gWIZNETINFO.ip[0] = 192; gWIZNETINFO.ip[1] = 168; gWIZNETINFO.ip[2] = 0; gWIZNETINFO.ip[3] = 123;   //RB13 = 0; RB14 = 1
            gWIZNETINFO.gw[0] = 192; gWIZNETINFO.gw[1] = 168; gWIZNETINFO.gw[2] = 0; gWIZNETINFO.gw[3] = 1;
            network_init();
            while(1) CommandPoll();     //Main loop when using Static IP 192.168.1.123
            break;
        case 2:
            gWIZNETINFO.mac[0] = 2;     //RB13 = 1; RB14 = 0     DHCP
            break;
        case 3:
            gWIZNETINFO.mac[0] = 0;     //RB13 = 1; RB14 = 1     DHCP
            break;
        default:
            break;
    }


    /************************************************/
    /* DHCP  			            */
    /************************************************/
    // must be set the default mac before DHCP started.
	setSHAR(gWIZNETINFO.mac);

	DHCP_init(SOCK_DHCP, gDATABUF);
	reg_dhcp_cbfunc(my_ip_assign, my_ip_assign, my_ip_conflict);

        prevTick = msTicks; //init previousTick to whatever msTicks is

    /* Main Loop */
    while(1)
    {
        switch(DHCP_run())
        {
                case DHCP_IP_ASSIGN:
                case DHCP_IP_CHANGED:
                        /* If this block empty, act with default_ip_assign & default_ip_update */
                    break;
                case DHCP_IP_LEASED:

                        // TO DO YOUR NETWORK APPs
////////                        if( (ret = tcps(SOCK_TCPS, RxBuff, Port)) < 0) {        //go check if any commands were sent
////////							printf("SOCKET ERROR : %ld\r\n", ret);
////////						}
                        CommandPoll();      //go check for caommand and execute
                        break;
                case DHCP_FAILED:
                        /* ===== Example pseudo code =====  */
                        // The below code can be replaced your code or omitted.
                        // if omitted, retry to process DHCP
                        my_dhcp_retry++;
                        if(my_dhcp_retry > MY_MAX_DHCP_RETRY)
                        {
                                #ifdef _MAIN_DEBUG_
                                printf(">> DHCP %d Failed\r\n", my_dhcp_retry);
                                #endif
                                my_dhcp_retry = 0;
                                DHCP_stop();      // if restart, recall DHCP_init()
                                network_init();   // apply the default static network and print out netinfo to serial
                        }
                        break;
                default:
                        break;
        }

    	/* LED Toggle every 1sec */
    	if((msTicks - prevTick) > led_msTick)
    	{
    		mLED_2_Toggle();
    		prevTick = msTicks;
    	}
      }//end of while() loop
 }//end of main

void CommandPoll (void)
{
     int i;
     int32_t ret = 0;
     uint16_t size = 0, sentsize=0;

     if( (ret = tcps(SOCK_TCPS, RxBuff, Port)) < 0) {        //go check if any commands were sent
        #ifdef _MAIN_DEBUG_
            printf("SOCKET ERROR : %ld\r\n", ret);
        #endif
	}
        HeartBeat ++;       //toggles LED indicating your looping
        if(HeartBeat == 0) mLED_3_Toggle();

        switch((RxBuff[0])) //command in RxBuff[0]
         {
            case sendDatatoPC:
                mLED_2_On();        //debugging to measure ethernet transfer time
                RxBuff[0] = 0;  //reset command to 0 so only once executed
                N = (uint16_t)(RxBuff[1] | RxBuff[2] << 8); //extract N, Fs, AD12B, ADCS from command bytes
                Fs = (unsigned long)RxBuff[5]<<16 | (unsigned long)RxBuff[4]<<8 | (unsigned long)RxBuff[3];
                AD12B = RxBuff[6];       //selects 10 or 12 bit ADC
                ADCS =  RxBuff[7];       //number of Tcyc in TAD
                SetAdc1(AD12B,ADCS);           	//A/D converter to 12 bit (AD12B=1), and TAD = (1/60M)(ADCS+1)=116.7nsec

                Fcyc = 2000000*M/N1;
                SamplePeriod = (Fcyc/Fs)-1;
                SetTmr3(SamplePeriod);		//Initialise the Timer to generate sampling event to ADC

                MainBufferIndex = 0;                //point to beginning of MainBuffer
                MainBufferFull = 0;                 //Main Buffer is empty
                while(!MainBufferFull);             //wait for MainBuffer to fill

                ptr = (uint8_t *)&temp;
                BytesSent = 0;
                BytesRemaining = 2*N;
                while(BytesSent < 2*N)      //later need to change send and wiznet send to allow (eds) ptr; currently these functions use (uint8_t*) pointer
                {
                    for(i = 0; i<1024; i++)
                    {
                    temp[i] = MainBuffer[i + BytesSent/2]; //get next 1024 samples (eds to regular array
                    }
                    size = 2048;
                    if((BytesRemaining) < 2048)size = BytesRemaining; //if less than 2048 bytes to send, send only that
                    sentsize = 0;
                    while(size != sentsize)
                    {
                       ret = send(SOCK_TCPS,ptr+sentsize,size-sentsize);
                       if(ret < 0)
                       {
                          close(SOCK_TCPS);
                          //return ret;
                       }
                       sentsize += ret; // Don't care SOCKERR_BUSY, because if busy, ret = 0.Keeps track of sending 2048 bytes
                       BytesSent += ret;    //keeps track of total bytes sent
                       BytesRemaining -= ret;
                    }
                }
                mLED_2_Off();
                break;
            case startGenerator:
                RxBuff[0] = 0;  //reset command to 0 so only once executed
                NoiseSignalFlag = false;    //used in interrup routine; false means generating signal, not noise
                DDSp = 0;
                PWMrate = 4000000*M/N1/256;
                //tempy = (long double) ((unsigned long)RxBuff[5]<<16 | (unsigned long)RxBuff[4]<<8 | (unsigned long)RxBuff[3]); //extract requested generator frequency from RxBuff
                GenFreq = (long double) ((unsigned long)RxBuff[5]<<16 | (unsigned long)RxBuff[4]<<8 | (unsigned long)RxBuff[3]); //extract requested generator frequency from RxBuff
                //tempy = ((tempy * pow(2,32))/468750.0) + .5;        //DDSd = (requested freq/468750 Hz)*(2^32); PWM running at 120MHz/256 = 468750 Hz (set in platforn_init()
                tempy = ((GenFreq * pow(2,32))/PWMrate) + .5;        //DDSd = (requested freq/468750 Hz)*(2^32); PWM running at 120MHz/256 = 468750 Hz (set in platforn_init()
                DDSd = (uint32_t) tempy ;
                //DDSd = 89712111;    //e.g., (10khz/468750 Hz)*(2^32); PWM running at 120MHz/256 = 468750 Hz

                for(i = 0; i<256; i++)
                {
                switch((RxBuff[7]))                 //selects the tupe of signal to generate
                         {
                    case 1:SIN8[i] = (uint8_t) (128 + 127 * sin((PI * i)/128) + .5);    //one cycle sine; the.5 is for rounding
                        break;
                    case 2:SIN8[i] = (i<128) ? 255 : 0;                                 //one cycle square wave; the.5 is for rounding
                        break;
                    case 3:SIN8[i] = abs((i % 256) - 128);                              //one cycle triangle wave; the.5 is for rounding
                        break;
                        }
                 }

                TRISBbits.TRISB14 = 1;  //impulse bit pin on RB14 is input so no impulse signal generated
                PTCON = 0x8000;     //turn on PWM
                IPC0bits.T1IP = 0x05;   // Set DDS Timer 1 Interrupt Priority Level higher than ADC; reset default is level 4 for ADC
                //IPC0bits.T1IP = 0x01;   // Set DDS Timer 1 Interrupt Priority Level lower than ADC; reset default is level 4 for ADC
                SetTmr1(127);       //Timer Period = (Fcyc/DDS Update Rate)-1 = (60MHz/468750)-1 = 128-1
//////////////                PR4 = 127;
//////////////                T4CONbits.TON = 1;      //start timer 4
//////////////                DMA0CONbits.CHEN = 1;       //turn on DMA
//////////////                //DMA0REQbits.FORCE = 1;// Force first word
                break;
            case stopGenerators:
                RxBuff[0] = 0;  //reset command to 0 so only once executed
                T1CONbits.TON = 0;      //turn timer1 off
                PTCON = 0x0000;     //turn off PWM

//////////////                T4CONbits.TON = 0;      //start timer 4
//////////////                DMA0CONbits.CHEN = 0;       //turn on DMA

                TRISBbits.TRISB14 = 1;  //impulse bit pin on RB14 is input so no impulse signal generated
                LATBbits.LATB14 = 0;    //impluse bit = 0 to be sure; probably not needed
                break;
            case startNoiseGenerator:
                RxBuff[0] = 0;                  //reset command to 0 so only once executed
                NoiseSignalFlag = true;        //flag used in timer1 interrupt to indicate gerating noise, not signal
                TRISBbits.TRISB14 = 1;  //impulse bit pin on RB14 is input so no impulse signal generated
                PTCON = 0x8000;                 //turn on PWM
                IPC0bits.T1IP = 0x01;   // Sets ADC interrupt prioity higher than DDS; allows time for rand() function
                SetTmr1(127);                   //Timer Period = (Fcyc/DDS Update Rate)-1 = (60MHz/468750)-1 = 128-1
                break;
            case startImpulseGenerator:
                TRISBbits.TRISB14 = 0;  //PWM1H on RB14 is output;  not used in Elektor version
                LATBbits.LATB14 = 0;    //impluse bit = 0 initially
                PTCON = 0x0000;     //turn off PWM
                break;
            case changeFcyc:
                RxBuff[0] = 0;  //reset command to 0 so only once executed
                M = (uint16_t)(RxBuff[1] | RxBuff[2] << 8); //extract M
                N1 = RxBuff[6];       //extract N1
                ConfigureOscillator(M, N1); //change Fcyc = 2,000,000*M/N1*2

                PWMrate = 4000000*M/N1/256;         //update DDSd to match new Fcyc
                tempy = ((GenFreq * pow(2,32))/PWMrate) + .5;        //DDSd = (requested freq/468750 Hz)*(2^32); PWM running at 120MHz/256 = 468750 Hz (set in platforn_init()
                DDSd = (uint32_t) tempy ;
                break;
            default:
                break;
         }//end switch
}

void  wizchip_select(void) {
    WIZCS = 0;}
void  wizchip_deselect(void) {
    WIZCS = 1;}
void  wizchip_write(uint8_t wb)
{
    uint8_t dummy;
    SPI1BUF = wb;			// write to buffer for TX
    while( !SPI1STATbits.SPIRBF );      // wait for TX complete
    dummy = SPI1BUF;                    // empties
}
uint8_t wizchip_read()
{
    SPI1BUF = 0x00;			// write to buffer for TX
    while( !SPI1STATbits.SPIRBF );      // wait for TX complete
    return SPI1BUF;			// read the received values
}

// Intialize the network information to be used in WIZCHIP //
void network_init(void)
{
        uint8_t tmpstr[6] = {0,};
	wiz_NetInfo netinfo;

	// Set Network information from netinfo structure
	ctlnetwork(CN_SET_NETINFO, (void*)&gWIZNETINFO);

#ifdef _MAIN_DEBUG_
	// Get Network information
	ctlnetwork(CN_GET_NETINFO, (void*)&netinfo);

	// Display Network Information
	ctlwizchip(CW_GET_ID,(void*)tmpstr);

	if(netinfo.dhcp == NETINFO_DHCP) printf("\r\n=== %s NET CONF : DHCP ===\r\n",(char*)tmpstr);
	else printf("\r\n=== %s NET CONF : Static ===\r\n",(char*)tmpstr);

	printf("MAC: %02X:%02X:%02X:%02X:%02X:%02X\r\n",netinfo.mac[0],netinfo.mac[1],netinfo.mac[2],
			netinfo.mac[3],netinfo.mac[4],netinfo.mac[5]);
	printf("SIP: %d.%d.%d.%d\r\n", netinfo.ip[0],netinfo.ip[1],netinfo.ip[2],netinfo.ip[3]);
	printf("GAR: %d.%d.%d.%d\r\n", netinfo.gw[0],netinfo.gw[1],netinfo.gw[2],netinfo.gw[3]);
	printf("SUB: %d.%d.%d.%d\r\n", netinfo.sn[0],netinfo.sn[1],netinfo.sn[2],netinfo.sn[3]);
	printf("DNS: %d.%d.%d.%d\r\n", netinfo.dns[0],netinfo.dns[1],netinfo.dns[2],netinfo.dns[3]);
	printf("===========================\r\n");
#endif
}


// TCP Loop using ioLibrary_BSD			 //
int32_t tcps(uint8_t sn, uint8_t * buf, uint16_t port)
{
   int32_t ret;
   uint16_t size = 0;       // sentsize=0;
   switch(getSn_SR(sn))
   {
      case SOCK_ESTABLISHED :
         if(getSn_IR(sn) & Sn_IR_CON)       //connection with peer is successful
         {
            setSn_IR(sn,Sn_IR_CON);
         }
         if((size = getSn_RX_RSR(sn)) > 0)
         {
            if(size > Rx_BUF_SIZE) size = Rx_BUF_SIZE;
            ret = recv(sn,buf,size);        //receive command and parameter values
            if(ret <= 0) return ret;
         }
         break;
      case SOCK_CLOSE_WAIT :
         if((ret=disconnect(sn)) != SOCK_OK) return ret;
          break;
      case SOCK_INIT :
         if( (ret = listen(sn)) != SOCK_OK) return ret;
         break;
      case SOCK_CLOSED:
          if((ret=socket(sn,Sn_MR_TCP,port,0x00)) != sn)
            return ret;
         break;
      default:
         break;
   }
   return 1;
}

void platform_init(void)
{
    ANSELA = ANSELB = 0x0000; //init analog ports
    mInitAllLEDs();
    mInitAllSwitches();
    mLED_1_Off();
    mLED_2_Off();
    mLED_3_Off();

    TRISBbits.TRISB10 = 0;  // WIZNET RESET output
    TRISBbits.TRISB6 = 0; // WIZNeT CS output
    TRISBbits.TRISB12 = 1;  // WIZRDY input

#ifdef _MAIN_DEBUG_
    RPOR1bits.RP37R = 1;   //pin 14/RP37/D1 assigned to U1tx
    U1BRG = 389;            // 60Mhz Fp, 9600 Baud
    U1MODEbits.UARTEN = 1;  // And turn the peripheral on
    TRISBbits.TRISB5 = 0;   //B5 is an output
#endif

    //set up PWM for signal/noise generator
    TRISBbits.TRISB15 = 0;  //PWM1L on RB15 is output
    TRISBbits.TRISB14 = 1;  //PWM1H on RB14 is input initially; this is the impulse output bit when in impulse generation mode
    LATBbits.LATB14 = 0;    //PWM1H not being used here; set to 0 for now

    PTCON = 0x0000;     //turn off PWM
    PTCON2bits.PCLKDIV = 0;     //1:1 Fosc divider; PWM clock is 120 MHz
    PTPER = 256;          //PWM running 120MHz/256 = 468750 Hz; 256 pulse width steps per cycle available
    PHASE1 = 0;
    PDC1 = 128;           //50% duty cycle for now
    DTR1 = 0;
    //IOCON1 = 0xC400;      //Set PWM Mode to Redundant mode) and connect PWM1L/PWM1H pins to PWM
    IOCON1 = 0x4400;
    IOCON2 = 0x0000;      //need this to ensure other PWMs are off; defaults to 0xC000 on reset
    IOCON3 = 0x0000;
    PWMCON1 = 0x0000;
    FCLCON1 = 0x0003;
    PTCONbits.EIPU = 1;   //immediate update PDC enabled
    PWMCON1bits.IUE = 1;
//    PTCON = 0x8000;     //turn on PWM

////////////    TMR4 = 0x0000;
////////////    PR4 = 4999;             // Trigger ADC1 every 125 ?s @ 40 MIPS
////////////    IFS1bits.T4IF = 0;   // Clear Timer4 interrupt
////////////    IEC1bits.T4IE = 1;
////////////    //T4CONbits.TON = 1;
////////////
////////////    DMA0CONbits.CHEN =0;        //disable dma
////////////    DMA0CONbits.SIZE = 1;       //signal gen table byte for now
////////////    DMA0CONbits.DIR = 1;        //from RAM to peripheral
////////////    DMA0CONbits.AMODE = 0;      //register indirect, post increment
////////////    DMA0CONbits.MODE = 0;       //continuous, ping pong off
////////////    DMA0REQbits.IRQSEL = 0x1C;  //timer 4 triggers dma
////////////
////////////    DMA0STAL = __builtin_dmaoffset(SIN8);
////////////    DMA0STAH = 0x0000;
////////////
////////////    IFS0bits.DMA0IF = 0;// Clear the DMA Interrupt Flag bit
////////////    IEC0bits.DMA0IE = 0;// disable the DMA Interrupt
////////////
////////////    DMA0PAD = (volatile unsigned int)&PDC1;     //from RAM to PWM duty cycle
////////////    DMA0CNT = 126;
////////////
////////////    //DMA0CON.CHEN = 1;       //turn on DMA

  }
void InitSPI2(void)
{
    SPI1STATbits.SPIEN = 0;         //turn SP1 off to setup
    SPI1CON1bits.CKE = 1;           //serial data changes on clock hi to lo (not used in framed mode)
    SPI1CON1bits.MSTEN = 1;         //master mode enabled
    SPI1CON1bits.SMP = 1;           //Input data is sampled at end of data output time (recommended for fastest speed; set after MSTEN)
    SPI1CON1bits.PPRE = 0b11;       //Primary prescale 1:1
    SPI1CON1bits.SPRE = 0b010;      //Secondary prescale 6:1 (10MHz)
    //SPI1CON1bits.SPRE = 0b100;      //Secondary prescale 4:1 (15MHz)
    SPI1STATbits.SPIEN = 1;         //turn SP1 on
}
void ConfigureOscillator(unsigned int M, unsigned char N1)
{
    // Configure the device PLL to obtain 60 MIPS operation. The crystal
    // frequency is 8MHz. Divide 8MHz by 2, multiply by 60 and divide by
    // 2. This results in Fosc of 120MHz. The CPU clock frequency is
    // Fcy = Fosc/2 = 60MHz. Wait for the Primary PLL to lock

	//PLLFBD = 58;			// M  = 60 (init value)
        PLLFBD = M-2;			// M  = 60
	CLKDIVbits.PLLPOST = 0;		// N2 = 2
        CLKDIVbits.PLLPRE = N1-2;	// N1 = 2   (init value)
       //CLKDIVbits.PLLPRE = 0;		// N1 = 2       //needed for 8 Mhz crystal
	OSCTUN = 0;

        __builtin_write_OSCCONH(0x03);    //for switching to primary oscillator with PLL
        __builtin_write_OSCCONL(0x01);
	while (OSCCONbits.COSC != 0x3);
}

void __attribute__((__interrupt__, no_auto_psv)) _T1Interrupt(void)
{
//mLED_2_Toggle();        //debugging tool
    
if(!NoiseSignalFlag){
    PDC1 = SIN8[(DDSp>>24)];                //we are generating signal

}else
    PDC1 = (uint8_t) (rand() & 0x00FF);     //we are generating noise

DDSp += DDSd;       //increment phase accumulator
IFS0bits.T1IF = 0; //Clear Timer1 interrupt flag

}

void SetTmr5(unsigned int Period)
{
        T5CONbits.TON = 0;      //turn timer off
        TMR5 = 0x0000;          //reset count
        PR5 = Period;           //rate = (Fosc/2/Period)-1 = (60MHz/Period)-1
        IPC7bits.T5IP = 0x01;   // DHCP Timer-Set Timer 5 Interrupt Priority Level to low level priority
        IFS1bits.T5IF = 0;
        IEC1bits.T5IE = 1;      //enable interrupt
        T5CONbits.TON = 1;      //start timer
}
/*****************************************************************************
 * Interrupt Service Routine for system tick counter
 *****************************************************************************/
void __attribute__((__interrupt__, no_auto_psv)) _T5Interrupt(void)
{
        //mLED_2_Toggle();          //debugging tool
	msTicks++;                  // increment ms tick counter )
	if(msTicks % 1000 == 0)	DHCP_time_handler();
        IFS1bits.T5IF = 0; //Clear Timer1 interrupt flag
}

/*******************************************************
 * @ brief Call back for ip assign & ip update from DHCP
 *******************************************************/
void my_ip_assign(void)
{
   getIPfromDHCP(gWIZNETINFO.ip);
   getGWfromDHCP(gWIZNETINFO.gw);
   getSNfromDHCP(gWIZNETINFO.sn);
   getDNSfromDHCP(gWIZNETINFO.dns);
   gWIZNETINFO.dhcp = NETINFO_DHCP;
   /* Network initialization */
   network_init();      // apply from dhcp
#ifdef _MAIN_DEBUG_
   printf("DHCP LEASED TIME : %ld Sec.\r\n", getDHCPLeasetime());
#endif
}

/************************************
 * @ brief Call back for ip Conflict DHCP
 ************************************/
void my_ip_conflict(void)
{
#ifdef _MAIN_DEBUG_
	printf("CONFLICT IP from DHCP\r\n");
#endif
   //halt or reset or any...
   while(1); // this example is halt.
}

////void __attribute__((__interrupt__,no_auto_psv)) _DMA0Interrupt(void)
////{
////IFS0bits.DMA0IF = 0;
////}

////void __attribute__((__interrupt__, no_auto_psv)) _T4Interrupt(void)
////{
////        mLED_2_Toggle();          //debugging tool
////        IFS1bits.T4IF = 0; //Clear Timer1 interrupt flag
////}



