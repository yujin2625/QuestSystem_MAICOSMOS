using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStep : StepBase
{
    public override void EndStep()
    {
        Debug.Log("PositionStep Complete!!");
        base.EndStep();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter : " + other.name);
            EndStep();
        }
    }




}
