using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections.Generic;

public class API_Call : MonoBehaviour
{
    public DeserializeData deserialize;
    public GameObject listWindow;
    public GameObject fetchButton;
    public GameObject loadingIcon;
    public void InitializeCall()
    {
        StartCoroutine(GetRequest("https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data"));
        loadingIcon.SetActive(true);
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    loadingIcon.SetActive(false);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    loadingIcon.SetActive(false);
                    break;
                case UnityWebRequest.Result.Success:
                    GlobalVariables.jsonData = webRequest.downloadHandler.text; //Storing json as a global string for better access during runtime
                    fetchButton.SetActive(false);
                    loadingIcon.SetActive(false);
                    deserialize.Deserialize();
                    listWindow.SetActive(true);
                    break;
            }
        }
    }
    
}