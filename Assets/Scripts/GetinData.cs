using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetinData : MonoBehaviour
{
    private string URL = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    public TextMeshProUGUI clientsTextMesh;
    public TMP_Dropdown filterDropdown;
    public GameObject popupPanel;
    public TextMeshProUGUI popupNameText;
    public TextMeshProUGUI popupPointsText;
    public TextMeshProUGUI popupAddressText;

    private JSONNode jsonData;
    private JSONArray clientsArray;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                jsonData = SimpleJSON.JSON.Parse(json);

                clientsArray = jsonData["clients"].AsArray;
                filterDropdown.ClearOptions();
                filterDropdown.AddOptions(new List<string> { "All Clients", "Managers Only", "Non-Managers" });

                filterDropdown.onValueChanged.AddListener(OnFilterDropdownChanged);

                DisplayClients(clientsArray);
            }
        }
    }

    void OnFilterDropdownChanged(int value)
    {
        switch (value)
        {
            case 0: 
                DisplayClients(clientsArray);
                break;

            case 1: 
                JSONArray managers = new JSONArray();
                foreach (JSONNode clientNode in clientsArray)
                {
                    if (clientNode["isManager"].AsBool)
                        managers.Add(clientNode);
                }
                DisplayClients(managers);
                break;

            case 2: 
                JSONArray nonManagers = new JSONArray();
                foreach (JSONNode clientNode in clientsArray)
                {
                    if (!clientNode["isManager"].AsBool)
                        nonManagers.Add(clientNode);
                }
                DisplayClients(nonManagers);
                break;

            default:
                break;
        }
    }

    void DisplayClients(JSONArray clientsToShow)
    {
       
        string clientsList = "Clients:\n";
        foreach (JSONNode clientNode in clientsToShow)
        {
            string label = clientNode["label"];
            clientsList += "- " + label + "\n";
        }

        clientsTextMesh.text = clientsList;
    }

  
}
