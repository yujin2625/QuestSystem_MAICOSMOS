using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    public string ReturnedData { get { return m_returnedData; } }
    private string m_returnedData = null;

    //https://maicosmos.com/yujin/yujin.php
    public void StartWebRequest(string _url)
    {
        StartCoroutine(SendWebRequest(_url));
    }
    private IEnumerator SendWebRequest(string _url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(_url);
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
            Debug.LogError("There was an error getting the data request");
        else
        {
            m_returnedData = webRequest.downloadHandler.text ;
            Debug.Log(m_returnedData);
        }
    }
}



