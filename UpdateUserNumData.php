<?php
require_once ("../g5/common.php");
require_once ('../api/config.php');
session_start();

$userid = "test1";
//$userid = trim($_POST['userid']);
$num_type = $_POST["NumType"];
$num = $_POST["NumCount"];

$sql = "SELECT * FROM yj_mb_quest_num WHERE mb_id = '$userid'";
$stmt = $pdo->prepare($sql);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

$current_num = $result[0][$num_type];
$num += $current_num;
echo $num;
echo $num_type;
$sql = "UPDATE yj_mb_quest_num SET $num_type='$num' WHERE mb_id='$userid'";

$stmt = $pdo->prepare($sql);
$stmt->execute();
/*
switch ($num_type) {
    case "mb_point":        // g5_member 테이블
        //     $sql = "SELECT * FROM g5_member WHERE mb_id = '$userid'";
        //     $stmt = $pdo->prepare($sql);
        //     $stmt->execute();
        //     $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        //     if (count($result) == 0) {
        //         $sql = "INSERT INTO g5_member (mb_id, '$num_type') VALUES ('$userid','$num')";
        //     } else {
        //         $sql = "UPDATE g5_member SET '$num_type'=$num WHERE mb_id='$userid'";
        //     }
        //     $stmt = $pdo->prepare($sql);
        //     $stmt->execute();
        break;
    default:                // yj_mb_quest_num 테이블
        $sql = "SELECT * FROM yj_mb_quest_num WHERE mb_id = '$userid'";
        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

        $current_num = $result[0][$num_type];
        $num += $current_num;
        echo $num;
        echo $num_type;
        $sql = "UPDATE yj_mb_quest_num SET '$num_type'='$num' WHERE mb_id='$userid'";

        $stmt = $pdo->prepare($sql);
        $stmt->execute();
        break;
}
*/


?>