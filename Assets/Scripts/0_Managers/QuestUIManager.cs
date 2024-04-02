using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum EQuestRadio
{
    Started,
    Completed,
    Daily,
    Weekly,
    Monthly,
    Last
}
public class QuestUIManager : MonoBehaviour
{
    #region singleton
    public static QuestUIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    #endregion

    [Header("Buttons")]
    [SerializeField] private RadioButtonGroup TypeRadioButtonGroup;
    [SerializeField] private RadioButtonGroup QuestRadioButtonGroup;
    [SerializeField] private List<Button> QuestButtons = new List<Button>();
    //[SerializeField] private Button LeftPageButton;
    //[SerializeField] private Button RightPageButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text QuestText;
    [SerializeField] private TMP_Text QuestRewardText;

    [Header("UI Step Buttons")]
    [SerializeField] private List<ButtonDictionary> Buttons = new List<ButtonDictionary>();
    public Dictionary<string, Button> ButtonDictionary = new Dictionary<string, Button>();

    public List<Quest> tempQuestList = new List<Quest>();

    private List<GameObject> QuestButtonChecks
    {
        get
        {
            List<GameObject> checks = new List<GameObject>();
            foreach (var button in QuestButtons)
            {
                checks.Add(button.GetComponentInChildren<Image>().gameObject);
            }
            return checks;
        }
    }

    private void Start()
    {
        foreach (var button in Buttons)
        {
            ButtonDictionary.Add(button.key, button.value);
        }

    }

    public void OnClickTypeRadio()
    {
        List<Quest> quests = QuestManager.instance.ReturnQuestsByType((EQuestRadio)TypeRadioButtonGroup.SelectedButtonIndex);
        tempQuestList.Clear();
        tempQuestList = quests;
        for(int i = 0; i < QuestButtons.Count; i++)
        {
            if (i < quests.Count)
            {
                QuestButtons[i].interactable = true;
                QuestButtons[i].GetComponentInChildren<TMP_Text>().text = quests[i].Title;
            }
            else
            {
                QuestButtons[i].interactable = false;
                QuestButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            }
        }
        OnClickQuestRadio();
    }

    public void OnClickQuestRadio()
    {
        if (tempQuestList.Count == 0)
        {
            QuestText.text = "";
            QuestRewardText.text = "";
            return;
        }
        QuestText.text = tempQuestList[QuestRadioButtonGroup.SelectedButtonIndex].Context;
        QuestRewardText.text = tempQuestList[QuestRadioButtonGroup.SelectedButtonIndex].StepIndex.ToString();
    }

}

[Serializable]
public class ButtonDictionary
{
    public string key;
    public Button value;
}