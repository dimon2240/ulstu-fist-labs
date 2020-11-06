#!/bin/bash

dialog --title 'Информационная безопасность' --msgbox 'Лабораторная работа №1\nТестирование псевдослучайных последовательностей\nВыполнил Вершинин Д. В.' 10 50
 
TMPFCMD="/tmp/cmd.tmp"
TMPOUT="temp.txt"
INF=10000

dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD

MENUSELECT=$?

CMD2RUN=$(cat $TMPFCMD)
 
while [ $INF -gt 0 ]
do
	if [ $MENUSELECT -eq "0" ]; then #если в меню не нажали отмену
		case "$CMD2RUN" in                #смотрим на что нажали
			"g1")	#################################################################################################STD GENERATOR
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите длину последовательности" \
			15 50 0 \
				"Length :" 1 1	"$length" 	1 20 8 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/generator.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
			"g2")	##########################################################################################Конгруентный генератор
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите длину последовательности" \
			15 50 0 \
				"Length :" 1 1	"$length" 	1 20 8 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/generatorCubeCongr.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
			"g3")	##########################################################################################Аддитивный генератор
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите длину последовательности" \
			15 50 0 \
				"Length :" 1 1	"$length" 	1 20 8 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/generatorAdditive.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
			"g4")	##########################################################################################RSA
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите длину последовательности" \
			15 50 0 \
				"Length :" 1 1	"$length" 	1 20 8 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/generatorRSA.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
			"g5")	##########################################################################################lab3
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите параметр -s для шифрования и -d для дешифрования, имя файла с сообщением, имя файла для вывода и пароль" \
			15 50 0 \
				"Length :" 1 1	"$length" 	1 20 50 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/lab3.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
			"g6")	##########################################################################################lab4
			length=""
			
			exec 3>&1
			
			# Store data to $VALUES variable
			VALUES=$(dialog --ok-label "Submit" \
				  --title "Информационная безопасность" \
				  --form "Укажите параметр -s для шифрования и -d для дешифрования, имя файла с сообщением, имя файла для вывода и пароль" \
			15 50 0 \
				"Input :" 1 1	"$length" 	1 20 50 0 \
			2>&1 1>&3)

			length=$(echo "$VALUES" | sed -n 1p)
			
			# close fd
			exec 3>&-
			
			python3 /home/dmitriy/PycharmProjects/inf_security/venv/lab4.py $length > $TMPFCMD
			dialog --title 'Результат генерации и тестировния' --tailbox $TMPFCMD 25 60
			dialog --title 'Информационная безопасность' --menu 'Выберите команду для выполнения' 20 30 15 g1 generate g2 Kongruent g3 Additive g4 RSA g5 lab3 g6 lab4 2> $TMPFCMD
			MENUSELECT=$?
			CMD2RUN=$(cat $TMPFCMD)
			;;
		esac
	else
		break
	fi
done
clear

rm -f $TMPFCMD