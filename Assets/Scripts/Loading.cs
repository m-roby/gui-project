using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    public string Path;
    public string Ticket_Name;

    public string Command_File_Name;

    public string Name;
    public int Error_Code;
    public string TroubleShooting_Steps;
    public string Terminal_Summary;


    void Start()
    {
        Invoke("Loading_Screen", 1f);
    }

    public void Loading_Screen()
    {
        Command_File_Name = "/Command_Cache.txt";
        Path = Application.dataPath + Command_File_Name;

        if (File.Exists(Path) == true)
        {
            string[] lines = File.ReadAllLines(Path);
            if (lines[lines.Length - 1] == "Done")
            {
                Debug.Log("Done Running Command");
                SceneManager.LoadScene("recovery");
            }
            else
            {
                Invoke("Loading_Screen", 1f);
                Debug.Log("Still Loading");

            }

        }
    }
	
	void Update ()
    {
	
	}
}
