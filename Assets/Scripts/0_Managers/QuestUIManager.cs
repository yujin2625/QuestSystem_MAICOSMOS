using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestUIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private List<Button> TabButtons = new List<Button>();
    [SerializeField] private List<Button> QuestButtons = new List<Button>();
    [SerializeField] private Button LeftPageButton;
    [SerializeField] private Button RightPageButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text QuestText;
    [SerializeField] private TMP_Text QuestRewardText;
    
    public void SetQuestUI()
    {
        for(int i = 0; i < QuestButtons.Count; i++)
        {

        }



    }

    
}
