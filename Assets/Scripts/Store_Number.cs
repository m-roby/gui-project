using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Store_Number : MonoBehaviour {

    string File_Name;
    string Path;
    public GameObject Data_Saver;
    public string StoreNumber;

    // Use this for initialization
    void Start()
    {

        File_Name = "/Store_Number.txt";
        Path = Application.dataPath + File_Name;
        StoreNumber = System.IO.File.ReadAllText(Path);

        gameObject.GetComponent<Text>().text = "Store Number: " + StoreNumber;
        Data_Saver.GetComponent<Save_Data>().Store_Number = StoreNumber;

    }
}
