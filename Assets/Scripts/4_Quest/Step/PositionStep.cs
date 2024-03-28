using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStep :StepBase
{
    [SerializeField] private GameObject TriggerPrefab;
    public GameObject m_triggerPrefab { get => TriggerPrefab; set => TriggerPrefab = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter : " + other.name);
            //QuestManager.instance.NextStep();
            Destroy(gameObject);
        }
    }

}
