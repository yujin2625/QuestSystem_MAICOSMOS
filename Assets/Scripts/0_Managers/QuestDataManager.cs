using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestDataManager : MonoBehaviour
{
    public static QuestDataManager instance;


    private DataLoader DataLoader;

    [SerializeField] private string URL;

    [SerializeField] private QuestDataSet m_QuestDataSet = new QuestDataSet();
    public QuestDataSet QuestDataSet { get { return m_QuestDataSet; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;

        DataLoader = GetComponentInChildren<DataLoader>();

        StartGetData();
    }

    private void Start()
    {
        // 퀘스트 창 버튼 클릭 했을 때 실행하도록 변경해야 함
        //StartCoroutine(GetData(URL));
    }
    public void StartGetData()
    {
        StartCoroutine(GetData(URL));
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
        m_QuestDataSet = JsonConvert.DeserializeObject<QuestDataSet>(str);
    }


}

[Serializable]
public class QuestDataSet
{
    [SerializeField] public List<QuestData> QuestDatas;
}

[Serializable]
public class QuestData
{
    [SerializeField] public int id;
    [SerializeField] public string mb_id;
    [SerializeField] public string quest_id;
    [SerializeField] public int cond_num;
    [SerializeField] public int completed;

    public bool IsCompleted { get { return completed != 0; } }
    public bool IsStarted { get { return cond_num != 0; } }


    public QuestData(int id, string mb_id, string quest_id, int cond_num, int completed)
    {
        this.id = id;
        this.mb_id = mb_id;
        this.quest_id = quest_id;
        this.cond_num = cond_num;
        this.completed = completed;
    }

    public void Print()
    {
        Debug.Log("id : " + id);
        Debug.Log("mb_id : " + mb_id);
        Debug.Log("quest_id : " + quest_id);
        Debug.Log("cond_num : " + cond_num);
        Debug.Log("completed : " + IsCompleted);
    }
}
