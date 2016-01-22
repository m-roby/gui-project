using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class KMX_Error_Check : MonoBehaviour {

    public string Name;
    public string Address;
    public string Virtual_Device_1;
    public string Virtual_Device_2;
    public string Status;
    public string Bump_Bar;

    private string Packet_Loss_Removal = "% packet loss";
    private string Packet_Loss_String;
    public int Packet_Loss_int;

    public bool Error_Detected;
    public string Errors_Detected;

    public GameObject Button;
    public GameObject Parent;
    public GameObject Canvas;

    public string Terminal_Summary;


    // Use this for initialization
    void Start () {

        Name = gameObject.GetComponent<Kitchen_Device_Info>().Name;
        Address = gameObject.GetComponent<Kitchen_Device_Info>().Address;
        Virtual_Device_1 = gameObject.GetComponent<Kitchen_Device_Info>().Virtual_Device_1;
        Virtual_Device_2 = gameObject.GetComponent<Kitchen_Device_Info>().Virtual_Device_2;
        Status = gameObject.GetComponent<Kitchen_Device_Info>().Status;

        Packet_Loss_String = gameObject.GetComponent<Kitchen_Device_Info>().Packet_Loss;

        if (Packet_Loss_String != "Non-IP Device" && Packet_Loss_String != "Unable to aquire")
        {
            Packet_Loss_String = Packet_Loss_String.Replace(Packet_Loss_Removal, "");
            Packet_Loss_int = int.Parse(Packet_Loss_String);
        }

        Bump_Bar = gameObject.GetComponent<Kitchen_Device_Info>().Bump_Bar;

        Parent = GameObject.Find(Name);
        Canvas = GameObject.Find("Canvas");

        CheckForErrors();
        DisplayButton();
        Summary();
     

    }

    public void CheckForErrors()
    {
        if (Status != "LIVE")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 9;
            Errors_Detected = ("Non-Live status on device " + Name);

        }

        if (Packet_Loss_String != "Non-IP Device" || Packet_Loss_String != "Unable to aquire")
        {
            if (Packet_Loss_int > 20)
            {
                Error_Detected = true;
                gameObject.GetComponent<Error_Codes>().Error_Code_Number = 1;
                Errors_Detected = (Errors_Detected + "\n" + " Packet Loss Error Detected on " + Name);
            }
        }


       if (Packet_Loss_String == "Unable to aquire")
       {
          Error_Detected = true;
           gameObject.GetComponent<Error_Codes>().Error_Code_Number = 1;
            Errors_Detected = (Errors_Detected + "\n" + " Packet Loss Error Detected on " + Name);
       }


        if (Bump_Bar == "None")
        {
            Error_Detected = true;
            gameObject.GetComponent<Error_Codes>().Error_Code_Number = 10;
            Errors_Detected = (Errors_Detected + "\n" + " No bump bar connected to " + Name);
        }

    }



    public void DisplayButton()
    {
        if (Error_Detected == true)
        {
            Button = Instantiate(Resources.Load("Error_Button") as GameObject);
            Button.transform.SetParent(Canvas.transform);
            Button.transform.position = Parent.transform.position;
            Button.GetComponent<Parent>().Parent_Object = gameObject;
            Button.name = ("Error_" + Name);

        }

    }

    public void Summary()
    {
        Terminal_Summary =

        "Name: " + Name + Environment.NewLine +
        "Address: " + Address + Environment.NewLine +
        "Virtual_Device 1: " + Virtual_Device_1 + Environment.NewLine +
        "Virtual_Device_2: " + Virtual_Device_2 + Environment.NewLine +
        "Status: " + Status + Environment.NewLine +
        "Bump_Bar: " + Bump_Bar + Environment.NewLine +
        "Packet_Loss: " + Packet_Loss_int + Environment.NewLine;
    }

}
