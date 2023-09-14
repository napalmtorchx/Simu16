; Interrupt request identifiers
DEFINE IRQ_KEYBOARD 0x00
DEFINE IRQ_TIMER    0x01

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
	