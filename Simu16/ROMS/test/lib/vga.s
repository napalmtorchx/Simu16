INCLUDE "lib/system.s"

; VGA Controller Ports
DEFINE VGA_PORT_CMD    0x00D0		; Video command port
DEFINE VGA_PORT_DATA   0x00D1		; Video data port
DEFINE VGA_PORT_COLOR  0x00D2		; Video foreground and background port
DEFINE VGA_PORT_CURSOR 0x00D3		; Video cursor position port
DEFINE VGA_PORT_SIZE   0x00D4		; Video width and height port

; VGA Mode Information
DEFINE VGA_WIDTH  80		; Default video width
DEFINE VGA_HEIGHT 25		; Default video height
DEFINE VGA_SIZE   0x0FA0	; Default video buffer size
DEFINE VGA_ADDR   0x0000	; Video buffer address

; VGA Commands
DEFINE VGA_CMD_CLEAR	0x01		; Clear the screen
DEFINE VGA_CMD_PIXEL	0x02		; Draw pixel to position on screen
DEFINE VGA_CMD_FILL		0x03		; Fill rectangle on screen
DEFINE VGA_CMD_RECT		0x04		; Draw rectangle outline on screen
DEFINE VGA_CMD_CHAR		0x05 		; Draw character on screen
DEFINE VGA_CMD_STRING	0x06 		; Draw string on screen
DEFINE VGA_CMD_PRINTC	0x07		; Print character to the screen
DEFINE VGA_CMD_PRINTSTR	0x08		; Print string at address to the screen
DEFINE VGA_CMD_PRINTDEC	0x09		; Print decimal value to the screen
DEFINE VGA_CMD_PRINTHEX	0x0A		; Print hexadecimal value to the screen
DEFINE VGA_CMD_NEWLINE  0x0B		; Print newline on screen
DEFINE VGA_CMD_SCROLL	0x0C		; Scroll one line on screen
DEFINE VGA_CMD_SETPOS   0x0D		; Set cursor position
DEFINE VGA_CMD_SETMODE	0x0E		; Set video Mode
DEFINE VGA_CMD_GETMODE	0x0F		; Get current video Mode

; Initialize the screen
SCREEN_INIT:
	LD 		RC, 0000
	OUT		VGA_PORT_CURSOR, RC

	LD 		RC, 0x0E01
	JS 		SCREEN_CLEAR
	
	RET

SCREEN_NEWLINE:
	LD 		R9, VGA_CMD_NEWLINE
	OUT 	VGA_PORT_CMD, R9
	RET

; Clear the screen - STACK(bg)	
SCREEN_CLEAR:
	LD		R9, VGA_CMD_CLEAR			; Load clear command into R9
	OUT		VGA_PORT_COLOR, RC			; Send value in R0 to color port
	OUT 	VGA_PORT_CMD,  R9			; Send clear to command port
	RET
	
SCREEN_PRINTC:
	RET

SCREEN_PRINT:
	LD 		R9, VGA_CMD_PRINTSTR
	OUT		VGA_PORT_DATA, RS
	OUT		VGA_PORT_CMD,  R9
	RET
	
SCREEN_PRINTDEC:
	LD		R9, VGA_CMD_PRINTDEC
	OUT		VGA_PORT_DATA, RC
	OUT		VGA_PORT_CMD, R9
	RET
	