using Newtonsoft.Json;
using UnityEngine;
using System;
using System.Collections.Generic;

public class DeserializeData: MonoBehaviour
{
    public static AllData allData;
    public static List<Client> clients = new List<Client>();
    public void Deserialize()
    {
        allData = DeserializeJson(GlobalVariables.jsonData);
    }

    public AllData DeserializeJson(string jsonData)
    {
        try
        {
            // Deserialize the entire JSON object
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

            // Extract and deserialize clients array
            clients = JsonConvert.DeserializeObject<List<Client>>(jsonObject["clients"].ToString());
            
            // Extract and deserialize data dictionary
            var dataDictionary = new Dictionary<string, Data>();
            var dataObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonObject["data"].ToString());
            foreach (var keyValuePair in dataObject)
            {
                int id = int.Parse(keyValuePair.Key); // Extract ID from key
                var clientData = JsonConvert.DeserializeObject<Data>(keyValuePair.Value.ToString());
                dataDictionary.Add(id.ToString(), clientData);
            }
            
        // Build the AllClients object
        return new AllData
            {
                dataDict = dataDictionary,
                label = (string)jsonObject["label"]
            };
        }
        catch (Exception e)
        {
            Debug.LogError("Error deserializing JSON data: " + e.Message);
            return null;
        }
    }

}

public class Client
{
    public bool isManager { get; set; }
    public int id { get; set; }
    public string label { get; set; }
}

public class Data
{
    public string address { get; set; }
    public string name { get; set; }
    public int points { get; set; }
}

public class AllData
{
    public Dictionary<string, Data> dataDict { get; set; }
    public string label { get; set; }
}