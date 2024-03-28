using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQuestType
{
    None,
    Tutorial,
    Daily,
    Weekly,
    Last
}
public enum EConditionType
{
    Position,
    UI,
    Input,
    Last
}

//[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/QuestScriptableObject", order = 1)]
public class QuestObject : MonoBehaviour
{
    public string QuestID { get { return name; } }

    [SerializeField] private EQuestType m_questType;
    public EQuestType EQuestType { get => m_questType; }

    [SerializeField] private string m_title;
    public string Title { get => m_title; }

    [SerializeField,TextArea] private string m_context;
    public string Context { get => m_context; }

    [SerializeField] private List<StepBase> m_Steps = new List<StepBase>();
    public List<StepBase> Steps { get => m_Steps; }




}
