using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBase : MonoBehaviour
{
    //[SerializeField] private string context;
    //public string Context { get => context; }

    private QuestObject QuestObject { get => GetComponentInParent<QuestObject>(); }
    
    public virtual void EndStep()
    {
        QuestObject.NextStep();
        gameObject.SetActive(false);
    }
}
