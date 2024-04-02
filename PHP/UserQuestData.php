<?php
require_once("../g5/common.php");
require_once('../api/config.php');
session_start();


//$userid = trim($_POST['userid']);
$userid = "test1";

$sql = "SELECT * FROM yj_mb_quest WHERE mb_id = '".$userid."'";
$stmt = $pdo -> prepare($sql);
$stmt -> execute();
$result = $stmt -> fetchAll(PDO::FETCH_ASSOC);
echo "{\"QuestDatas\":".json_encode($result, JSON_PRETTY_PRINT|JSON_UNESCAPED_UNICODE)." }\n";


?>