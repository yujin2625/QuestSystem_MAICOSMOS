using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NumStep : StepBase
{
    [SerializeField] private EConditionNumType EConditionNum;
    public EConditionNumType m_eConditionNum { get => EConditionNum; set => EConditionNum = value; }

    [SerializeField] private int ConditionNum;
    public int m_conditionNum { get => ConditionNum; set => ConditionNum = value; }
}