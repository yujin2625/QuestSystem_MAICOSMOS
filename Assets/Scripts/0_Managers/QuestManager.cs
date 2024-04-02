using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

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


    /// <summary>
    /// QM�� ����
    /// - DB���� ���� �޾ƿ���/�����ϱ�
    /// - ����� ����Ʈ ���� �з�, ����
    /// - ����Ʈ ���� ���� Ȯ��
    /// - ����Ʈ ������Ʈ ����
    /// </summary>


    // Need to get input from inspector
    [Header("Required Inputs")]
    [SerializeField] private string GetUserQuestURL;        // ������ ����Ʈ ���� �޾ƿ��� url
    [SerializeField] private string GetQuestURL;            // ����Ʈ ���� �޾ƿ��� url
    [SerializeField] private Transform QuestParent;         // ����Ʈ ���� �� �θ� ������Ʈ

    [Space(10f)]

    // Serialized for Debugging
    [Header("View Result")]
    [SerializeField] private bool IsQuestSet = true;        // ����Ʈ ���� �޾ƿ��� �Ϸ� �߳�?


    // #### ����Ʈ ������ �׻� QuestList ��� ####
    public List<Quest> QuestList = new List<Quest>();       // ���� ����Ʈ ����

    #region ForDebugging

    //public UserQuestDataSet userQuestDataSet;               // ������ ����Ʈ ������
    //public QuestDataSet questDataSet;                       // ����Ʈ ������

    #endregion

    private List<QuestObject> QuestObjects { get { return QuestParent.GetComponentsInChildren<QuestObject>().ToList(); } }

    //private List<Quest> StartableQuests = new List<Quest>();
    //private List<Quest> StartedQuests = new List<Quest>();
    //private List<Quest> CompletedQuests = new List<Quest>();
    //private List<Quest> DailyQuests = new List<Quest>();
    //private List<Quest> WeeklyQuests = new List<Quest>();

    private IEnumerator Start()
    {
        yield return SetQuestData();        // ����Ʈ ���� ���� �޾ƿ� ��
        CreateQuestObjects();               // ����Ʈ ������Ʈ ����!


    }

    public IEnumerator SetQuestData()
    {
        UserQuestDataSet userQuestDataSet;
        QuestDataSet questDataSet;

        // =========User Quest Data ��������==================
        Debug.Log("Getting User Quest Data...");
        UnityWebRequest userQuestWWW = UnityWebRequest.Get(GetUserQuestURL);
        yield return userQuestWWW.SendWebRequest();
        if (userQuestWWW.error != null)
        {
            Debug.LogError("Getting User Quest Data Failed");
            Debug.LogError(userQuestWWW.error);
            yield break;
        }
        userQuestDataSet = JsonUtility.FromJson<UserQuestDataSet>(userQuestWWW.downloadHandler.text);
        Debug.Log("User Quest Data Get Complete!!");

        // =========Quest Data ��������====================
        Debug.Log("Getting Quest Data...");
        UnityWebRequest questWWW = UnityWebRequest.Get(GetQuestURL);
        yield return questWWW.SendWebRequest();
        if (questWWW.error != null)
        {
            Debug.LogError("Getting Quest Data Failed");
            Debug.LogError(questWWW.error);
            yield break;
        }
        questDataSet = JsonUtility.FromJson<QuestDataSet>(questWWW.downloadHandler.text);
        Debug.Log("Quest Data Get Complete!!");

        // ===========Quest List �����ϱ�==================
        if (!IsQuestSet)
        {
            Debug.LogError("!!!!!!!! Quest Is Already Getting Set !!!!!!!!\nThis Method Will Be Stopped");
            yield break;
        }
        IsQuestSet = false;
        Debug.Log("Setting Quest List...");
        QuestList.Clear();
        foreach (QuestData item in questDataSet.QuestDatas)
        {
            UserQuestData userData = GetMatchingData(userQuestDataSet, item);
            Debug.LogWarning(item.repeat_type);
            if (userData == null)
                QuestList.Add(new Quest(item.qm_id.ToString(), item.name, item.repeat_type, item.reward_point, item.min_level, item.space, item.title, item.context));
            else
                QuestList.Add(new Quest(item.qm_id.ToString(), item.name, item.repeat_type, item.reward_point, item.min_level, item.space, item.title, item.context, userData.cond_num, userData.completed));
        }
        Debug.Log("Quest List Set Complete !!");
        IsQuestSet = true;

    }

    public void CreateQuestObjects()
    {   // ���� ���� ����Ʈ ������Ʈ ���� ( ���� ���� ����Ʈ && �������� �ʴ� ����Ʈ )
        if (!IsQuestSet)
        {
            Debug.LogError("QuestNotSetYet");
            return;
        }
        foreach (Quest quest in GetQuestsPlayable())     // ��� ���� ���� ����Ʈ
        {
            if (!CheckQuestObjectExsistance(quest.QuestName))       // ����Ʈ ������Ʈ �������� ������
            {
                GameObject obj = Instantiate(Resources.Load(quest.QuestName) as GameObject, QuestParent);
                obj.GetComponent<QuestObject>().Quest = quest;              // ����Ʈ ������Ʈ�� ����Ʈ ���� ����
            }
        }

    }

    private bool CheckQuestObjectExsistance(string name)
    {   // ����Ʈ ������Ʈ ���� ���� ����
        foreach (QuestObject obj in QuestObjects)
        {
            if (obj.name.Split('(')[0] == name)
                return true;
        }
        return false;

    }

    public List<Quest> GetQuestByRepeatType(ERepeatType repeatType)
    {   // �ݺ� ������ ���� ����Ʈ ����
        if (!IsQuestSet)
        {
            Debug.LogError("QuestNotSetYet");
            return null;
        }
        List<Quest> result = new List<Quest>();
        foreach (Quest quest in QuestList)
        {
            if (quest.RepeatType == repeatType)
                result.Add(quest);
        }
        return result;
    }

    public List<Quest> GetQuestByCompletion(bool completion)
    {   // �Ϸ� ���ο� ���� ����Ʈ ����
        if (!IsQuestSet)
        {
            Debug.LogError("QuestNotSetYet");
            return null;
        }
        List<Quest> result = new List<Quest>();
        foreach (var quest in QuestList)
        {
            if (quest.IsCompleted == completion)
                result.Add(quest);
        }
        return result;
    }
    public List<Quest> GetQuestsPlayable()
    {
        if (!IsQuestSet)
        {
            Debug.LogError("QuestNotSetYet");
            return null;
        }
        List<Quest> result = new List<Quest>();
        foreach (var quest in QuestList)
        {
            if (quest.IsStartable)
                result.Add(quest);
        }
        return result;
    }
    private QuestData GetMatchingData(QuestDataSet questDataSet, UserQuestData userQuestData)
    {   // ������ ����Ʈ ������ �´� ����Ʈ ����
        foreach (QuestData quest in questDataSet.QuestDatas)
        {
            if (quest.name == userQuestData.quest_id)
                return quest;
        }
        return null;
    }
    private UserQuestData GetMatchingData(UserQuestDataSet userQuestDataSet, QuestData questData)
    {   // ����Ʈ ������ �´� ������ ����Ʈ ���� ����
        foreach (UserQuestData userData in userQuestDataSet.QuestDatas)
        {
            if (userData.quest_id == questData.name)
                return userData;
        }
        return null;
    }

    public List<Quest> ReturnQuestsByType(EQuestRadio questRadio)
    {
        List<Quest> result = new List<Quest>();
        switch (questRadio)
        {
            case EQuestRadio.Started:
                foreach (Quest quest in QuestList)
                {
                    if (quest.IsStartable && quest.RepeatType != ERepeatType.daily && quest.RepeatType != ERepeatType.weekly && quest.RepeatType != ERepeatType.monthly)
                        result.Add(quest);
                }
                break;
            case EQuestRadio.Completed:
                foreach (Quest quest in QuestList)
                {
                    if (quest.IsCompleted && quest.RepeatType != ERepeatType.daily && quest.RepeatType != ERepeatType.weekly && quest.RepeatType != ERepeatType.monthly)
                        result.Add(quest);
                }
                break;
            case EQuestRadio.Daily:
                foreach (Quest quest in QuestList)
                {
                    if (quest.RepeatType == ERepeatType.daily)
                        result.Add(quest);
                }
                break;
            case EQuestRadio.Weekly:
                foreach (Quest quest in QuestList)
                {
                    if (quest.RepeatType == ERepeatType.weekly)
                        result.Add(quest);
                }
                break;
            case EQuestRadio.Monthly:
                foreach (Quest quest in QuestList)
                {
                    if (quest.RepeatType == ERepeatType.monthly)
                        result.Add(quest);
                }
                break;
        }

        return result;
    }

}


