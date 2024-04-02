using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadioButtonGroup : MonoBehaviour
{
    [SerializeField] private List<RadioButton> RadioButtons;
    [SerializeField] private RadioButton ClickedOnAwakeButton;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color SelectedColor;

    private RadioButton SelectedButton;
    public int SelectedButtonIndex { get { if (SelectedButton == null) return 0; return RadioButtons.IndexOf(SelectedButton); } }

    private void Awake()
    {
        SelectedButton = ClickedOnAwakeButton;
        if (!RadioButtons.Contains(ClickedOnAwakeButton))
        {
            Debug.LogError("RadioButtons does not contain ClickedOnAwakeButton");
        }
        List<RadioButton> uniqueRadioButtons = RadioButtons.Distinct().ToList();
        if (RadioButtons.Except(uniqueRadioButtons).Count() > 0)
        {
            Debug.LogError("RadioButtons are not unique");
        }
        SetRadioButtonOn(RadioButtons, ClickedOnAwakeButton);
    }
    private void SetRadioButtonOn(List<RadioButton> radiobuttons, int index)
    {
        SelectedButton = radiobuttons[index];
        for (int i = 0; i < radiobuttons.Count; i++)
        {
            if (i == index)
            {
                radiobuttons[i].SetRadioOn();
                radiobuttons[i].GetComponent<Image>().color = SelectedColor;
            }
            else
            {
                radiobuttons[i].SetRadioOff();
                radiobuttons[i].GetComponent<Image>().color = NormalColor;
            }
        }
    }
    private void SetRadioButtonOn(List<RadioButton> radiobuttons, RadioButton button)
    {
        SelectedButton = button;
        foreach (RadioButton radiobutton in radiobuttons)
        {
            if (button == radiobutton)
            {
                radiobutton.SetRadioOn();
                radiobutton.GetComponent<Image>().color = SelectedColor;
            }
            else
            {
                radiobutton.SetRadioOff();
                radiobutton.GetComponent<Image>().color = NormalColor;
            }
        }
    }

    public void RadioSelected()
    {
        SetRadioButtonOn(RadioButtons, EventSystem.current.currentSelectedGameObject.GetComponent<RadioButton>());
    }
    public void ResetRadioSelected()
    {
        SetRadioButtonOn(RadioButtons, ClickedOnAwakeButton);
    }
}
