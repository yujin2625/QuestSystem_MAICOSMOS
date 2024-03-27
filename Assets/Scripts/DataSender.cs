using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataSender : MonoBehaviour
{
    public static DataSender Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    [SerializeField] private string sendURL;
    public void StartSendQuestData(string quest_id, string cond_num, string completed)
    {
        StartCoroutine(SendQuestData(sendURL, quest_id, cond_num, completed));
    }
    private IEnumerator SendQuestData(string _url, string quest_id, string cond_num, string completed)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("quest_id=" + quest_id + "&cond_num=" + cond_num + "&completed=" + completed));
        WWWForm form = new WWWForm();
        form.AddField("quest_id", quest_id);
        form.AddField("cond_num", cond_num);
        form.AddField("completed", completed);
        UnityWebRequest webRequest = UnityWebRequest.Post(_url, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
            Debug.LogError(webRequest.error);
        else
        {
            Debug.Log("quest data upload complete!");
        }
    }



}
