#include "user.h"
#include "delay.h"
#include "P33Exxxx.h"

unsigned int temp_count;

void Delay( unsigned int delay_count )
{
	temp_count = delay_count +1;
	asm volatile("outer: dec _temp_count");
	asm volatile("cp0 _temp_count");
	asm volatile("bra z, done");
	asm volatile("repeat #3200" );
	asm volatile("nop");
	asm volatile("repeat #3200" );
	asm volatile("nop");
	asm volatile("bra outer");
	asm volatile("done:");
}

void Delay_Us( unsigned int delayUs_count )
{
	temp_count = delayUs_count +1;
	asm volatile("outer1: dec _temp_count");
	asm volatile("cp0 _temp_count");
	asm volatile("bra z, done1");
	asm volatile("repeat #1500" );
	asm volatile("nop");
	asm volatile("repeat #1500" );
	asm volatile("nop");
	asm volatile("bra outer1");
	asm volatile("done1:");
}

