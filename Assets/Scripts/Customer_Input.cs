using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Customer_Input : MonoBehaviour {
    public string Name;
    public string Contact_Info;
    public string Ticket_Content;

    public GameObject Ticket_Panel;
    public GameObject Data_Saver;
    public GameObject Panel;

    public GameObject Ticket_Submission_Error_Panel;
    public string Ticket_Error;

	void Start() {
        Ticket_Panel = GameObject.Find("Ticket");
        Data_Saver = GameObject.Find("Data_Save");
	
	}

	public void Get_Name(InputField nameField)
    {
        Name = "Name: " + nameField.text;
	}

    public void Get_Contact_Info(InputField contactField)
    {
        Contact_Info = "Best Method for Contact: " + contactField.text;
    }

    public void Get_Ticket_Content(InputField contentField)
    {
        Ticket_Content = "Issue Summary: " + contentField.text;
    }

    public void Close_Pannel()
    {
        Destroy(Ticket_Panel, 0f);
    }

    public void Save_Ticket_Info()
    {
        bool NameNull = false;
        bool ContactNull = false;

        if (String.IsNullOrEmpty(Name))
        {
            NameNull = true;
            Ticket_Error = "Name field cannot be blank";
        }

        if (String.IsNullOrEmpty(Contact_Info))
        {
            ContactNull = true;
            Ticket_Error = Ticket_Error + Environment.NewLine + "Contact field cannot be blank";
        }

        if (NameNull == false && ContactNull == false)
        {
            Data_Saver.GetComponent<Ticket_Format>().Contact_Name = Name;
            Data_Saver.GetComponent<Ticket_Format>().Contact_info_From_User = Contact_Info;
            Data_Saver.GetComponent<Ticket_Format>().Issue_Summary_From_User = Ticket_Content;

            Data_Saver.GetComponent<Save_Data>().Customer_Input = Name + Environment.NewLine + Contact_Info + Environment.NewLine + Ticket_Content + Environment.NewLine;
            Data_Saver.GetComponent<Save_Data>().Terminal_Summary = Panel.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
            Data_Saver.GetComponent<Save_Data>().Submit_Ticket();
            Data_Saver.GetComponent<Save_Data>().Scrub_Variables();
            Destroy(Panel, 0f);
            Destroy(Ticket_Panel, 0f);
        }
        else
        {
            Ticket_Submission_Error_Panel = Instantiate(Resources.Load("Ticket_Errors") as GameObject);
            Ticket_Submission_Error_Panel.name = "Ticket_Errors";
            Ticket_Submission_Error_Panel.transform.SetParent(Ticket_Panel.transform, false);
        }
    }

}
