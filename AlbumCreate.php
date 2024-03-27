<?php
require_once("../g5/common.php");

require_once('config.php');
session_start();

$userid = $_POST['userid']; // 사용자 ID
$album = $_POST['album']; // 앨범 이름 

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

$sql = "INSERT INTO album (userid, albumname) VALUES ('$userid','$album')";
$statement = $pdo->prepare($sql);
if($statement -> execute()){
    //sql 성공
    $last_id = $pdo -> lastInsertId();
} else {
    //sql 실패
    $last_id = -1;
}

echo json_encode(array("albumid" => $last_id, "albumname" => $album ));
exit;