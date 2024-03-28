using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStep : StepBase
{
    [SerializeField] private string PressedUI;
    public string m_pressedUI { get => PressedUI; set => PressedUI = value; }


}
