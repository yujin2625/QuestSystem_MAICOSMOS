using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : MonoBehaviour
{
    private DataLoader DataLoader;
    [SerializeField] private string URL;

    private void Awake()
    {
        DataLoader = GetComponentInChildren<DataLoader>();
    }

    private void Start()
    {
        StartCoroutine(StartGetData(URL));
    }

    private IEnumerator StartGetData(string url)
    {
        string str = null;
        str = DataLoader.GetReturnedData(url);
        Debug.Log("1" + str);
        yield return new WaitUntil(() => str != null);
        Debug.Log("2" + str);
    }

    


}
