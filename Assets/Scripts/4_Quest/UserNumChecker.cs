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
        //Debug.LogError(eConditionNumType.ToString());
        form.AddField("NumType", eConditionNumType.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post(checkUserUrl, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
            Debug.LogError(webRequest.error);

        //Debug.LogWarning(webRequest.downloadHandler.text);

        CurrentUserNum = Convert.ToInt32(webRequest.downloadHandler.text);

    }

    public void StartCheckUserNum(Quest _quest, EConditionNumType _eConditionNumType, int _conditionUserNum)
    {
        m_quest = _quest;
        EConditionNumType = _eConditionNumType;
        ConditionUserNum = _conditionUserNum;
        StartCoroutine(CheckUserNum(EConditionNumType));
    }
}
