using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Fake_Done : MonoBehaviour {
    public string Path;
    public string Ticket_Name;

    public string Command_File_Name;

    public string Name;
    public int Error_Code;
    public string TroubleShooting_Steps;
    public string Terminal_Summary;

    int Counter;

    // Use this for initialization
    void Start () {

       InvokeRepeating("Loading_Screen", 0f, 10f);
        Counter = 0;

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

            }
            else
            {
                Command_File_Name = "/Command_Cache.txt";
                Path = Application.dataPath + Command_File_Name;
                string Command_Text = "Done" + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);

            }

        }
    }
}
