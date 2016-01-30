using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Ticket_Format : MonoBehaviour {
    public GameObject Data_Saver;

    public string Path;
    public string Ticket_Name;

    public int Error_Code;
    public string Error_Code_Summary;

    public string Device;

    public string Contact_info_From_User;
    public string Issue_Summary_From_User;

    public string Store_Number;
    public string Customer_fName;
    public string Customer_lName;
    public string Contact_Name;
    public string Incident_Main_Summary;
    public string Incident_Main_Notes;
    public string Service_Type;
    public string Work_Detail_Summary;
    public string Work_Detail_Notes;
    public string Assigned_Company;
    public string Assigned_Organization;
    public string Assigned_Group;
    public string Incident_Status;
    public string Incident_Number;
    public string OpCat1;
    public string OpCat2;
    public string OpCat3;
    public string ProdCat1;
    public string ProdCat2;
    public string ProdCat3;
    public string ProdName;
    public string ResCat1;
    public string ResCat2;
    public string ResCat3;
    public string ResProdName;
    public string Resolution;

    public bool AutoTicket;
    public string AutoTicketNotes;

    // Use this for initialization
    void Start () {
        Data_Saver = GameObject.Find("Data_Save");


    }

    public void Format_Ticket()
    {
        Ticket_Name = "/Ticket_" + UnityEngine.Random.Range(9999999, 0) + ".txt";
        Path = Application.dataPath + "/Tickets" + Ticket_Name;

        Error_Code_Text();

        Store_Number = Data_Saver.GetComponent<Save_Data>().Store_Number;
        Customer_lName = Store_Number;
        Customer_fName = "Pizza Hut";

        Incident_Main_Notes = "";

        Service_Type = "User Service Restoration";

        Assigned_Company = "Pizza Hut";
        Assigned_Organization = "PHI";
        Assigned_Group = "PHI Help Desk L1";

        OpCat1 = "Failed";
        OpCat2 = "N/A";
        OpCat3 = "N/A";

        ProdCat1 = "Above Store";
        ProdCat2 = "Automation";
        ProdCat3 = "Monitoring";
        ProdName = "Alerts";

        if (AutoTicket != true)
        {
            Work_Detail_Notes =
            Contact_info_From_User + Environment.NewLine + Environment.NewLine +
            Issue_Summary_From_User + Environment.NewLine + Environment.NewLine +
            Data_Saver.GetComponent<Save_Data>().Terminal_Summary + Environment.NewLine + Environment.NewLine +
            Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps + Environment.NewLine;
        }

        if (AutoTicket == true)
        {
            Work_Detail_Notes = AutoTicketNotes;
        }

        if (Incident_Status != "Resolved")
        {
            ResCat1 = "";
            ResCat2 = "";
            ResCat3 = "";
            ResProdName = "";
            Resolution = "";
        }
        else
        {
            ResCat1 = "Above Store";
            ResCat2 = "Automation";
            ResCat3 = "Monitoring";
            ResProdName = "Alerts";
            Resolution = "Resolved by utility";
        }

        File.WriteAllText(Path,
            "[Store Number]: " + Environment.NewLine + Store_Number + Environment.NewLine + Environment.NewLine +

            "[Customer First Name]: " + Environment.NewLine + Customer_fName + Environment.NewLine + Environment.NewLine +
            "[Customer Last Name]: " + Environment.NewLine + Customer_lName + Environment.NewLine + Environment.NewLine +

            "[Contact Name]: " + Environment.NewLine + Contact_Name + Environment.NewLine + Environment.NewLine +

            "[Incident Main Summary]: " + Environment.NewLine + Incident_Main_Summary + Environment.NewLine + Environment.NewLine +
            "[Incident Main Notes]: " + Environment.NewLine + Incident_Main_Notes + Environment.NewLine + Environment.NewLine +

            "[Service Type]: " + Environment.NewLine + Service_Type + Environment.NewLine + Environment.NewLine +

            "[Work Detail Section - Summary]: " + Environment.NewLine + Work_Detail_Summary + Environment.NewLine + Environment.NewLine +
            "[Work Detail Section - Notes]: " + Environment.NewLine + Work_Detail_Notes + Environment.NewLine + Environment.NewLine +

            "[Assigned Company]: " + Environment.NewLine + Assigned_Company + Environment.NewLine + Environment.NewLine +
            "[Assigned Organization]: " + Environment.NewLine + Assigned_Organization + Environment.NewLine + Environment.NewLine +
            "[Assigned Group]: " + Environment.NewLine + Assigned_Group + Environment.NewLine + Environment.NewLine +

            "[Incident Status]: " + Environment.NewLine + Incident_Status + Environment.NewLine + Environment.NewLine +
            "[Incident Number]: " + Environment.NewLine + Incident_Number + Environment.NewLine + Environment.NewLine +

            "[Operational Categorization Tier 1]: " + Environment.NewLine + OpCat1 + Environment.NewLine + Environment.NewLine +
            "[Operational Categorization Tier 2]: " + Environment.NewLine + OpCat2 + Environment.NewLine + Environment.NewLine +
            "[Operational Categorization Tier 3]: " + Environment.NewLine + OpCat3 + Environment.NewLine + Environment.NewLine +

            "[Product Categorization Tier 1]: " + Environment.NewLine + ProdCat1 + Environment.NewLine + Environment.NewLine +
            "[Product Categorization Tier 2]: " + Environment.NewLine + ProdCat2 + Environment.NewLine + Environment.NewLine +
            "[Product Categorization Tier 3]: " + Environment.NewLine + ProdCat3 + Environment.NewLine + Environment.NewLine +
            "[Product Name]: " + Environment.NewLine + ProdName + Environment.NewLine + Environment.NewLine +

            "[Resolution Product Categorization Tier 1]: " + Environment.NewLine + ResCat1 + Environment.NewLine + Environment.NewLine +
            "[Resolution Product Categorization Tier 2]: " + Environment.NewLine + ResCat2 + Environment.NewLine + Environment.NewLine +
            "[Resolution Product Categorization Tier 3]: " + Environment.NewLine + ResCat3 + Environment.NewLine + Environment.NewLine +
            "[Resolution Product Name]: " + Environment.NewLine + ResProdName + Environment.NewLine + Environment.NewLine +

            "[Resolution]: " + Environment.NewLine + Resolution + Environment.NewLine + Environment.NewLine );


    }

    public void Error_Code_Text()
    {
        Error_Code = Data_Saver.GetComponent<Save_Data>().Error_Code;

        switch (Error_Code)
        {
            case 0:
                Error_Code_Summary = "System Appears to be operating normally";
                break;

            case 1:
                Error_Code_Summary = "Packet Loss.";
                break;

            case 2:
                Error_Code_Summary = "Input,Output errors on Hard Drive. ";
                break;

            case 3:
                Error_Code_Summary = "Printer Port Errors. ";
                break;

            case 4:
                Error_Code_Summary = " Run Level is not '4'. ";
                break;

            case 5:
                Error_Code_Summary = "Printer Simlink issues. ";
                break;

            case 6:
                Error_Code_Summary = "Printer is Configured but not attached. ";
                break;

            case 7:
                Error_Code_Summary = "Printer Attached but is not configured. ";
                break;

            case 8:
                Error_Code_Summary = "Touch Screen not detected. ";
                break;

            case 9:
                Error_Code_Summary = "Kitchen Device Status is not 'LIVE'. ";
                break;

            case 10:
                Error_Code_Summary = "No Bump Bar Attached to KMX device. ";
                break;

            case 11:
                Error_Code_Summary = "Error #11: Printer out of Paper. ";
                break;
        }

        if (Error_Code != 0)
        {
            Device = Data_Saver.GetComponent<Save_Data>().Name;
            Incident_Main_Summary = Device + " " + Error_Code_Summary;
            Work_Detail_Summary = Incident_Main_Summary;
        }
        else
        {
            Incident_Main_Summary = "User Reported Issue on " + Device;
            Work_Detail_Summary = Incident_Main_Summary;
        }

    }
}
