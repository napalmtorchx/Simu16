; Default stack address space
DEFINE STACK_ADDR 0xE000
DEFINE IDTR_ADDR  0x3000

; Common argument offsets
DEFINE ARG0 0x04
DEFINE ARG1 0x06
DEFINE ARG2 0x08
DEFINE ARG3 0x0A
DEFINE ARG4 0x0C
DEFINE ARG5 0x0E
DEFINE ARG6 0x10
DEFINE ARG7 0x12

; Interrupt request identifiers
DEFINE IRQ_KEYBOARD 0x00
DEFINE IRQ_TIMER    0x01

; Serial/UART COM1 Ports
DEFINE UART_COM1_CMD  0x03A0		; COM1 Command port
DEFINE UART_COM1_DATA 0x03A1		; COM1 Data port

; Serial/UART COM2 Ports
DEFINE UART_COM2_CMD  0x03B0		; COM2 Command port
DEFINE UART_COM2_DATA 0x03B1		; COM2 Data port

; Serial/UART COM3 Ports
DEFINE UART_COM3_CMD  0x03C0		; COM3 Command port
DEFINE UART_COM3_DATA 0x03C1		; COM3 Data port

; Serial/UART COM4 Ports	
DEFINE UART_COM4_CMD  0x03D0		; COM4 Command port
DEFINE UART_COM4_DATA 0x03D1		; COM4 Data port

; Serial/UART Commands
DEFINE UART_PUTC      0x01			; Print single character to UART
DEFINE UART_PRINT     0x02			; Print zero-terminated string to UART
DEFINE UART_PRINT_INT 0x03			; Print integer value to UART
DEFINE UART_RESET     0xFF			; Reset UART

; Keyboard Controller Ports
DEFINE KBD_CMD  0x0060				; Keyboard command port
DEFINE KBD_DATA 0x0061				; Keyboard data port

; Disk Controller Ports
DEFINE SDC_CMD    0x0080	; Serial disk controller command port
DEFINE SDC_BUFFER 0x0081	; Serial disk controller buffer port
DEFINE SDC_SECTOR 0x0082	; Serial disk controller sector port
DEFINE SDC_LENGTH 0x0083	; Serial disk controller length port

; Disk Controller Commands
DEFINE DISK_READ   0x0001	; Command to read sector from disk
DEFINE DISK_WRITE  0x0002	; Command to write sector to disk	
DEFINE DISK_DETECT 0x0003	; Command to check if disk is detected

; Disk Information
DEFINE SECTOR_SIZE   512	; Disk sector size in bytes
DEFINE SECTOR_BUFFER 0xD000	; Reserved memory for disk buffer

; Enable bank switching
ENABLE_BANKING:
	LDR 	R0, EF			; Copy execution flags into R0
	OR  	R0, 0b1000		; Set bank enable bit
	LDR 	EF, R0			; Restore execution flags back into EF
	RET	

; Disable bank switching
DISABLE_BANKING:
	LDR 	R0, EF			; Copy execution flags into R0
	AND		R0, 0b0111		; Clear bank enable bit
	LDR 	EF, R0			; Restore execution flags back into EF
	RET

; Set current bank index - INDEX(RA)
SET_BANKINDEX:
	LDR		R0, CF			; Copy control flags into R0
	AND		R0, 0xFF00		; Clear current bank index
	ORR		R0, RA			; Or bank index value into control flags
	SHL		R0, 8			; Shift value 8 bits into its correct location
	LDR		CF, R0			; Restore control flags back into EF
	RET

; Enable interrupt requests
ENABLE_IRQS:
	LDR 	R0, EF			; Copy execution flags into R0
	OR		R0, 0b10000		; Set interrupt enable bit
	LDR 	EF, R0			; Restore execution flags back into EF
	RET

; Disable interrupt requests
DISABLE_IRQS:
	LDR 	R0, EF			; Copy execution flags into R0
	AND		R0, 0b011111	; Clear interrupt enable bit
	LDR 	EF, R0
	RET

; Set interrupt descriptor table page index - INDEX(RA)
SET_IDTR:
	LDR 	R0, CF			; Copy control flags into R0
	AND		R0, 0xFF00		; Clear current interrupt descriptor page index
	ORR		R0, RA			; Or table page index into control flags
	LDR		CF, R0			; Restore control flags back into CF
	RET

SET_NEGATIVE:
	LDR 	R0, EF			; Copy execution flags into R0
	OR		R0, 0b100		; Clear negative bit
	LDR 	EF, R0			; Restore execution flags back into EF
	RET

UNSET_NEGATIVE:
	LDR 	R0, EF			; Copy execution flags into R0
	AND		R0, 0b011		; Clear negative bit
	LDR 	EF, R0			; Restore execution flags back into EF
	RET
	