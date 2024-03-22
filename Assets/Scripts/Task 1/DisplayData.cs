using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    public GameObject labelHolder;
    public GameObject pointsHolder;

    public GameObject labelPrefab;
    public GameObject pointsPrefab;

    public GameObject popupWindow;

    public TMP_Dropdown dropdown;

    void Start()
    {
        FormList();
    }


    public void FormList()
    {
        ClearList();

        if(dropdown.value == 0)
        {
            foreach (Client client in DeserializeData.clients)
            {
                GameObject labelObject = Instantiate(labelPrefab, labelHolder.transform);
                labelObject.name = client.id.ToString(); //Setting label GameObject's name as id will help identify during access
                labelObject.transform.Find("Text").GetComponent<TMP_Text>().text = client.label;

                GameObject pointsObject = Instantiate(pointsPrefab, pointsHolder.transform);

                // If data exists for a given client id
                if (DeserializeData.allData.dataDict.ContainsKey(client.id.ToString()))
                {
                    pointsObject.transform.Find("Text").GetComponent<TMP_Text>().text = DeserializeData.allData.dataDict[client.id.ToString()].points.ToString();
                    labelObject.GetComponent<Button>().onClick.AddListener(delegate { OpenPopUp(client.id); });
                }
            }
        }
        else if(dropdown.value == 1)
        {
            foreach (Client client in DeserializeData.clients)
            {
                if (client.isManager)
                {
                    GameObject labelObject = Instantiate(labelPrefab, labelHolder.transform);
                    labelObject.name = client.id.ToString(); //Setting label GameObject's name as id will help identify during access
                    labelObject.transform.Find("Text").GetComponent<TMP_Text>().text = client.label;

                    GameObject pointsObject = Instantiate(pointsPrefab, pointsHolder.transform);
                    if (DeserializeData.allData.dataDict.ContainsKey(client.id.ToString()))
                    {
                        pointsObject.transform.Find("Text").GetComponent<TMP_Text>().text = DeserializeData.allData.dataDict[client.id.ToString()].points.ToString();
                        labelObject.GetComponent<Button>().onClick.AddListener(delegate { OpenPopUp(client.id); });
                    }
                }
            }
        }
        else if(dropdown.value == 2)
        {
            foreach (Client client in DeserializeData.clients)
            {
                if (!client.isManager)
                {
                    GameObject labelObject = Instantiate(labelPrefab, labelHolder.transform);
                    labelObject.name = client.id.ToString(); //Setting label GameObject's name as id will help identify during access
                    labelObject.transform.Find("Text").GetComponent<TMP_Text>().text = client.label;

                    GameObject pointsObject = Instantiate(pointsPrefab, pointsHolder.transform);
                    if (DeserializeData.allData.dataDict.ContainsKey(client.id.ToString()))
                    {
                        pointsObject.transform.Find("Text").GetComponent<TMP_Text>().text = DeserializeData.allData.dataDict[client.id.ToString()].points.ToString();
                        labelObject.GetComponent<Button>().onClick.AddListener(delegate { OpenPopUp(client.id); });
                    }
                }
            }
        }
    }

    public void ClearList()
    {
        for (int i = 0; i < labelHolder.transform.childCount; i++)
            Destroy(labelHolder.transform.GetChild(i).gameObject);
        for (int i = 0; i < pointsHolder.transform.childCount; i++)
            Destroy(pointsHolder.transform.GetChild(i).gameObject);
    }

    public void OpenPopUp(int id)
    {
        popupWindow.transform.DOMoveY(GetComponent<RectTransform>().position.y, 1);
        popupWindow.transform.Find("Name").GetComponent<TMP_Text>().text = "Name: " + DeserializeData.allData.dataDict[id.ToString()].name;
        popupWindow.transform.Find("Points").GetComponent<TMP_Text>().text = "Points: " + DeserializeData.allData.dataDict[id.ToString()].points.ToString();
        popupWindow.transform.Find("Address").GetComponent<TMP_Text>().text = "Address: " + DeserializeData.allData.dataDict[id.ToString()].address;
    }

    public void ClosePopUp()
    {
        popupWindow.transform.DOMoveY(GetComponent<RectTransform>().position.y + 2000, 1);
    }
}
