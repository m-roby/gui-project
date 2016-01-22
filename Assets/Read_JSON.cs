using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Read_JSON : MonoBehaviour {

    public string JSON_Input;
    public JsonData data;
    public int terminals;
    public GameObject[] arTerminals = new GameObject[4];
    public GameObject computer;
    public GameObject computer1;
    public Vector2 spawn;
    public float X = -6;

    // Use this for initialization
    void Start () {

        JSON_Input = File.ReadAllText(Application.dataPath + "/Resources/document.json");
        computer = Resources.Load("computer") as GameObject;

        data = JsonMapper.ToObject(JSON_Input);
        //Debug.Log(data["Terminals"][1]["Name"]);
        //Debug.Log(GetItem("TTYP53", "Terminals")["brand"]);
        terminals = data["Type"]["POS"].Count;
        spawn = new Vector2(X, 0);
        GetName();

    }


    void GetName()
    {
        for (int i = 0; i < terminals ; i++)
        {
            arTerminals[i] = Instantiate(computer);
            arTerminals[i].transform.position = spawn;
            arTerminals[i].name = "" + data["Type"]["POS"][i];
            X = X + 2;
            spawn = new Vector2((X), 0);
        }
    }



    JsonData GetItem(string Name, string type)
    {
        for (int i = 0; i < data[type].Count; i++)
        {
            if (data[type][i]["Name"].ToString() == Name)
            {
                return data[type][i];
            }
        }
        return null;
    }





	// Update is called once per frame
	void Update () {
	
	}
}
