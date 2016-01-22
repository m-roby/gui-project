#!/bin/bash


STARTUP () {

rm /media/usb/GUI_Project/Linux_Build/UI_Data/Script_Output.txt 2> /dev/null
rm /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt 2> /dev/null
rm /media/usb/GUI_Project/Linux_Build/UI_Data/Store_Number.txt 2> /dev/null

echo "Done" > /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt
echo " " > /media/usb/GUI_Project/Linux_Build/UI_Data/Script_Output.txt


VAR=$( cat /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt |tail -1 |awk '{print $1}')
Cache_File="/media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt"
Script_Output="/media/usb/GUI_Project/Linux_Build/UI_Data/Script_Output.txt"

}

GET_STORE_NUMBER () {

SN=$(phtbpr unit |head -10 |grep "6 Dig" |awk '{print $6}')

echo $SN > /media/usb/GUI_Project/Linux_Build/UI_Data/Store_Number.txt

}

CHECK_FOR_NEW_COMMAND () {
		until [ "$VAR" != "Done" ]; do
		VAR=$( cat /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt |tail -1 |awk '{print $1}') 2> /dev/null
		cat /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt
		sleep 1
		done
		
		INTERPRET_COMMAND
	}

INTERPRET_COMMAND () {

		if [ "$VAR" = "RefreshTerminals" ]; then
		bash -x /media/usb/GUI_Project/Linux_Build/Scripts/HWInfo_To_Json_LinuxBuild.bash &
		wait
		echo "Done" >> $Cache_File
		CHECK_FOR_COMPLETED_COMMAND
		fi
		
		
		if [ "$VAR" = "RefreshKitchen" ]; then
		/usr/fms/op/bin/phprint.s > $Script_Output &
		wait
		echo "Done" >> $Cache_File
		CHECK_FOR_COMPLETED_COMMAND
		fi

		


}

CHECK_FOR_COMPLETED_COMMAND () {

		until [ "$VAR" = "Done" ]; do
		VAR=$( cat /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt |tail -1 |awk '{print $1}') 2> /dev/null
		cat /media/usb/GUI_Project/Linux_Build/UI_Data/Command_Cache.txt
		sleep 1
		done
		
		CHECK_FOR_NEW_COMMAND 

}

STARTUP
GET_STORE_NUMBER
CHECK_FOR_NEW_COMMAND 
