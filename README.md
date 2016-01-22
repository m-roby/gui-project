# gui-project

Read Me!


Self-Help GUI system V0.1

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
*** Works on KMX Kitchens ONLY (for now)
*** Supported terminals: Fusion, Beetle, VXL, Everserv 500  & 2000, Optiplex XE (others will show up as a generic image)(for now)
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
FEATURES STILL ABSENT:

-- Troubleshooting pictures and Text not (ALL) added yet
-- help with devices that have not triggered an error not added yet
-- open/close remedy tickets automatically not added yet

Features Added:

-- Native build to Ubuntu Image
-- Dynamic creation of GUI
-- Icon pictures relect installed terminals
-- Text Ticket Creation
-- Handshake with Linux before running commands via Command_Cache.txt
-- user created tickets for terminals not triggering errors.
-- running commands
-- 5 functioning troubleshooting guide w/ text ticket creation

IssuesWith Full TS Steps Implemented:

printer out of paper, 
printer configured but not attached,
printer port errors,
touch screen not attached
Kitchen device status is not "Live"

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




//////////
How to Use:
//////////

**currently all scripts are set up to be run off of a flashdrive that is mounted to /media/usb
**load the project onto a flashdrive so that the file hierarchy looks like this:

/GUI_Project/Linux_Build/

that way when mounted the directopries will follow the /media/usb/GUI_Project/Linux_Build/UI_Data... conventions.


Windows: Run UI.exe
	(Note, running commands and refreshing terminal configurations not supported on windows)

Ubuntu: 

	Step 1: run /Scripts/Command_Cache_Checker.bash
	Step 2: run startx, open xterm window, navigate to folder containing UI.x86 and do ./UI.x86
	Step 3: from the launcher, choose your desired window resolution and if windowed or fullscreen
	step 4: click "Refresh Terminal Info" button in top right corner of UI


NO JSON files pre-loaded on Ubuntu, GUI can now generate its own =]



///////////////////////////
Troubleshooting info
///////////////////////////

	-Logfiles may be created by using ./UI.x86 -logfile log.txt to start the UI	
	
	-Logfiles are stored in the same directory as the UI.x86 application

	-Command_Cache.txt may slow down performance if the file size gets too large

	-if command cache polling script is not running, UI will not execute scipts, script must be running to tell UI that all cached jobs have finished processing.
		if for some reason the you have to force this file to say that a task has completed, do it by appending "Done" as the last line of the file 
		or deleting it entirely and restarting the UI.
	
	-if UI crashes to desktop, check the logfile. If a shader failing to compile error is present or OpenGL version is not supported, try executing as ./UI.x86 -force -opengl
		this forces the program to run on the most basic OpenGL version Available

	-Xorg.conf configuration can cause screen to be off center if not configured properly. Delete the bad configuration if necessary

	-GUI will not be able to run commands in Command_Cache_Checker.bash stops looping, this will happen if the last line of Command_Cache.txt doses not meet the critera of the script.
		if you are experiencing this issue, append "Done" as the last line of the file, or delete it and recreate it.

	-Tickets will not save if the folder /Ui_Data/Tickets does not exist. IF you are unable to submit tickets, make sure this folder exists.

	- Tickets will not recieve the output information from scripts if /UI_Data/Script_Output.txt does not exist
 

	
	



