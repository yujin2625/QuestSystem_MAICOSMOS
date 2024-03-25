using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuestUIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private RadioButtonGroup TypeRadioButtonGroup;
    [SerializeField] private RadioButtonGroup QuestRadioButtonGroup;
    [SerializeField] private List<Button> QuestButtons = new List<Button>();
    [SerializeField] private Button LeftPageButton;
    [SerializeField] private Button RightPageButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text QuestText;
    [SerializeField] private TMP_Text QuestRewardText;

    private QuestDataSet m_questDataSet;

    private List<Quest> m_QuestList = new List<Quest>();
    private List<Quest> m_StartedQuests = new List<Quest>();
    private List<Quest> m_CompletedQuests = new List<Quest>();
    private List<Quest> m_DailyQuests = new List<Quest>();
    private List<Quest> m_WeeklyQuests = new List<Quest>();

    private Quest m_SelectedQuest;

    private void Start()
    {
        OnClickQuest();
    }
    public void OnClickQuest()
    {
        StartCoroutine(SetQuestUI());
    }
    private IEnumerator SetQuestUI()
    {
        m_QuestList = QuestManager.instance.GetQuests();
        QuestDataManager.instance.StartGetData();
        yield return new WaitUntil(() => QuestDataManager.instance.QuestDataSet != null);
        m_questDataSet = QuestDataManager.instance.QuestDataSet;
        Debug.Log("0000000000000000000");
        SortQuests();
        Debug.Log("1111111111111111111");
        InitQuestUI();
    }

    public void InitQuestUI()
    {
        switch (TypeRadioButtonGroup.SelectedButtonIndex)
        {
            case 0:
                SetQuestButtonText(m_StartedQuests);
                break;
            case 1:
                SetQuestButtonText(m_CompletedQuests);
                break;
            case 2:
                SetQuestButtonText(m_DailyQuests);
                break;
            case 3:
                SetQuestButtonText(m_WeeklyQuests);
                break;
            case 4:
                SetQuestButtonText(m_QuestList);
                break;
        }
    }
    private void SetQuestButtonText(List<Quest> quests)
    {
        SetQuestButtons(quests);
        SetQuestText(quests);
    }
    private void SetQuestButtons(List<Quest> quests)
    {
        for (int i = 0; i < QuestButtons.Count; i++)
        {
            if (i > quests.Count - 1)
            {
                QuestButtons[i].GetComponentInChildren<TMP_Text>().text = "";
                QuestButtons[i].GetComponent<Button>().enabled = false;
                continue;
            }
            QuestButtons[i].GetComponent<Button>().enabled = true;
            QuestButtons[i].GetComponentInChildren<TMP_Text>().text = quests[i].Title;
        }
    }
    private void SetQuestText(List<Quest> quests)
    {
        // ================ 나중에 간단하게 바꿀것======================================
        if (quests.Count == 0)
        {
            QuestText.text = "";
            QuestRewardText.text = "";
            return;
        }
        Quest quest = quests[QuestRadioButtonGroup.SelectedButtonIndex];
        if (quest == null)
        {
            QuestText.text = "";
            QuestRewardText.text = "";
            return;
        }
        QuestText.text = quest.Context;
        QuestRewardText.text = "Reward not set yet";
    }
    private void SortQuests()
    {
        foreach (Quest data in m_QuestList)
        {
            if (FindQuestData(data.QuestID).IsCompleted)
            {
                m_CompletedQuests.Add(data);
                continue;
            }

            // check if repeated quest
            switch (data.EQuestType)
            {
                case EQuestType.Daily:
                    m_DailyQuests.Add(data);
                    continue;
                case EQuestType.Weekly:
                    m_WeeklyQuests.Add(data);
                    continue;
            }

            if (FindQuestData(data.QuestID).IsStarted)
            {
                m_StartedQuests.Add(data);
                continue;
            }

        }

    }
    private Quest FindQuest(string id)
    {
        foreach (Quest data in m_QuestList)
        {
            if (data.QuestID == id)
                return data;
        }
        Debug.LogError("QuestList does not contain " + id);
        return null;
    }
    private QuestData FindQuestData(string id)
    {
        foreach (QuestData data in m_questDataSet.QuestDatas)
        {
            if (data.quest_id == id) return data;
        }
        return null;
    }
    public void ResetQuestRadio()
    {
        QuestRadioButtonGroup.ResetRadioSelected();
    }
}
