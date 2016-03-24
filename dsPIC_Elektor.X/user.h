#define SYS_FREQ        120000000L
#define FCY             SYS_FREQ/2

/******************************************************************************/
//WIZNET Stuff                                                 */
/******************************************************************************/

#define WIZRESET LATBbits.LATB10
#define WIZCS    LATBbits.LATB6
#define WIZRDY   PORTBbits.RB12

/** LEDs ************************************************************/
#define mInitAllLEDs()      LATB &= 0xFFCF; TRISB &= 0xFFCF; LATA &= 0xFFEF; TRISA &= 0xFFEF;

    #define mLED_1              LATBbits.LATB5      
    #define mLED_2              LATAbits.LATA4
    #define mLED_3              LATBbits.LATB4
    #define mLED_4              LATBbits.LATB5

    #define mGetLED_1()         mLED_1
    #define mGetLED_2()         mLED_2
    #define mGetLED_3()         mLED_3
    #define mGetLED_4()         mLED_4

    #define mLED_1_On()         mLED_1 = 1;
    #define mLED_2_On()         mLED_2 = 1;
    #define mLED_3_On()         mLED_3 = 1;
    #define mLED_4_On()         mLED_4 = 1;

    #define mLED_1_Off()        mLED_1 = 0;
    #define mLED_2_Off()        mLED_2 = 0;
    #define mLED_3_Off()        mLED_3 = 0;
    #define mLED_4_Off()        mLED_4 = 0;

    #define mLED_1_Toggle()     mLED_1 = !mLED_1;
    #define mLED_2_Toggle()     mLED_2 = !mLED_2;
    #define mLED_3_Toggle()     mLED_3 = !mLED_3;
    #define mLED_4_Toggle()     mLED_4 = !mLED_4;

/** Switches *********************************************************/
    #define mInitSwitch1()      TRISBbits.TRISB13=1;
    #define mInitSwitch2()      TRISBbits.TRISB14=1;
    #define mInitAllSwitches()  mInitSwitch1();mInitSwitch2();
    #define sw1                 PORTBbits.RB14
    #define sw2                 PORTBbits.RB13



 





