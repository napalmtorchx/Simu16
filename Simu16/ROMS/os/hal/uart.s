
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