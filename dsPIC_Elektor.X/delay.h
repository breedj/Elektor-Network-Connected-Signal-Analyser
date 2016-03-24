void Delay( unsigned int delay_count );
void Delay_Us( unsigned int delayUs_count );

#define Delay200uS_count  (FCY * 0.0002) / 1080
#define Delay_1mS_Cnt	  (FCY * 0.001) / 2950
#define Delay_2mS_Cnt	  (FCY * 0.002) / 2950
#define Delay_5mS_Cnt	  (FCY * 0.005) / 2950
#define Delay_10mS_Cnt    (FCY * 0.010) / 2950
#define Delay_15mS_Cnt 	  (FCY * 0.015) / 2950
#define Delay_250mS_Cnt   (FCY * 0.250) / 2950
#define Delay_1S_Cnt	  (FCY * 1) / 2950
