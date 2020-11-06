<html>
<head>
	<title>Первая лабораторная работа</title>
</head>
<body>
<H1>Первая лабораторная работа</H1>
<p>Программа выполняет объединение, пересечение, дополнение и симметрическую разность двух множеств. Каждый элемент множества должен удовлетворять следующим условиям. Первый элемент множества цифра, второй - четная цифра, третий - нечетная цифра и четвертый - цифра. Элементы вводить через одинарный пробел.</p>
<form method="get" action="">
	<H4>Первое поле</H4>
    <input name="massA" class="str1" type="text" placeholder="Множество 1" value="<?=($_GET['massA'])?>" required>
	<br>
	<H4>Второе поле</H4>
    <input name="massB" class="str2" type="text" placeholder="Множество 2" value="<?=($_GET['massB'])?>" required>
	<br>
	<br>
    <input type="submit" value="Выполнить операцию">
</form>

<?php
	$massA_str = $_GET['massA'];
    $massA_num = explode(" ", preg_replace('/[\s]{2,}/', ' ', $massA_str));
	echo "<br>";
	
    $massB_str = $_GET['massB'];
    $massB_num = explode(" ", preg_replace('/[\s]{2,}/', ' ', $massB_str));
	echo "<br>";
	
	function Validation1($massA){
		$n=count($massA);
		
		for ($i = 0;$i < $n;$i++) {
			$col=0;
			$elem=$massA[$i];
			while($elem>0){
				$elem=floor($elem/10);
				$col++;
			}

			if($col!=4){
				echo '<font color="red"> Каждый элемент множества должен состоять из 4 цифр</font>';
				return false;
			}
		}
		
		for ($i = 0;$i < $n;$i++) {
			 for ($j = $i+1;$j < $n;$j++) {
				 if($massA[$i]==$massA[$j]){
					for ($k = $j;$k < $n;$k++) {
						$massA[$k]=$massA[$k+1];
					}
					$n--;
					$i=-1;
				 }
			 }
		}
		
		for ($i = 0;$i < $n;$i++){
			if(is_numeric(($massA[$i])/1000)==true and (($massA[$i]/100)%10)%2==0 and (($massA[$i]/10)%10)%2==1 and is_numeric($massA[$i]%10)==true){
					$finmas[$i]=$massA[$i];
				}else {
					echo '<font color="red"> Один из элементов массива не удовлетворяет условию задачи</font>';
					return false;
				}
		}
		
		return $finmas;
	}
	
	
	function Union($massA,$massB){
		$n1=count($massA);
		$n2=count($massB);
		$sum=$n1+$n2;
        for ($i = 0;$i < $n1;$i++) {
            $result[$i] = $massA[$i];
        }
		$k=0;
		for ($j = $n1;$j < $sum;$j++) {
            $result[$j] = $massB[$k];
			$k++;
        }
		$z=0;
		$n=count($result);
		
		for ($i = 0;$i < $n;$i++) {
			 for ($j = $i+1;$j < $n;$j++) {
				 if($result[$i]==$result[$j]){
					for ($k = $j;$k < $n-1;$k++) {
						$result[$k]=$result[$k+1];
					}
					$z++;
					$n--;
				 }
			 }
		}
		for ($i = 0;$i < $sum-$z;$i++) {
			echo $result[$i]," ";
		}
		return true;
	}
	
	
	
	function Intersection($massA,$massB){
		$n1=count($massA);
		$n2=count($massB);
		$sum=$n1+$n2;
		for ($i = 0;$i < $n1;$i++) {
			 for ($j = 0;$j < $n2;$j++) {
				if($massA[$i]==$massB[$j]){
					$result[$i]=$massA[$i];
				}
			 }
		}
		for ($i = 0;$i < $sum;$i++) {
			echo $result[$i]," ";
		}
		return true;
	}
	
	
	
	function Addition($massA, $massB){
		$n1 = count($massA);
		$n2 = count($massB);
		$sum=$n1+$n2;
		for ($i = 0;$i < $n1;$i++) {
			$result[$i]=$massA[$i];
		}
		for ($i = 0;$i < $n1;$i++) {
			 for ($j = 0;$j < $n2;$j++) {
				if($result[$i]==$massB[$j]){
					$result[$i]='';
				}
			 }
		}
		for ($i = 0;$i < $sum;$i++) {
			echo $result[$i]," ";
		}
	}
	
	if(Validation1($massA_num)==false){
		echo '<style> INPUT.str1{border: 4px solid #F00;}</style>'; 
	}else{
		echo '<style> INPUT.str1{border: 4px solid #0F0;}</style>';
		if(Validation1($massB_num)==false){
		echo '<style> INPUT.str2{border: 4px solid #F00;}</style>'; 
	}else{
			echo '<style> INPUT.str2{border: 4px solid #0F0;}</style>'; 
			$massivA=Validation1($massA_num);
			$massivB=Validation1($massB_num);
			$n1 = count($massivA);
			$n2 = count($massivB);
			$sum=$n1+$n2;
			echo "Множество А: ";
			for ($i = 0;$i < $n1;$i++) {
				echo $massivA[$i]," ";
			}
			echo "<br>";
			echo "Множество B: ";
			for ($i = 0;$i < $n2;$i++) {
				echo $massivB[$i]," ";
			}
			echo "<br>";
			echo "Объединение множеств: ";
			Union($massivA,$massivB);
			echo "<br>";
			echo "Пересечение множеств: ";
			Intersection($massivA,$massivB);
			echo "<br>";
			echo "Дополнение множеств (A/B): ";
			Addition($massivA,$massivB);
			echo "<br>";
			echo "Дополнение множеств (B/A): ";
			Addition($massivB,$massivA);
			echo "<br>";
			echo "Симметрическая разность множеств: ", Addition($massivA,$massivB)." ".Addition($massivB,$massivA);
		}
	}
?>

</body>
</html>
