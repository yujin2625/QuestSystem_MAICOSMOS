using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    public IEnumerator SendWebRequest(string _url, Action<string> callback)
    {
        // m_returnedData = null;
        UnityWebRequest webRequest = UnityWebRequest.Get(_url);
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
        {
            Debug.LogError("There was an error getting the data request");
            callback.Invoke(null);
        }
        else
        {
            callback.Invoke(webRequest.downloadHandler.text);
            // m_returnedData = webRequest.downloadHandler.text ;
            //Debug.Log(m_returnedData);
        }
    }
}




