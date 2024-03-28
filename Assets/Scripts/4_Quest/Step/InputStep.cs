using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStep : StepBase
{
    [SerializeField] private string PressedKey;
    public string m_pressedKey { get => PressedKey; set => PressedKey = value; }
}
