using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class POS_Error_Check : MonoBehaviour {

    public string Name;
    public string IP_Address;
    public string Uptime;
    public string Run_Level;
    public string Motherboard;
    public string Motherboard_Manufacturer;
    public string Printer_Configured;
    public string Printer_Device_Node;
    public string Printer_Attached;
    public string Printer_SimLink;
    public string Cash_Drawer_Configured;
    public string Touch_Screen;
    public string Drive_Health;
    public string Printer_Errors;

    private string Packet_Loss_Removal = "% packet loss";
    private string Packet_Loss;
    public int Packet_Loss_int;

    public string Terminal_Summary;

    public GameObject Parent;
    public GameObject Canvas;
    public GameObject Button;

    public string Errors_Detected;

    public bool Error_Detected;

    // Use this for initialization
    void Start() {

        Name = gameObject.GetComponent<POS_Device_Info>().Name;
        IP_Address = gameObject.GetComponent<POS_Device_Info>().IP_Address;
        Uptime = gameObject.GetComponent<POS_Device_Info>().Uptime;
        Run_Level = gameObject.GetComponent<POS_Device_Info>().Run_Level;
        Printer_Configured = gameObject.GetComponent<POS_Device_Info>().Printer_Configured;
        Printer_Device_Node = gameObject.GetComponent<POS_Device_Info>().Printer_Device_Node;
        Printer_Attached = gameObject.GetComponent<POS_Device_Info>().Printer_Attached;
        Printer_SimLink = gameObject.GetComponent<POS_Device_Info>().Printer_SimLink;
        Touch_Screen = gameObject.GetComponent<POS_Device_Info>().Touch_Screen;
        Drive_Health = gameObject.GetComponent<POS_Device_Info>().Drive_Health;
        Printer_Errors = gameObject.GetComponent<POS_Device_Info>().Printer_Errors;

        Packet_Loss = gameObject.GetComponent<POS_Device_Info>().Packet_Loss;
        if (Packet_Loss != "Unable to aquire")
        {
            Packet_Loss = Packet_Loss.Replace(Packet_Loss_Removal, "");
            Packet_Loss_int = int.Parse(Packet_Loss);
        }


        Parent = GameObject.Find(Name);
        Canvas = GameObject.Find("Canvas");

        Error_Detected = false;

        CheckForErrors();
        DisplayButton();
        Summary();


    }


    public void CheckForErrors()
    {
        if (Packet_Loss_int > 20 || Packet_Loss == "Unable to aquire")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 1;
            Errors_Detected=("Packet Loss Error Detected on " + Name);
            return;
        }



        if (Drive_Health != "No Issues Detected" && Drive_Health != "Unable to aquire")
        {
            /* will need to wait until API is set up to check if a ticket for the issue alread exists bofore creating*/

            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 2;
            Errors_Detected =("Drive Error Detected on " + Name);

            GameObject dataSaver = GameObject.Find("Data_Save");
            Summary();

            dataSaver.GetComponent<Ticket_Format>().Contact_Name = "System Generated Ticket";
            dataSaver.GetComponent<Ticket_Format>().AutoTicket = true;
            dataSaver.GetComponent<Save_Data>().Error_Code = 2;
            dataSaver.GetComponent<Save_Data>().Name = Name;

            dataSaver.GetComponent<Ticket_Format>().AutoTicketNotes =
                "Warning: " + "I/O Errors have been detected on " + Name +
                ". I/O errors are the result of damage to the internal drive of the computer and will require a reaplcement terminal." +
                Environment.NewLine + Environment.NewLine +
                "A ticket was automatically created and submitted as soon as this error was detected. For more information please contact the Help Desk" +
                Environment.NewLine + Environment.NewLine +
                Terminal_Summary;

            dataSaver.GetComponent<Save_Data>().Submit_Ticket();
            dataSaver.GetComponent<Ticket_Format>().AutoTicket = false;
        }

        if (Printer_Errors == "Printer out of Paper")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 11;
            Errors_Detected =("Printer out of paper on " + Name);
        }

        if (Printer_Errors == "Printer Port Error Detected")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 3;
            Errors_Detected = ("Printer Port Error Detected on " + Name);
        }

        if (Run_Level != "4")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 4;
            Errors_Detected =("Run Level Error Detected on " + Name);
        }



        if (Printer_Configured == "Yes" && Printer_Attached != "None")
        {
            if (Printer_Device_Node != Printer_SimLink && Printer_Device_Node != "/dev/rcprinter")
            {
                Error_Detected = true;
                gameObject.GetComponent<Error_Codes>().Error_Code_Number = 5;
                Errors_Detected =("Printer found as " + Printer_Attached + " configured as " + Printer_Device_Node + " but Simlink set to " + Printer_SimLink + " on " + Name);

            }
        }


        if (Printer_Configured == "Yes" && Printer_Attached == "None")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 6;
            Errors_Detected =("Printer is configured but not attached on " + Name);

        }


        if (Printer_Configured == "No" && Printer_Attached != "None")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 7;
            Errors_Detected =("Printer is attached as " + Printer_Attached + "but not configured for use on " + Name);

        }

        if (Touch_Screen == "No Touch Screen Detected")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 8;
            Errors_Detected = ("No Touch Screen Detected " + Name);
        }





    }


    public void DisplayButton()
    {
        if (Error_Detected == true)
        {
            Button = Instantiate(Resources.Load("Error_Button") as GameObject);
            Button.transform.SetParent(Canvas.transform);
            Button.transform.position = Parent.transform.position;
            Button.name = ("Error_" + Name);
            Button.GetComponent<Parent>().Parent_Object = gameObject;

        }

    }

    void Summary()
    {
        Terminal_Summary =

    "Name: " + Name + Environment.NewLine +
    "IP Address: " + IP_Address + Environment.NewLine +
    "Uptime: " + Uptime + Environment.NewLine +
    "Run_Level: " + Run_Level + Environment.NewLine +
    "Motherboard: " + gameObject.GetComponent<POS_Device_Info>().Motherboard + Environment.NewLine +
    "Motherboard Manufacturer: " + gameObject.GetComponent<POS_Device_Info>().Motherboard_Manufacturer + Environment.NewLine +
    "Printer Configured?: " + Printer_Configured + Environment.NewLine +
    "Printer Device Node: " + Printer_Device_Node + Environment.NewLine +
    "Printer Attached: " + Printer_Attached + Environment.NewLine +
    "Printer SimLink: " + Printer_SimLink + Environment.NewLine +
    "Cash Drawer Configured: " + gameObject.GetComponent<POS_Device_Info>().Cash_Drawer_Configured + Environment.NewLine +
    "Touch Scrren: " + Touch_Screen + Environment.NewLine +
    "Drive Health: " + Drive_Health + Environment.NewLine +
    "Printer Errors: " + Printer_Errors + Environment.NewLine +
    "Packet Loss: " + Packet_Loss_int + Environment.NewLine;


}
}