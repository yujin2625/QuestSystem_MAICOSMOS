using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStep : StepBase
{
    //[SerializeField] private GameObject UI;
    [SerializeField] private string ButtonName;
    public override void EndStep()
    {
        QuestUIManager.Instance.ButtonDictionary[ButtonName].onClick.RemoveListener(EndStep);
        Debug.Log("UIStep Complete !!");
        base.EndStep();
    }
    private void OnEnable()
    {
        QuestUIManager.Instance.ButtonDictionary[ButtonName].onClick.AddListener(EndStep);
    }
}