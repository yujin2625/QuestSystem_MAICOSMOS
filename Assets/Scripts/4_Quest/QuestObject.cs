using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class QuestObject : MonoBehaviour
{
    public Quest Quest { set { m_Quest = value; } }
    private Quest m_Quest;
    //private List<StepBase> Steps { get { return GetComponentsInChildren<StepBase>().ToList(); } }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>m_Quest!=null);
        //Debug.Log("Steps.Count = "+Steps.Count);
        transform.GetChild(m_Quest.StepIndex).gameObject.SetActive(true);
        //Steps[m_Quest.StepIndex].gameObject.SetActive(true);
    }
    private void Awake()
    {


    }

    public void NextStep()
    {
        m_Quest.StepIndex++;
        if (m_Quest.StepIndex >= transform.childCount)       // 다음 스텝이 없을 시
        {
            EndQuest();
            return;
        }
        // 다음 스텝 오브젝트 켜기
        transform.GetChild(m_Quest.StepIndex).gameObject.SetActive(true);
        // DB에 정보 전달
    }

    public void EndQuest()
    {
        // 퀘스트 완료 됨 DB에 전달
        Debug.Log("Quest " + m_Quest.QuestName + " is Finished !!");
        Destroy(gameObject);
    }





}

    //// 퀘스트 ID (DB 참조용)
    //[SerializeField] private string m_QuestID;
    //public string QuestID { get => m_QuestID; }

    //// 퀘스트 이름 (Prefab 이름용)
    //[SerializeField] private string m_QuestName;
    //public string QuestName { get => m_QuestName; }

    //// 반복 종류 (일회성, 매일, 매주, 매달, 상시, 필수)
    //[SerializeField] ERepeatType m_RepeatType;
    //public ERepeatType RepeatType { get => m_RepeatType; }

    //// 포인트 보상
    //[SerializeField] private int m_RewardPoint;
    //public int RewardPoint { get => m_RewardPoint; }

    //// 퀘스트 시작 조건 - 레벨
    //[SerializeField] private int m_MinLevel;
    //public int MinLevel { get => m_MinLevel; }

    //// 퀘스트 시작 조건 - 장소
    //[SerializeField] private ESpace m_Space;
    //public ESpace Space { get => m_Space; }