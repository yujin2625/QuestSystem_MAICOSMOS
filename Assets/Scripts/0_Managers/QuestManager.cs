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
            QuestData questData = QuestDataContainer.Instance.FindQuestData(quest.QuestID);
            Quests.Add(new Quest(quest.QuestID, quest.EQuestType, quest.Title, quest.Context, quest.Steps, questData == null ? 0 : questData.cond_num + 1));
        }
    }
    private void StartAllQuests()
    {
        foreach (Quest quest in Quests)
        {
            //QuestData questData = QuestDataContainer.Instance.FindQuestData(quest.QuestID);
            //if (questData==null)
            //    StepManager.instance.StartStep(quest.Steps[quest.CurrentStep - 1]);
            
            quest.NextStep();
        }
    }

    public void NextStep(Quest quest)
    {
        quest.NextStep();
    }
    public void NextStep(QuestScriptableObject questSO)
    {
        foreach (Quest quest in Quests)
        {
            if (quest.QuestID == questSO.QuestID)
            {
                quest.NextStep();
            }
        }
    }
    public void NextStep(string questID)
    {
        foreach (Quest quest in Quests)
        {
            if (quest.QuestID == questID)
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
        if (CurrentStep >= Steps.Count)
        {
            return false;
        }
        CurrentStep++;
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