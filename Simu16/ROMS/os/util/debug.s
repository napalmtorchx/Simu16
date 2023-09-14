INCLUDE "hal/uart.s"
INCLUDE "common.s"

; Write character to serial debugger - CHAR(RA)
DEBUG_PUTC:
	LD  	R1, UART_PUTC			; Load UART PUTC command into R1
	OUT 	UART_COM1_CMD,  R1		; Send command to serial port
	OUT 	UART_COM1_DATA, RA		; Send character value to serial port
	RET		

; Write string to serial debugger - STRING(RS)
DEBUG_PRINT:
	LD 		R1, UART_PRINT
	OUT 	UART_COM1_CMD,  R1
	OUT 	UART_COM1_DATA, RS
	RET
	
; Write integer value to serial debugger - VALUE(RA)
DEBUG_PRINT_INT:
	LD 		R1, UART_PRINT_INT
	OUT 	UART_COM1_CMD,  R1
	OUT 	UART_COM1_DATA, RA
	RET
	