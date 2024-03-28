using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserNumChecker : MonoBehaviour
{
    private Quest m_quest;
    private EConditionNumType EConditionNumType;
    private int ConditionUserNum = 1;
    private int CurrentUserNum = 0;

    private void Update()
    {
        if (CurrentUserNum >= ConditionUserNum)
        {
            Debug.Log("UserNum - " + EConditionNumType.ToString() + " has reached " + ConditionUserNum);
            StopAllCoroutines();
            QuestManager.instance.NextStep(m_quest);
            Destroy(gameObject);
        }
    }

    private IEnumerator CheckUserNum(EConditionNumType eConditionNumType)
    {
        string checkUserUrl = "https://maicosmos.com/yujin/UserNumData.php";
        WWWForm form = new WWWForm();
        form.AddField("NumType", eConditionNumType.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post(checkUserUrl, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
            Debug.LogError(webRequest.error);
        CurrentUserNum = Convert.ToInt32(webRequest.downloadHandler.text);
    }

    public void StartCheckUserNum(Quest _quest, EConditionNumType _eConditionNumType, int _conditionUserNum)
    {
        m_quest = _quest;
        EConditionNumType = _eConditionNumType;
        ConditionUserNum = _conditionUserNum;

        PresentChecker();
        
        StartCoroutine(CheckUserNum(EConditionNumType));
    }

    private void PresentChecker()        // 출석 관련 스텝 처리
    {
        if (EConditionNumType == EConditionNumType.mb_present || EConditionNumType == EConditionNumType.mb_present_week)
        {
            DataSender.Instance.StartAddQuestNumData(EConditionNumType, 1);
            CurrentUserNum++;
        }
    }
}
