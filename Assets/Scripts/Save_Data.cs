using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Save_Data : MonoBehaviour {
    public string Path;
    public string Ticket_Name;

    public string Command_File_Name;

    public string Name;
    public int Error_Code;
    public string TroubleShooting_Steps;
    public string Script_Output;
    public string Terminal_Summary;

    public string Customer_Input;
    public string Resolution;

    public string Store_Number;

    void Start()
    {
        UnityEngine.Random.Range(9999999, 0);
    }


	// Use this for initialization
	public void  Submit_Ticket()
    {
        Ticket_Name = "/Ticket_" + UnityEngine.Random.Range(9999999, 0) + ".txt";
        Path = Application.dataPath + "/Tickets" + Ticket_Name;
        string Ticket_Text = Terminal_Summary + Environment.NewLine;
        File.WriteAllText(Path, Store_Number + Environment.NewLine + Customer_Input + Environment.NewLine + Ticket_Text + Environment.NewLine + Environment.NewLine + "Error Code: " + Error_Code + Environment.NewLine + Environment.NewLine + TroubleShooting_Steps + Environment.NewLine + Script_Output + Environment.NewLine + Resolution);

        Command_File_Name = "/Command_Cache.txt";
        Path = Application.dataPath + Command_File_Name;
        string Command_Text = "Ticket " + Ticket_Name + " Created " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine + "Done" + Environment.NewLine;
        File.AppendAllText(Path, Command_Text);

    }

    public void Submit_Command()
    {
        Command_File_Name = "/Command_Cache.txt";
        Path = Application.dataPath + Command_File_Name;

        if (File.Exists(Path) == true)
        {
            string[] lines = File.ReadAllLines(Path);
            if (lines[lines.Length - 1] != "Done")
            {
                Debug.Log("Command Pending");
                string Command_Text = "Command Already Pending " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine + "Done" + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
            }
            else
            {
                string Command_Text = "RefreshTerminals " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
                SceneManager.LoadScene("Loading");
            }
        }else
        {
            string Command_Text = "RefreshTerminals " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
            File.AppendAllText(Path, Command_Text);
            SceneManager.LoadScene("Loading");
        }


    }

    public void Refresh_Terminals()
    {
        Command_File_Name = "/Command_Cache.txt";
        Path = Application.dataPath + Command_File_Name;

        if (File.Exists(Path) == true)
        {
            string[] lines = File.ReadAllLines(Path);
            if (lines[lines.Length - 1] != "Done")
            {
                Debug.Log("Command Pending");
                string Command_Text = "Command Already Pending " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
            }
            else
            {
                string Command_Text = "RefreshTerminals " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
                SceneManager.LoadScene("Loading");
            }
        }
        else
        {
            string Command_Text = "RefreshTerminals " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
            File.AppendAllText(Path, Command_Text);
            SceneManager.LoadScene("Loading");
        }


    }

    public void Refresh_Kitchen()
    {
        Command_File_Name = "/Command_Cache.txt";
        Path = Application.dataPath + Command_File_Name;

        if (File.Exists(Path) == true)
        {
            string[] lines = File.ReadAllLines(Path);
            if (lines[lines.Length - 1] != "Done")
            {
                Debug.Log("Command Pending");
                string Command_Text = "Command Already Pending " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
            }
            else
            {
                string Command_Text = "RefreshKitchen " + "at " + DateTime.Now + " " + Time.realtimeSinceStartup + Environment.NewLine;
                File.AppendAllText(Path, Command_Text);
            }
        }


    }

    public void Scrub_Variables()
    {
        TroubleShooting_Steps = null;
        Terminal_Summary = null;
        Resolution = null;
        Customer_Input = null;
        Error_Code = 0;
        Debug.Log("Scrubbing variable");

    }

}


