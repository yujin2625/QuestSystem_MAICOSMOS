using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECondition
{
    Position,
    Num,
    Input,
    UI,
    LAST
}
public enum EConditionNumType
{
    mb_point,
    mb_present,
    mb_present_week,
    mb_coin,
    last
}

public class StepBase : MonoBehaviour
{
    [Header("Step")]
    //[SerializeField] private QuestObject m_quest;
    //public QuestObject Quest { get => m_quest; set => m_quest = value; }

    [SerializeField] private int m_priority;
    public int Priority { get => m_priority; }

    [SerializeField] private bool m_isCompleted;
    public bool IsCompleted { get => m_isCompleted; set => m_isCompleted = value; }

    [SerializeField] private ECondition m_condition;
    public ECondition Condition { get => m_condition; set => m_condition = value; }

    
    //============ Step Conditions ==============================================

    

    //[SerializeField] private PositionStep m_positionStep;
    //public PositionStep PositionStep { get => m_positionStep; }

    //[SerializeField] private NumStep m_numStep;
    //public NumStep NumStep { get => m_numStep; }

    //[SerializeField] private InputStep m_inputStep;
    //public InputStep InputStep { get => m_inputStep; }

    //[SerializeField] private UIStep m_UIStep;
    //public UIStep UIStep { get => m_UIStep; }
}

//[Serializable]
//public class PositionStep
//{
//    [SerializeField] private GameObject TriggerPrefab;
//    public GameObject m_triggerPrefab { get => TriggerPrefab; set => TriggerPrefab = value; }
//}

//[Serializable]
//public class NumStep
//{
//    [SerializeField] private EConditionNumType EConditionNum;
//    public EConditionNumType m_eConditionNum { get => EConditionNum; set => EConditionNum = value; }

//    [SerializeField] private int ConditionNum;
//    public int m_conditionNum { get => ConditionNum; set => ConditionNum = value; }
//}
//[Serializable]
//public class InputStep
//{
//    [SerializeField] private string PressedKey;
//    public string m_pressedKey { get => PressedKey; set => PressedKey = value; }
//}
//[Serializable]
//public class UIStep
//{
//    [SerializeField] private string PressedUI;
//    public string m_pressedUI { get => PressedUI; set => PressedUI = value; }
//}
