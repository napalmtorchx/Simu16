INCLUDE "core/cmdline.s"
INCLUDE "core/exeload.s"
INCLUDE "hal/disk.s"
INCLUDE "hal/keyboard.s"
INCLUDE "hal/uart.s"
INCLUDE "hal/vga.s"
INCLUDE "util/debug.s"
INCLUDE "util/system.s"
INCLUDE "common.s"

ORG 0x1000
JP START

STR_BOOT:
	DB "PurpleMoon Operating System", 0x0A, 0x00

START:
	LD 		BP, STACK_ADDR			; Load stack address into BP
	LDR		SP, BP					; Copy stack address from BP into SP
	LD		RS, STR_BOOT			; Load boot message into source register
	JS		DEBUG_PRINT				; Call debug print string function
	JP 		LOOP
	
LOOP:
	JP 		LOOP
	
