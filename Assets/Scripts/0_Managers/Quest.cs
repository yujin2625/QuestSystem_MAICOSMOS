using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


[Serializable]
public class Quest
{
    public string QuestID { get; set; }
    public EQuestType EQuestType { get; set; }

    public string Title { get; set; }
    public string Context { get; set; }

    public List<Step> Steps = new List<Step>();

    public int CurrentStep { get; set; }
    public bool Completed { get { return CurrentStep >= Steps.Count; } set { } }
    public bool NextStep()
    {
        if (CurrentStep >= Steps.Count)
        {
            return false;
        }
        AddCurrentStep();
        if (CurrentStep >= Steps.Count)
        {
            Debug.Log(Title + " QuestIsFinished");
            //DB에 해당 퀘스트 완료 표시
            DataSender.Instance.StartSendQuestData(QuestID, CurrentStep.ToString() , "1");
            return false;
        }
        // DB에 해당 스텝 완료 표시
        Debug.Log("CurrentStep to string"+CurrentStep.ToString());
        DataSender.Instance.StartSendQuestData(QuestID, CurrentStep.ToString(), "0");
        //StepManager.instance.StartStep(Steps[CurrentStep]);
        return true;
    }

    private void AddCurrentStep()
    {
        CurrentStep++;
        DataSender.Instance.StartSendQuestData(QuestID, CurrentStep.ToString(), Completed ? "1" : "0");
    }



    public Quest(string questID, EQuestType eQuestType, string title, string context, List<StepBase> steps, int currentStep,bool completed)
    {
        QuestID = questID;
        EQuestType = eQuestType;
        Title = title;
        Context = context;
        //Steps.Clear();
        foreach (StepBase step in steps)
        {
            //Steps.Add(new Step(questID, step.Priority, step.IsCompleted, step.Condition, step.PositionStep, step.NumStep, step.InputStep, step.UIStep));
        }
        CurrentStep = currentStep;
        Completed = completed;
    }
}