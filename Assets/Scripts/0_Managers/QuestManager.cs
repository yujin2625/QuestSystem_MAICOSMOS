using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

    [SerializeField] private List<QuestObject> QuestSOs = new List<QuestObject>();
    [SerializeField] private QuestDataSet QuestDataSet = new QuestDataSet();
    [SerializeField] private List<Quest> Quests = new List<Quest>();

    private IEnumerator Start()
    {
        yield return GetData(LoadDataURL);
        if (QuestDataSet != null)
            SetQuestData();
        else
            Debug.LogError(" Failed retrive data set");
    }

    public void SetQuestData()
    {
        foreach (QuestObject so in QuestSOs)
        {
            QuestData questData = FindQuestData(so.QuestID);
            if (questData == null)
                Quests.Add(new Quest(so.QuestID, so.EQuestType, so.Title, so.Context, so.Steps, 0, false));
            else
                Quests.Add(new Quest(so.QuestID, so.EQuestType, so.Title, so.Context, so.Steps, questData.cond_num, questData.IsCompleted));
        }
    }

    private IEnumerator GetData(string url)
    {
        yield return DataLoader.SendWebRequest(url, (string result) =>
        {
            QuestDataSet = JsonConvert.DeserializeObject<QuestDataSet>(result);
        });
    }

    private QuestObject FindQuestSO(string id)
    {
        foreach (QuestObject data in QuestSOs)
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

    internal void NextStep(Quest m_quest)
    {
        throw new NotImplementedException();
    }
}
