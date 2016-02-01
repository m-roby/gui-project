#!/bin/bash

###This Script is designed to pull information about the number of POS and kitchen devices on the stores network as well as their names, then save this information in JSON format
##does not support QSR EPIC Devices



rm /tmp/KitchenDevices.txt 2>/dev/null
rm /tmp/d.txt 2>/dev/null
##rm /media/usb/GUI_Project/Linux_Build/UI_Data/Resources/SystemInfo.json 2>/dev/null
rm /tmp/json.txt 2>/dev/null


##find the number POS devices

	Counter=1
	Old_Terminal_Name=""
	Terminal_Name=$(phtbpr tdev 2>/dev/null |grep "ttyp" |sed -e 's/Device Path : //' |sed -e 's/[/dev]//g' |sed -r 's/\s+//g' |head -$Counter |tail -1)

		until  [ "$Terminal_Name" = "$Old_Terminal_Name" ]; do 
			Old_Terminal_Name=$(phtbpr tdev 2>/dev/null |grep "ttyp" |sed -e 's/Device Path : //' |sed -e 's/[/dev]//g' |sed -r 's/\s+//g' |head -$Counter |tail -1)
			let Counter+=1
			Terminal_Name=$(phtbpr tdev 2>/dev/null |grep "ttyp" |sed -e 's/Device Path : //' |sed -e 's/[/dev]//g' |sed -r 's/\s+//g' |head -$Counter |tail -1)
		done

	Number_of_terminals=$((${Counter} - 1)) 


##find their names
	Counter=1
	
		for (( i = 0; i < ${Number_of_terminals}; ++i )); do
			Terminal_Name=$(phtbpr tdev 2>/dev/null |grep "ttyp" |sed -e 's/Device Path : //' |sed -e 's/[/dev]//g' |sed -r 's/\s+//g' |head -$Counter |tail -1)
			Terminal[i]="$Terminal_Name"
			let Counter+=1
		done

	
	


##Find the number of kitchen devices
	
	Counter=1
	Old_Kitchen_Name=""
	Kitchen_Name=$(phtbpr devt 2>/dev/null |grep "Description" |sed -e 's/Description : //' |sed -e 's/^[ \t]*//' |head -$Counter |tail -1)
	
		until  [ "$Kitchen_Name" = "$Old_Kitchen_Name" ]; do 
				echo $Kitchen_Name >>/tmp/d.txt
				Old_Kitchen_Name=$(phtbpr devt 2>/dev/null |grep "Description" |sed -e 's/Description : //' |sed -e 's/^[ \t]*//' |head -$Counter |tail -1)
				let Counter+=1
				Kitchen_Name=$(phtbpr devt 2>/dev/null |grep "Description" |sed -e 's/Description : //' |sed -e 's/^[ \t]*//' |head -$Counter |tail -1)
		done
		
##remove non-essential items from the list


		cat /tmp/d.txt |sed '/Report Printer/d' |sed '/Ops Metrics Daemon/d' |sed '/KMX Master Process/d' |sed '/Future DAEMON/d'  >>/tmp/KitchenDevices.txt
		


##find the new number


		
	Counter=1
	Old_Kitchen_Name=""
	Kitchen_Name=$(cat /tmp/KitchenDevices.txt |head -$Counter |tail -1)
	
		until  [ "$Kitchen_Name" = "$Old_Kitchen_Name" ]; do 
				Old_Kitchen_Name=$(cat /tmp/KitchenDevices.txt |head -$Counter |tail -1)
				let Counter+=1
				Kitchen_Name=$(cat /tmp/KitchenDevices.txt |head -$Counter |tail -1)
		done
	

	Number_of_Kitchen_Devices=$((${Counter} - 1))
	
	
	
	
	
		Counter=1
	
		for (( i = 0; i < ${Number_of_Kitchen_Devices}; ++i )); do
			Kitchen_Name=$(phtbpr devt 2>/dev/null |grep "Description" |sed -e 's/Description : //' |sed -e 's/^[ \t]*//' |head -$Counter |tail -1)
			Kitchen[i]="$Kitchen_Name"
			let Counter+=1
		done
		
	#echo "${Kitchen[@]}"
	#echo "${Kitchen[1]}"
	
	
	
	
