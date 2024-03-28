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
//$userid = trim($_POST['userid']);
$num_type = $_POST["NumType"];

// $sql = "SELECT * FROM yj_mb_quest_num WHERE mb_id = '$userid'";
// $stmt = $pdo->prepare($sql);
// $stmt->execute();
// $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
// echo $result[0][$num_type];


switch ($num_type) {
    case "mb_point":        // g5_member 테이블
        $sql = "SELECT * FROM g5_member WHERE mb_id = '$userid'";
        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        echo $result[0][$num_type];
        break;
    default:                // yj_mb_quest_num 테이블
        $sql = "SELECT * FROM yj_mb_quest_num WHERE mb_id = '$userid'";
        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        echo $result[0][$num_type];
        break;

}



?>