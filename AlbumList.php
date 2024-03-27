<?php
require_once("../g5/common.php");

require_once('config.php');
session_start();

$userid = $_POST['userid']; // 아카이브 주인 닉네임? ID?

if(!$userid)
{
    http_response_code(400); // 400 Bad Request
    exit;
} 

// Check Session ss_mb_id
if(!isset($_SESSION['ss_mb_id'])) {
    $_SESSION['ss_mb_id'] = 'maicosmos'; // for test
    // user was not login so send status error and stop 
    // this must be activated in real service !!!
    // http_response_code(403); // 403 mean forbidden
    // return;
}

$sql = "SELECT * FROM album WHERE album.userid = '$userid'";
$statement = $pdo->prepare($sql);
$statement -> execute();
$albumlist = array();

while($row=$statement->fetch(PDO::FETCH_ASSOC))
{
    $albumlist[] = $row;
}

$album =  ['albumlist' => $albumlist];
echo json_encode($album, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
?>