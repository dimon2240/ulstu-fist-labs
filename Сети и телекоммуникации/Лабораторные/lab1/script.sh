#!/bin/bash

dialog --title 'Сети и телекоммуникации' --msgbox 'Лабораторная работа №1\nИспользование сетевых утилит ОС Windows для проверки и настройки локальной сети\nВыполнил Вершинин Д. В.' 10 50
 
TMPFCMD="/tmp/cmd.tmp"
TMPOUT="temp.txt"
INF=10000

dialog --title 'Сети и телекоммуникации' --menu 'Выберите команду для выполнения' 20 30 15 p ping i ipconfig t tracert a arp 2> $TMPFCMD

MENUSELECT=$?

CMD2RUN=$(cat $TMPFCMD)
 
while [ $INF -gt 0 ]
do
	if [ $MENUSELECT -eq "0" ]; then #если в меню не нажали отмену
		case "$CMD2RUN" in                #смотрим на что нажали
			"p")  ##########################################################################################PING############################################################
			TTL=""
			interval=""
			count=""
			destination=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "ping" \
				  --form "Укажите параметры для команды" \
			15 50 0 \
				"Count (-c) :" 1 1	"$count" 	1 20 8 0 \
				"TTL (-t):"    2 1	"$TTL"  	2 20 8 0 \
				"Interval (-i):"    3 1	"$interval"  	3 20 8 0 \
				"Destination:"     4 1	"$destination" 	4 20 20 0 \
			2>&1 1>&3)

			count=$(echo "$VALUES" | sed -n 1p)
			TTL=$(echo "$VALUES" | sed -n 2p)
			interval=$(echo "$VALUES" | sed -n 3p)
			destination=$(echo "$VALUES" | sed -n 4p)			
			
			# close fd
			exec 3>&-			
			
			dialog --title 'PING' --inputbox 'Здесь можно изменить параметры для команды ping' 10 40 "ping -c $count -t $TTL -i $interval $destination" 2> $TMPFCMD
			CMD2RUN=$(cat $TMPFCMD)
			if [ "$?" -ne "-1" ]; then
				{
					i=0
					while [ "$i" -lt "101" ]; do
					sleep $interval
					echo $i
					let "i += 100/$count"
					done; echo '100'; 
				} | dialog --title 'PING' --gauge 'Подождите окончания выполнения команды ...' 10 50 0
				$CMD2RUN > $TMPFCMD
				dialog --title 'Результат команды ping' --tailbox $TMPFCMD 25 60
				dialog --title 'Сети и телекоммуникации' --menu 'Выберите команду для выполнения' 20 30 15 p ping i ipconfig t tracert a arp 2> $TMPFCMD
				MENUSELECT=$?
				CMD2RUN=$(cat $TMPFCMD)
			else
				echo "Get -1 return code from dialog"
			fi
			;;
			"i")  ##########################################################################################IPCONFIG############################################################		
			
			dialog --title 'IPCONFIG' --inputbox 'ifconfig в линукс - аналог ipconfig из Windows. Здесь можно изменить параметры для команды ifconfig' 10 40 "ifconfig -s" 2> $TMPFCMD
			CMD2RUN=$(cat $TMPFCMD)
			if [ "$?" -ne "-1" ]; then
				{
					i=0
					while [ "$i" -lt "101" ]; do
					sleep 1
					echo $i
					let "i += 100/2"
					done; echo '100'; 
				} | dialog --title 'IPCONFIG' --gauge 'Подождите окончания выполнения команды ...' 10 50 0
				$CMD2RUN > $TMPFCMD
				dialog --title 'Результат команды ipconfig' --tailbox $TMPFCMD 25 60
				dialog --title 'Сети и телекоммуникации' --menu 'Выберите команду для выполнения' 20 30 15 p ping i ipconfig t tracert a arp 2> $TMPFCMD
				MENUSELECT=$?
				CMD2RUN=$(cat $TMPFCMD)
			else
				echo "Get -1 return code from dialog"
			fi
			;;
			"t") ###################################################################################################TRACERT###################################################
			MAXTTL=""
			FIRSTTTL=""
			nqueries=""
			destination=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "ping" \
				  --form "traceroute в linux - аналог команды tracert. Укажите параметры для команды" \
			15 50 0 \
				"MaxTTL (-m) :" 1 1	"$MAXTTL" 	1 20 8 0 \
				"FirstTTL (-f):"    2 1	"$FIRSTTTL"  	2 20 8 0 \
				"NQueries (-q):"    3 1	"$NQueries"  	3 20 8 0 \
				"Destination:"     4 1	"$destination" 	4 20 20 0 \
			2>&1 1>&3)

			MAXTTL=$(echo "$VALUES" | sed -n 1p)
			FIRSTTTL=$(echo "$VALUES" | sed -n 2p)
			nqueries=$(echo "$VALUES" | sed -n 3p)
			destination=$(echo "$VALUES" | sed -n 4p)			
			
			# close fd
			exec 3>&-			
			
			dialog --title 'TRACERT' --inputbox 'Здесь можно изменить параметры для команды tracerout' 10 40 "traceroute -m $MAXTTL -f $FIRSTTTL -q $nqueries $destination" 2> $TMPFCMD
			CMD2RUN=$(cat $TMPFCMD)
			if [ "$?" -ne "-1" ]; then
				{
					i=0
					while [ "$i" -lt "101" ]; do
					sleep 1
					echo $i
					let "i += 100/10"
					done; echo '100'; 
				} | dialog --title 'TRACERT' --gauge 'Подождите окончания выполнения команды ...' 10 50 0
				$CMD2RUN > $TMPFCMD
				dialog --title 'Результат команды tracerout' --tailbox $TMPFCMD 25 60
				dialog --title 'Сети и телекоммуникации' --menu 'Выберите команду для выполнения' 20 30 15 p ping i ipconfig t tracert a arp 2> $TMPFCMD
				MENUSELECT=$?
				CMD2RUN=$(cat $TMPFCMD)
			else
				echo "Get -1 return code from dialog"
			fi
			;;
			"a") ###################################################################################################ARP###################################################
			dialog --title 'PING' --inputbox 'Здесь можно изменить параметры для команды ping' 10 40 "arp -a" 2> $TMPFCMD
			CMD2RUN=$(cat $TMPFCMD)
			if [ "$?" -ne "-1" ]; then
				{
					i=0
					while [ "$i" -lt "101" ]; do
					sleep 1
					echo $i
					let "i += 100/2"
					done; echo '100'; 
				} | dialog --title 'ARP' --gauge 'Подождите окончания выполнения команды ...' 10 50 0
				$CMD2RUN > $TMPFCMD
				dialog --title 'Результат команды arp' --tailbox $TMPFCMD 25 60
				dialog --title 'Сети и телекоммуникации' --menu 'Выберите команду для выполнения' 20 30 15 p ping i ipconfig t tracert a arp 2> $TMPFCMD
				MENUSELECT=$?
				CMD2RUN=$(cat $TMPFCMD)
			else
				echo "Get -1 return code from dialog"
			fi
			;;
		esac
	else
		break
	fi
done
clear

rm -f $TMPFCMD