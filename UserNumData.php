<?php
require_once ("../g5/common.php");
require_once ('../api/config.php');
session_start();

// function print_values($arr){
//     global $count;
//     global $values;

//     if(!is_array($arr)){
//         die("ERROR: Input is not an array");
//     }

//     foreach($arr as $key=>$value){
//         if(is_array($value)){
//             print_values($value);
//         }else{
//             $values[] = $value;
//             $count++;
//         }
//     }
// }

$userid = "test1";

$num_type = $_POST["NumType"];
// $num_type = "mb_level";
// echo $num_type;

switch ($num_type) {
    case "mb_level":
        $sql = "SELECT mb_level FROM g5_member WHERE mb_id = '$userid'";
        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        // var_dump($result);
        echo $result[0]['mb_level'];
        break;
    case "mb_point":
        $sql = "SELECT mb_point FROM g5_member WHERE mb_id = '$userid'";
        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        echo $result[0]['mb_point'];
        break;
}



?>