; Included required library files
INCLUDE "lib/system.s"
INCLUDE "lib/debug.s"
INCLUDE "lib/keys.s"
INCLUDE "lib/vga.s"

; Define origin offset of application
ORG 0x1000

; First instruction - jump to entry point
JS  START

; Boot message
STR_BOOT:
	DB "SIMU16-BIOS" + " VER 1.1", 0x0A, 0x00

; Keyboard init message
STR_KBD_INIT:
	DB "Initialized keyboard driver", 0x0A, 0x00
	
STR_TEST:
	DB "Fuck you!", 0x0A, 0x00

; Entry point of application
START:
	LD 		BP, STACK_ADDR			; Load stack address into BP
	LDR		SP, BP					; Copy stack address from BP to SP
	LD		RS, STR_BOOT			; Load boot message into source register
	JS		DEBUG_PRINT				; Call debug print string function
	LD		RA, 0x69				; Load test bank index into RA
	JS		SET_BANKINDEX			; Set bank index	
	JS 		ENABLE_BANKING			; Enable bank switching
	LD		RA, IDTR_ADDR			; Load IDT address into RA
	DIV		RA, 256					; Divide address by page size to get index
	JS		SET_IDTR				; Call function to set interrupt descriptor table register
	JS		ENABLE_IRQS				; Enable interrupts
	JS		KBD_INIT				; Call keyboard initialization function	
	
	LD 		RD, SECTOR_BUFFER		; Load sector buffer address into RD
	LD 		RA, 0x0041				; Load starting sector into RA
	LD		RC, 1					; Load amount of sectors into RC
	JS 		READ_SECTORS			; Call disk read function
	
	JS 		SCREEN_INIT
	
	LD		RC, 0
	JP 		LOOP					; Goto main loop
	
LOOP:
	ADD 	RC, 1					; increment RC
	JS 		SCREEN_PRINTDEC			; Call vga print string function
	JS		SCREEN_NEWLINE			; Newline
	
	JP 		LOOP					; Repeat loop
	
KBD_INIT:
	STW 	IDTR_ADDR, KBD_CALLBACK ; Set callback pointer for keyboard
	LD 		RS, STR_KBD_INIT		; Load init message into source register
	JS		DEBUG_PRINT				; Print message
	RET
	
KBD_CALLBACK:
	IN		KBD_DATA, RA			; Load scancode value into R0
	JS		DEBUG_PUTC				; Directly print scancode - not efficient but works
	IRET

READ_SECTORS:
	LD 		R0, DISK_READ			; Load disk read command into R0
	OUT		SDC_BUFFER, RD			; Send destination address to buffer port
	OUT		SDC_SECTOR, RA			; Send sector index to sector port
	OUT 	SDC_LENGTH, RC			; Send amount of sectors to length port
	OUT		SDC_CMD, R0				; Send disk read command to port
	RET
	