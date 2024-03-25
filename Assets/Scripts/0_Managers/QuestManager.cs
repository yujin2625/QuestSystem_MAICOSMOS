using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    [SerializeField] private List<QuestScriptableObject> m_scriptableQuests;
    [SerializeField] private List<Quest> Quests = new List<Quest>();

    private void Start()
    {
        InitQuests();
        StartAllQuests();
    }

    private void InitQuests()
    {
        foreach (QuestScriptableObject quest in m_scriptableQuests)
        {
            Quests.Add(new Quest(quest.QuestID, quest.EQuestType, quest.Title, quest.Context, quest.Steps, quest.CurrentStep));
        }
    }
    private void StartAllQuests()
    {
        foreach (Quest quest in Quests)
        {
            StepManager.instance.StartStep(quest.Steps[quest.CurrentStep]);
        }
    }

    public void NextStep(Quest quest)
    {
        quest.NextStep();
    }
    public void NextStep(QuestScriptableObject quest)
    {
        foreach(Quest q in Quests)
        {
            if(q.QuestID == quest.QuestID)
            {
                q.NextStep();
            }
        }
    }
    public void NextStep(string questID)
    {
        foreach(Quest quest in Quests)
        {
            if(quest.QuestID == questID)
                quest.NextStep();
        }
    }

    public List<Quest> GetQuests()
    {
        return Quests;
    }
}

[Serializable]
public class Quest
{
    public string QuestID { get; set; }
    public EQuestType EQuestType { get; set; }

    public string Title { get; set; }
    public string Context { get; set; }

    public List<Step> Steps = new List<Step>();

    public int CurrentStep { get; set; }
    public bool NextStep()
    {
        CurrentStep++;
        if (CurrentStep >= Steps.Count)
        {
            Debug.Log(QuestID + " QuestIsFinished");
            //DB에 해당 퀘스트 완료 표시
            return false;
        }
        // DB에 해당 스텝 완료 표시
        StepManager.instance.StartStep(Steps[CurrentStep]);
        return true;
    }

    public Quest(string questID, EQuestType eQuestType, string title, string context, List<StepScriptableObject> steps, int currentStep)
    {
        QuestID = questID;
        EQuestType = eQuestType;
        Title = title;
        Context = context;
        //Steps.Clear();
        foreach (StepScriptableObject step in steps)
        {
            Steps.Add(new Step(questID, step.Priority, step.IsCompleted, step.Condition, step.PositionStep, step.NumStep, step.InputStep, step.UIStep));
        }
        CurrentStep = currentStep;
    }
}