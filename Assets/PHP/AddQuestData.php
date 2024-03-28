<?php
require_once("../g5/common.php");
require_once('../api/config.php');
session_start();

function print_values($arr){
    global $count;
    global $values;

    if(!is_array($arr)){
        die("ERROR: Input is not an array");
    }

    foreach($arr as $key=>$value){
        if(is_array($value)){
            print_values($value);
        }else{
            $values[] = $value;
            $count++;
        }
    }
}

//$userid = $_POST['userid'];
$userid = "test1";
$quest_id = $_POST['quest_id'];
$cond_num = $_POST['cond_num'];
$completed = $_POST['completed'];

$sql = "SELECT * FROM yj_mb_quest WHERE mb_id = '$userid' AND quest_id = '$quest_id'";
$stmt = $pdo -> prepare($sql);
$stmt -> execute();
$result = $stmt -> fetchAll(PDO::FETCH_ASSOC);

if($result!=null)
{
    $sql = "UPDATE yj_mb_quest SET cond_num = $cond_num, completed = $completed WHERE mb_id='$userid' AND quest_id = '$quest_id'";
}
else{
     $sql = "INSERT INTO yj_mb_quest (mb_id, quest_id, cond_num, completed) VALUES ('$userid','$quest_id', '$cond_num', '$completed')";
}
$stmt = $pdo -> prepare($sql);
$stmt -> execute();




?>