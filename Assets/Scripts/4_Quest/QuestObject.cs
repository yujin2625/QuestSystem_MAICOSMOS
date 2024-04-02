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
        if (m_Quest.StepIndex >= transform.childCount)       // ���� ������ ���� ��
        {
            EndQuest();
            return;
        }
        // ���� ���� ������Ʈ �ѱ�
        transform.GetChild(m_Quest.StepIndex).gameObject.SetActive(true);
        // DB�� ���� ����
    }

    public void EndQuest()
    {
        // ����Ʈ �Ϸ� �� DB�� ����
        Debug.Log("Quest " + m_Quest.QuestName + " is Finished !!");
        Destroy(gameObject);
    }





}

    //// ����Ʈ ID (DB ������)
    //[SerializeField] private string m_QuestID;
    //public string QuestID { get => m_QuestID; }

    //// ����Ʈ �̸� (Prefab �̸���)
    //[SerializeField] private string m_QuestName;
    //public string QuestName { get => m_QuestName; }

    //// �ݺ� ���� (��ȸ��, ����, ����, �Ŵ�, ���, �ʼ�)
    //[SerializeField] ERepeatType m_RepeatType;
    //public ERepeatType RepeatType { get => m_RepeatType; }

    //// ����Ʈ ����
    //[SerializeField] private int m_RewardPoint;
    //public int RewardPoint { get => m_RewardPoint; }

    //// ����Ʈ ���� ���� - ����
    //[SerializeField] private int m_MinLevel;
    //public int MinLevel { get => m_MinLevel; }

    //// ����Ʈ ���� ���� - ���
    //[SerializeField] private ESpace m_Space;
    //public ESpace Space { get => m_Space; }