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
    /// QM의 역할
    /// - DB에서 정보 받아오기/전달하기
    /// - 저장된 퀘스트 정보 분류, 저장
    /// - 퀘스트 실행 조건 확인
    /// - 퀘스트 오브젝트 생성
    /// </summary>


    // Need to get input from inspector
    [Header("Required Inputs")]
    [SerializeField] private string GetUserQuestURL;        // 유저의 퀘스트 정보 받아오는 url
    [SerializeField] private string GetQuestURL;            // 퀘스트 정보 받아오는 url
    [SerializeField] private Transform QuestParent;         // 퀘스트 생성 시 부모 오브젝트

    [Space(10f)]

    // Serialized for Debugging
    [Header("View Result")]
    [SerializeField] private bool IsQuestSet = true;        // 퀘스트 정보 받아오기 완료 했나?


    // #### 퀘스트 정보는 항상 QuestList 사용 ####
    public List<Quest> QuestList = new List<Quest>();       // 통합 퀘스트 정보

    #region ForDebugging

    //public UserQuestDataSet userQuestDataSet;               // 유저의 퀘스트 데이터
    //public QuestDataSet questDataSet;                       // 퀘스트 데이터

    #endregion

    private List<QuestObject> QuestObjects { get { return QuestParent.GetComponentsInChildren<QuestObject>().ToList(); } }

    //private List<Quest> StartableQuests = new List<Quest>();
    //private List<Quest> StartedQuests = new List<Quest>();
    //private List<Quest> CompletedQuests = new List<Quest>();
    //private List<Quest> DailyQuests = new List<Quest>();
    //private List<Quest> WeeklyQuests = new List<Quest>();

    private IEnumerator Start()
    {
        yield return SetQuestData();        // 퀘스트 통합 정보 받아온 후
        CreateQuestObjects();               // 퀘스트 오브젝트 생성!


    }

    public IEnumerator SetQuestData()
    {
        UserQuestDataSet userQuestDataSet;
        QuestDataSet questDataSet;

        // =========User Quest Data 가져오기==================
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

        // =========Quest Data 가져오기====================
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

        // ===========Quest List 설정하기==================
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
    {   // 진행 가능 퀘스트 오브젝트 생성 ( 진행 가능 퀘스트 && 존재하지 않는 퀘스트 )
        if (!IsQuestSet)
        {
            Debug.LogError("QuestNotSetYet");
            return;
        }
        foreach (Quest quest in GetQuestsPlayable())     // 모든 진행 가능 퀘스트
        {
            if (!CheckQuestObjectExsistance(quest.QuestName))       // 퀘스트 오브젝트 존재하지 않으면
            {
                GameObject obj = Instantiate(Resources.Load(quest.QuestName) as GameObject, QuestParent);
                obj.GetComponent<QuestObject>().Quest = quest;              // 퀘스트 오브젝트에 퀘스트 정보 전달
            }
        }

    }

    private bool CheckQuestObjectExsistance(string name)
    {   // 퀘스트 오브젝트 존재 여부 리턴
        foreach (QuestObject obj in QuestObjects)
        {
            if (obj.name.Split('(')[0] == name)
                return true;
        }
        return false;

    }

    public List<Quest> GetQuestByRepeatType(ERepeatType repeatType)
    {   // 반복 종류에 따라 퀘스트 리턴
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
    {   // 완료 여부에 따라 퀘스트 리턴
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
    {   // 유저의 퀘스트 정보에 맞는 퀘스트 리턴
        foreach (QuestData quest in questDataSet.QuestDatas)
        {
            if (quest.name == userQuestData.quest_id)
                return quest;
        }
        return null;
    }
    private UserQuestData GetMatchingData(UserQuestDataSet userQuestDataSet, QuestData questData)
    {   // 퀘스트 정보에 맞는 유저의 퀘스트 정보 리턴
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


