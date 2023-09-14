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