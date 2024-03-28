using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    //public static StepManager instance;

    //[SerializeField] private GameObject NumCheckerPrefab;
    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    instance = this;
    //}

    //public void StartStep(Step step)
    //{
    //    switch (step.Condition)
    //    {
    //        case ECondition.Position:
    //            GameObject pos_obj = Instantiate(step.PositionStep.m_triggerPrefab);
    //            pos_obj.GetComponent<PositionStepChecker>().m_quest = FindQuest(step);
    //            break;
    //        case ECondition.Num:
    //            GameObject num_obj = Instantiate(NumCheckerPrefab);
    //            num_obj.GetComponent<UserNumChecker>().StartCheckUserNum(FindQuest(step),step.NumStep.m_eConditionNum,step.NumStep.m_conditionNum);
    //            break;
    //        case ECondition.Input:
    //            break;
    //        case ECondition.UI:
    //            break;
    //    }
    //}

    //public void EndStep(Step step)
    //{
    //    switch (step.Condition)
    //    {
    //        case ECondition.Position:
    //            QuestManager.instance.NextStep(step.QuestID);
    //            break;
    //        case ECondition.Num:
    //            break;
    //        case ECondition.Input:
    //            break;
    //        case ECondition.UI:
    //            break;
    //    }
    //}

    //public Quest FindQuest(Step step)
    //{
    //    foreach(Quest quest in QuestManager.instance.GetQuests())
    //    {
    //        if(quest.QuestID == step.QuestID)
    //            return quest;
    //    }
    //    Debug.LogWarning("Can not find Matching Quest(" + step.QuestID + ") for Step(" + step.Priority + ")");
    //    return null;
    //}

}

public class Step
{
    public string QuestID { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted { get; set; }
    public ECondition Condition { get; set; }
    public PositionStep PositionStep { get; set; }
    public NumStep NumStep { get; set; }
    public InputStep InputStep { get; set; }
    public UIStep UIStep { get; set; }

    public Step(string questID, int priority, bool isCompleted, ECondition condition, PositionStep positionStep, NumStep numStep, InputStep inputStep, UIStep uIStep)
    {
        QuestID = questID;
        Priority = priority;
        IsCompleted = isCompleted;
        Condition = condition;
        PositionStep = positionStep;
        NumStep = numStep;
        InputStep = inputStep;
        UIStep = uIStep;
    }
}