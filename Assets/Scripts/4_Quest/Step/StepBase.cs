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

    [SerializeField] private int m_priority;
    public int Priority { get => m_priority; }

    [SerializeField] private bool m_isCompleted;
    public bool IsCompleted { get => m_isCompleted; set => m_isCompleted = value; }

    [SerializeField] private ECondition m_condition;
    public ECondition Condition { get => m_condition; set => m_condition = value; }

    private void Start()
    {
        GetComponentInParent<QuestObject>().nextStep();
    }


}
