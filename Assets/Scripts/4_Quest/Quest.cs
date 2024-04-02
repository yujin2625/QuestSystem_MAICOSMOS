using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ERepeatType
{
    one_off,        // ��ȸ��
    daily,          // ����
    weekly,         // ����
    monthly,        // �Ŵ�
    constantly,     // ���
    required,       // �ʼ�
    last
}
public enum ESpace
{
    any,            // �ƹ����̳�
    lobby,          // �κ�
    gallery,        // ������
    lecture,        // ���ǽ�
    last
}

[Serializable]
public class Quest      // DB���� �������� ����Ʈ ����
{
    //==============================================================================
    // quest_master���� ������ ����
    public string QuestID { get; set; }    // ����Ʈ ID (DB ������)
    public string QuestName { get; set; }    // ����Ʈ �̸� (Prefab �̸���)
    public ERepeatType RepeatType { get; set; }    // �ݺ� ���� (��ȸ��, ����, ����, �Ŵ�, ���, �ʼ�)
    public int RewardPoint { get; set; }    // ����Ʈ ����
    public int MinLevel { get; set; }    // ����Ʈ ���� ���� - ����
    public ESpace Space { get; set; }    // ����Ʈ ���� ���� - ���
    public string Title {  get; set; }      // ����Ʈ ����
    public string Context { get; set; }     // ����Ʈ ����

    //==============================================================================
    // yj_mb_quest���� ������ ���� 
    public int StepIndex { get; set; }     // ���� ���� ���� step index
    public int Completed { get; set; }     // ����Ʈ �Ϸ� ����

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
