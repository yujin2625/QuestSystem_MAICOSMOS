using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    private string LoadDataURL = "https://maicosmos.com/yujin/yujin.php";

    [SerializeField] private DataLoader DataLoader;

    [SerializeField] private List<QuestScriptableObject> QuestSOs = new List<QuestScriptableObject>();
    [SerializeField] private QuestDataSet QuestDataSet = new QuestDataSet();
    [SerializeField] private List<Quest> Quests = new List<Quest>();

    private void Start()
    {
        StartGetData();
        SetQuestData();
    }
    public void SetQuestData()
    {
        foreach (QuestScriptableObject so in QuestSOs)
        {
            QuestData questData = FindQuestData(so.QuestID);
            if (questData == null)
                Quests.Add(new Quest(so.QuestID, so.EQuestType, so.Title, so.Context, so.Steps, 0, false));
            else
                Quests.Add(new Quest(so.QuestID, so.EQuestType, so.Title, so.Context, so.Steps, questData.cond_num, questData.IsCompleted));
        }
    }
    public void StartGetData()
    {
        StartCoroutine(GetData(LoadDataURL));
    }
    private IEnumerator GetData(string url)
    {
        DataLoader.StartWebRequest(url);
        string str;
        do
        {
            yield return null;
            str = DataLoader.ReturnedData;
        } while (str == null);
        QuestDataSet = JsonConvert.DeserializeObject<QuestDataSet>(str);
    }
    private QuestScriptableObject FindQuestSO(string id)
    {
        foreach (QuestScriptableObject data in QuestSOs)
        {
            if (data.QuestID == id)
                return data;
        }
        Debug.LogError("QuestList does not contain " + id);
        return null;
    }
    private QuestData FindQuestData(string id)
    {
        foreach (QuestData data in QuestDataSet.QuestDatas)
        {
            if (data.quest_id == id) return data;
        }
        return null;
    }
}
