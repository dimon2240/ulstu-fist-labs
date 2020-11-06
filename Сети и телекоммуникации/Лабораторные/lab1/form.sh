#!/bin/bash
# useradd1.sh - A simple shell script to display the form dialog on screen
# set field names i.e. shell variables
TTL=""
			interval=""
count=""
destination=""

user_record=$(\
dialog                                             \
--separate-widget $'\n'                            \
--title "INSERIR"                                  \
--form ""                                          \
0 0 0                                              \
"Nome:"     1 1 "$nome"     1 10 30 0              \
"Morada:"       2 1     "$morada"       2 10 30 0  \
"Telefone:"     3 1     "$telefone" 3 10 30 0      \
"E-Mail:"       4 1     "$mail"         4 10 30 0  \
  3>&1 1>&2 2>&3 3>&- \
)
nome=$(echo "$user_record" | sed -n 1p)
morada=$(echo "$user_record" | sed -n 2p)
telefone=$(echo "$user_record" | sed -n 3p)
mail=$(echo "$user_record" | sed -n 4p)

echo $nome
echo $morada
echo $telefone
echo $mail