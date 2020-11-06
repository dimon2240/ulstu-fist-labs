<html>
<head>
	<title> лабораторная работа</title>
</head>
<body>
<H1>Вторая лабораторная работа</H1>
<p>Программа определяет свойства отношения заданного в виде бинарной матрицы размерностью nxn</p>
<form method="get" action="">
	<H4>Введите матрицу размерностью nxn</H4>
    <textarea name="matrix" class="str2" type="text" placeholder="Матрица" value="<?=($_GET['matrix'])?>" required></textarea>
	<br>
	<br>
    <input type="submit" value="Выполнить операцию">
</form>

<?php
	$arrStr = $_GET['matrix'];
	$arr = explode(" ", preg_replace('/[\s]{2,}/', ' ', $arrStr));
	
	$size = count($arr);
	
	$strSize = sqrt($size);
	
	function Reflexivity($array, $n, $strSize){
		for($i = 0; $i < $n; $i = $i + $strSize + 1){
			if ($array[$i]==0){
				echo "Отношение не рефлексивно <br>";
				return 0;
			}
		}
		echo "Отношение рефлексивно <br>";
	}
	
	function Symmetry($array, $strSize){
		$diag=0;
		
		for($k = 1;$k < $strSize;$k++){
			$counter=1;
			for($i = $diag+1; $i < ($strSize*$k); $i++){
					if($array[$i]!=$array[$diag+($strSize*$counter)]){
						echo "Отношение не симметрично <br>";
						return 0;
					}
				$counter++;
			}
			$diag = $diag + $strSize + 1;
		}
		echo "Отношение симметрично <br>";
	}
	
	function Transitivity($array, $strSize){
		$begin=0;
		$counter=0;
		$step=0;
		$size=$strSize*$strSize;
		
		for($l=0;$l<$size;$l++){
			$result[$l]=0;
		}
		
		for($j=0;$j<$size;$j++){
			for($i=$begin; $i<$begin+$strSize; $i++){
				$result[$counter]+=$array[$i]*$array[($step+$counter)];
				if($result[$counter]>0){
					$result[$counter]=1;
				}
				$step+=$strSize;
			}
			$step=0;
			$counter++;
			if((($counter % $strSize)==0) && $counter!=0){
				$begin+=$strSize;
			}
		}
		for($z=0;$z<$size;$z++){
			if($array[$z] < $result[$z]){
				echo "Отношение не транзитивно <br>";
				return 0;
			}
		}
		echo "Отношение транзитивно <br>";
	}
	
	function Lab3($array, $strSize){
		$counter=0;
		$begin=0;
		
		for($j=0;$j<$strSize;$j++){
			for($i=$begin;$i<($begin+$strSize);$i++){
				if($array[$i]==1){
					$counter++;
				}
			}
			if($counter != 1){
				echo"Данное отношение не является функцией<br>";
				return 0;				
			}
			$counter=0;
			$begin+=$strSize;
		}
		echo "Данное отношение является функцией";
	}
	
	for($i=0;$i<$size;$i++){
		if(($arr[$i]>1) || ($arr[$i]<0)){
			echo"Матрица должна быть бинарной, один из элементов матрицы не удовлетворяет условию";
			return 0;
		}
	}
	
	if(round($strSize)*round($strSize) == $size){
		Reflexivity($arr, $size, $strSize);
		Symmetry($arr, $strSize);
		Transitivity($arr, $strSize);
		Lab3($arr, $strSize);
	} else{
		echo "Матрица не квадратная, введите матрицу размерностью nxn";
	}
?>

</body>
</html>
