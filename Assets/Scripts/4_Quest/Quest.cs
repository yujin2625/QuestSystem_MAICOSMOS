using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ERepeatType
{
    one_off,        // 일회성
    daily,          // 매일
    weekly,         // 매주
    monthly,        // 매달
    constantly,     // 상시
    required,       // 필수
    last
}
public enum ESpace
{
    any,            // 아무곳이나
    lobby,          // 로비
    gallery,        // 갤러리
    lecture,        // 강의실
    last
}

[Serializable]
public class Quest      // DB에서 가져오는 퀘스트 정보
{
    //==============================================================================
    // quest_master에서 가져온 정보
    public string QuestID { get; set; }    // 퀘스트 ID (DB 참조용)
    public string QuestName { get; set; }    // 퀘스트 이름 (Prefab 이름용)
    public ERepeatType RepeatType { get; set; }    // 반복 종류 (일회성, 매일, 매주, 매달, 상시, 필수)
    public int RewardPoint { get; set; }    // 포인트 보상
    public int MinLevel { get; set; }    // 퀘스트 시작 조건 - 레벨
    public ESpace Space { get; set; }    // 퀘스트 시작 조건 - 장소
    public string Title {  get; set; }      // 퀘스트 제목
    public string Context { get; set; }     // 퀘스트 내용

    //==============================================================================
    // yj_mb_quest에서 가져온 정보 
    public int StepIndex { get; set; }     // 현재 진행 중인 step index
    public int Completed { get; set; }     // 퀘스트 완료 여부

    //==============================================================================
    public bool IsCompleted { get { return Completed != 0; } }
    public bool IsStartable { get { return Completed==0
                &&( Space==PlayerManager.Instance.PlayerSpace||Space==ESpace.any)
                && MinLevel<=PlayerManager.Instance.PlayerLevel; } }

    public Quest(string questID, string questName, string repeatType, int rewardPoint, int minLevel, ESpace space,string title,string context, int stepIndex = 0, int completed = 0)
    {
        QuestID = questID;
        QuestName = questName;
        RepeatType = (ERepeatType)Enum.Parse(typeof(ERepeatType), repeatType);
        RewardPoint = rewardPoint;
        MinLevel = minLevel;
        Space = space;
        Title = title;
        Context = context;
        StepIndex = stepIndex;
        Completed = completed;
    }

}
