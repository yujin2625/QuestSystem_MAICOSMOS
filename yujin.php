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



//============== 로그인한 유저에 해당하는 quest_data 확인하기===========

    //$userid = trim($_POST['userid']);
    
    // $sql = "SELECT * FROM yj_mb_quest WHERE mb_id = '".$userid."'  5";
    // $userData = sql_fetch($sql);

    // $quest_id = $userData[quest_id];
    // echo $quest_id;
    // echo "<br/>";
    // var_dump($quest_id);
    // echo "<br/>";
//====================================================================
//
//==============로그인 한 유저의 퀘스트 목록 가져오기====================

/*
    $sql = "SELECT * FROM yj_quests WHERE id = '".$userid."'";
    $result = sql_fetch($sql);
    echo $result;
    echo "<br/>";
    var_dump($result);
*/

//====================================================================
//
//============== 로그인한 유저에 해당하는 quest_data 확인하기===========


// $userid = "test1";

// $sql = "SELECT * FROM yj_mb_quest WHERE mb_id = '".$userid."'";
// $res = sql_query($sql);

// $result = array();


// // $quest_id = array();
// // for($i=0;$row = sql_fetch_array($res); $i++) {
// //     $quest_id[$i] = $row;
// // }
// // var_dump($quest_id);
// $row = sql_fetch_array($res);
// foreach($row as $key=>$value){
//     echo $value.",";
// }

//$userid = trim($_POST['userid']);
$userid = "test1";

$sql = "SELECT * FROM yj_mb_quest WHERE mb_id = '".$userid."'";
$stmt = $pdo -> prepare($sql);
$stmt -> execute();
$result = $stmt -> fetchAll(PDO::FETCH_ASSOC);
echo "{\"QuestDatas\":".json_encode($result, JSON_PRETTY_PRINT|JSON_UNESCAPED_UNICODE)." }\n";

/*
$sql = "SELECT * FROM yj_quests";
$stmt = $pdo -> prepare($sql);
$stmt -> execute();
$result = $stmt -> fetchAll(PDO::FETCH_ASSOC);
echo "{\"Data\":".json_encode($result, JSON_PRETTY_PRINT|JSON_UNESCAPED_UNICODE)."}";
  */  

?>