## set up the formatting for JSON output
	
	Object_Open="{"
	Object_Close="}"
	
	Array_Open="["
	Array_Close="]"
	
	Colon=":"
	
	Comma=","

	
	Main_Header="\"Type\""
	Sub_Header_1="\"POS\""
	Sub_Header_2="\"Kitchen\""
	
	Error_Text="Unable to aquire"
	
	##JSON_File="/media/usb/GUI_Project/Linux_Build/UI_Data/Resources/SystemInfo.json"
	JSON_File="/tmp/json.txt"
	
	echo "$Object_Open" >> $JSON_File							##top of the JSON file
	echo "$Sub_Header_1 $Colon $Array_Open" >> $JSON_File
	
	
	
		Counter=0
		Last_Item=$(( ${Number_of_terminals} - 1 ))
	
		for (( i = 0; i < ${Number_of_terminals}; ++i )); do
				Terminal_Name=${Terminal[${i}]}
				IP=$(netptyadmin -s |grep "$Terminal_Name" |awk '{print $3}')
				TTY=$(netptyadmin -s |grep "$Terminal_Name" |awk '{print $1}')
				MAC=$(cat /etc/dhcpd.conf |grep -ai "$TTY" -a1 |tail -1 |sed -e 's/hardware ethernet//' |sed -e 's/;//' |sed -e 's/^[ \t]*//' )
				Connectivity=$(ping -c 5 $IP)
				
				
				
				echo "$Object_Open">> $JSON_File
				echo "\"Name\"$Colon\"$Terminal_Name\"$Comma" >> $JSON_File
				echo "\"IP Address\"$Colon\"$IP\"$Comma" >> $JSON_File
				echo "\"MAC Address\"$Colon\"$MAC\"$Comma" >> $JSON_File
				
				
				
				
				Packet_Loss=$(grep -ai "packet" <<<"${Connectivity}" |awk '{print $6}' |sed -e 's/[%]//g')
				Failure=$(grep -ai "packet" <<<"${Connectivity}" |awk '{print $6}' |sed -e 's/[%]//g' |grep "+")
				
				if [ ! -z "$Failure" ]; then 
				Failures=$(sed -e 's/[+]//g' <<<"${Failure}")
				Packet_Loss=$(($Failures * 20))
				fi
				
				if [ "$Packet_Loss" -lt "60" ] && [ -z "$Failure"]; then
				
					Uptime=$(phzap ssh $IP uptime |awk '{print $3,$4,$5}')
					echo "\"Uptime\"$Colon\"$Uptime\"$Comma" >> $JSON_File
					
					Run_Level=$(phzap ssh $IP who -r |awk '{print $2}')
					echo "\"Run Level\"$Colon\"$Run_Level\"$Comma" >> $JSON_File
					
					USB=$(phzap ssh $IP lsusb |grep -ai epson |awk '{print $7,$8,$9,$10,$11,$12}')
					Serial=$(phzap ssh $IP cat /proc/tty/driver/serial |grep "CTS|DSR" |awk '{print $7}')
					Port=$(phzap ssh $IP cat /proc/tty/driver/serial |grep "CTS|DSR" |awk '{print $1}' |sed -e 's/://')
					
					
					TerminalInfo=$(/appl/fms/etc/syssus phtbpr tdev 2>/dev/null |grep $Terminal_Name -a8 |tail -9)
					DevNode=$(grep -ai "Dev Node" <<<"${TerminalInfo}" |sed -e 's/Rcpt Prntr Dev Node ://' |sed -e 's/^[ \t]*//' |sed -r 's/\s+//g')
							if [ "$DevNode" = "" ]; then 
								DevNode="None"
							fi
					simlink=$(phzap ssh $IP ls -alF /dev/rcprinter |awk '{print $11}')

					
					Printer_Configured=$(grep -ai "Recept Location? RLN" <<<"${TerminalInfo}" |sed -e 's/Recept Location? RLN ://' |sed -e 's/^[ \t]*//')
					Printer_Configured_YN=""
					
					
						if [ "$Printer_Configured" = "L" ]; then 
							Printer_Configured_YN="Yes"
						else
							Printer_Configured_YN="No"
						fi
						
					Drawer=$(grep -ai "Drawer Location? RLN" <<<"${TerminalInfo}" |sed -e 's/Drawer Location? RLN ://' |sed -e 's/^[ \t]*//')
					Drawer_YN=""	
					
						if [ "$Drawer" = "L" ]; then 
							Drawer_YN="Yes"
						else
							Drawer_YN="No"
						fi
					
					Printer_Attached=""
					
						if [ ! -z "$USB" ]; then 
							Printer_Attached="USB: $USB"
						fi
						
						if [ ! -z "$Serial" ]; then 
							Printer_Attached="Serial: On Port $Port"
						fi
						
						if [ -z "$USB" ] && [ -z "$Serial" ]; then
							Printer_Attached="None"
						fi
					
					MOTHERBOARD=$(phzap ssh $IP dmidecode -t 2 |grep -ai "Product Name" |sed -e 's/Product Name: //' | tr '[:lower:]' '[:upper:]' |sed -e 's/^[ \t]*//')
					MOTHERBOARD_MFG=$(phzap ssh $IP dmidecode -t 2 |grep -ai Manufacturer |sed -e 's/Manufacturer: //' | tr '[:lower:]' '[:upper:]' |sed -e 's/^[ \t]*//' )
					
					MON[0]="microtouch"
					MON[1]="elo touch"
					MON[2]="eGalax"
					MON[3]="Wincor"
					#MON[4]=
					#MON[5]=
					#MON[6]=
					
					TouchFlag=""

					COUNT=0
					END=4
					

					MONITOR=""
					
					if [ -z "$MONITOR" ]; then
						while [ "$COUNT" -ne "$END" ] && [ -z "$MONITOR" ] ; do
							MONITOR=$(phzap ssh $IP lsusb |grep -ai "${MON["$COUNT"]}" -m1 |awk '{print $7,$8,$9,$10,$11,$12}')
								let COUNT+=1
						done
					fi
					
					if [ -z "$MONITOR" ]; then
						MONITOR="No Touch Screen Detected"
					fi
					
					
					IO_Error=$(phzap ssh $IP dmesg |grep -ai "I/O error")
					IO_Text=""
					
						if [ -z "$IO_Error"]; then 
							IO_Text="No Issues Detected"
						else
							IO_Text="IO Errors Detected"
						fi
					
					
					Printer_Port_Error=$(phzap ssh $IP dmesg|tail -5 |grep -ai "on fire")
					Printer_Paper_Error=$(phzap ssh $IP dmesg|tail -5 |grep -ai "paper")
					
						if [ -z "$Printer_Port_Error" ] && [ -z "$Printer_Paper_Error" ]; then
						Printer_Error_Text="No Issues Detected"
						fi

					
						if [ ! -z "$Printer_Port_Error" ]; then
							Printer_Error_Text="Printer Port Error Detected"
						fi
						
						if [ ! -z "$Printer_Paper_Error" ]; then
							Printer_Error_Text="Printer out of Paper"
						fi
					
					echo "\"Packet Loss\"$Colon\"$Packet_Loss\"$Comma" >> $JSON_File
					echo "\"Motherboard\"$Colon\"$MOTHERBOARD\"$Comma" >> $JSON_File
					echo "\"Motherboard Manufacturer\"$Colon\"$MOTHERBOARD_MFG\"$Comma" >> $JSON_File
					echo "\"Printer Configured\"$Colon\"$Printer_Configured_YN\"$Comma" >> $JSON_File
					echo "\"Printer Device Node\"$Colon\"$DevNode\"$Comma" >> $JSON_File
					echo "\"Printer Attached\"$Colon\"$Printer_Attached\"$Comma" >> $JSON_File
					echo "\"Printer SimLink\"$Colon\"$simlink\"$Comma" >> $JSON_File
					echo "\"Cash Drawer Configured\"$Colon\"$Drawer_YN\"$Comma" >> $JSON_File
					echo "\"Touch Screen\"$Colon\"$MONITOR\"$Comma" >> $JSON_File
					echo "\"Drive Health\"$Colon\"$IO_Text\"$Comma" >> $JSON_File
					echo "\"Printer Errors\"$Colon\"$Printer_Error_Text\"" >> $JSON_File
						
						
					
				else
					echo "\"Uptime\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Run Level\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Packet Loss\"$Colon\"$Packet_Loss\"$Comma" >> $JSON_File
					echo "\"Motherboard\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Motherboard Manufacturer\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Printer Configured\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Printer Device Node\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Printer Attached\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Printer SimLink\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Cash Drawer Configured\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Touch Screen\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Drive Health\"$Colon\"$Error_Text\"$Comma" >> $JSON_File
					echo "\"Printer Errors\"$Colon\"$Error_Text\"" >> $JSON_File
				
				fi
					
					
					
				
				
				
					if [ "$Counter" != "$Last_Item" ]; then
							echo "$Object_Close$Comma" >> $JSON_File
						else
							echo "$Object_Close" >> $JSON_File
					fi
					
				let Counter+=1
			
		done
		
		
		
		
		
		
		
		
		
		
			
			echo "$Array_Close$Comma" >> $JSON_File
			echo "$Sub_Header_2 $Colon $Array_Open" >> $JSON_File
		
		
		
		
		
		
		
		
		
		
		Counter=0
		Last_Item=$(( ${Number_of_Kitchen_Devices} - 1 ))

		
		for (( i = 0; i < ${Number_of_Kitchen_Devices}; ++i )); do
				Kitchen_Name=${Kitchen[${i}]}
				Kitchen_Address=$(phtbpr devt 2> /dev/null |grep -ai "$Kitchen_Name" -a1 |head -1 |sed -e 's/Device Address : //' |sed -e 's/^[ \t]*//')
				Virtual_Device1=$(phtbpr devt 2> /dev/null |grep -ai "$Kitchen_Name" -a7 |grep "Device1" |sed -e 's/Device1 (Required) ://' |sed -e 's/^[ \t]*//')
				Virtual_Device2=$(phtbpr devt 2> /dev/null |grep -ai "$Kitchen_Name" -a7 |grep "Device2" |sed -e 's/Device2 (Optional) ://' |sed -e 's/^[ \t]*//')
						
						if [ -z "$Virtual_Device2" ]; then 
							Virtual_Device2="None"
						fi
						
				Phstatus=$(phstatus |grep "$Virtual_Device1" |tail -1 |cut -c30-33)
				
						
						
						
				echo "$Object_Open">> $JSON_File
				echo "\"Name\"$Colon\"$Kitchen_Name\"$Comma" >> $JSON_File
				echo "\"Address\"$Colon\"$Kitchen_Address\"$Comma" >> $JSON_File
				echo "\"Virtual Device 1\"$Colon\"$Virtual_Device1\"$Comma" >> $JSON_File
				echo "\"Virtual Device 2\"$Colon\"$Virtual_Device2\"$Comma" >> $JSON_File
				echo "\"Status\"$Colon\"$Phstatus\"$Comma" >> $JSON_File
				
				
				IP_Address=$(sed -e 's/[/dev/ttyp]//g' <<<"${Kitchen_Address}")
					if [ "$IP_Address" -gt 89 ]; then
						New_IP="10.1.2.$IP_Address"
						Connectivity=$(ping -c 5 $New_IP)
						Failure=$(grep -ai "packet" <<<"${Connectivity}" |awk '{print $6}' |sed -e 's/[%]//g' |grep "+")
						Packet_Loss=$(grep -ai "packet" <<<"${Connectivity}" |awk '{print $6}' |sed -e 's/[%]//g')
						
					else
					
						Packet_Loss="Non-IP Device"
					fi
					
					
					if [ "$IP_Address" -gt 99 ]; then 
					
						if [ "$Packet_Loss" -lt "60" ] && [ -z "$Failure"]; then 
							BumpBar=$(phzap ssh $New_IP lsusb |grep -ai "Logic Controls" |awk '{print $7,$8,$9}')
						else
							BumpBar="$Error_Text"
							Packet_Loss="$Error_Text"
						fi
						
					else
						BumpBar="Non-bump device"
					fi
					
					
					if [ -z "$BumpBar" ]; then 
						Bump="None"
					fi
					
				
				echo "\"Packet Loss\"$Colon\"$Packet_Loss\"$Comma" >> $JSON_File
				echo "\"Bump Bar\"$Colon\"$BumpBar\"" >> $JSON_File
				

				
					if [ "$Counter" != "$Last_Item" ]; then
							echo "$Object_Close$Comma" >> $JSON_File
						else
							echo "$Object_Close" >> $JSON_File
					fi
				let Counter+=1
		done
			
			
			
			
			
			
			
			
			
	echo "$Array_Close" >> $JSON_File
	
	echo "$Object_Close" >> $JSON_File   ###end of the JSON file
