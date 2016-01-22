using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Click_Behaviors : MonoBehaviour
{
    public GameObject Data_Saver;
    public GameObject Canvas;
    public GameObject Panel;
    public GameObject Terminal;
    public string Terminal_Name;
    string Error_Remove = "Error_";
    public Text Text;

    public GameObject Help_Button;
    public GameObject Error_Help_Button;

    public GameObject Terminal_Image;

    public int Clicks;
    public int Number_Of_Steps;

    void OnEnable()
    {
        Data_Saver = GameObject.Find("Data_Save");
    }

    public void Click_Error_Button()
    {
        Data_Saver.GetComponent<Save_Data>().Scrub_Variables();
        Terminal_Name = gameObject.transform.gameObject.name;
        Terminal_Name = Terminal_Name.Replace(Error_Remove, "");
        Terminal = gameObject.GetComponent<Parent>().Parent_Object;
        Panel = Instantiate(Resources.Load("Panel") as GameObject);
        Panel.transform.SetParent(Canvas.transform, false);
        Panel.name = "Panel";
        Text = Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>();
        Terminal.GetComponent<Error_Codes>().GetText();

        if (Terminal.GetComponent<POS_Error_Check>() != null)
        {
            Text.text = (Terminal.GetComponent<POS_Error_Check>().Errors_Detected + "\n" + Terminal.GetComponent<Error_Codes>().Error_Text);
        }
        else
        {
            Text.text = (Terminal.GetComponent<KMX_Error_Check>().Errors_Detected + "\n" + Terminal.GetComponent<Error_Codes>().Error_Text);
        }

        Error_Help_Button = Instantiate(Resources.Load("Error_Help_Button") as GameObject);
        Error_Help_Button.transform.SetParent(Panel.transform, false);
        Error_Help_Button.GetComponent<Parent>().Parent_Object = Panel;
        Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button = Error_Help_Button;
        Error_Help_Button.name = "Error_Help_Button";

        Panel.GetComponent<Pannel_Info_Storage>().Terminal = Terminal;

    }

    public void Click_Info_Button()
    {
        Data_Saver.GetComponent<Save_Data>().Scrub_Variables();
        Terminal_Name = gameObject.transform.parent.gameObject.name;
        Terminal = gameObject.GetComponent<Parent>().Parent_Object;
        Panel = Instantiate(Resources.Load("Panel") as GameObject);
        Panel.transform.SetParent(Canvas.transform, false);
        Panel.name = "Panel";
        Text = Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>();

        if (Terminal.GetComponent<POS_Device_Info>() != null)
        {
            Text.text = ("Name : " + Terminal.GetComponent<POS_Device_Info>().Name + "\n" +
                         "IP Address : " + Terminal.GetComponent<POS_Device_Info>().IP_Address + "\n" +
                         "Packet Loss : " + Terminal.GetComponent<POS_Device_Info>().Packet_Loss + "\n" +
                         "Uptime : " + Terminal.GetComponent<POS_Device_Info>().Uptime + "\n" +
                         "Run Level : " + Terminal.GetComponent<POS_Device_Info>().Run_Level + "\n" +
                         "Printer Configured? : " + Terminal.GetComponent<POS_Device_Info>().Printer_Configured + "\n" +
                         "Printer Attached? : " + Terminal.GetComponent<POS_Device_Info>().Printer_Attached + "\n" +
                         "Cash Drawer Configured? : " + Terminal.GetComponent<POS_Device_Info>().Cash_Drawer_Configured + "\n" +
                         "Touch Screen : " + Terminal.GetComponent<POS_Device_Info>().Touch_Screen);

            Terminal_Image = Panel.GetComponent<Pannel_Info_Storage>().Step_Image;
            Terminal_Image.GetComponent<Image>().sprite = Terminal.GetComponent<POS_Image_Changer>().Image;
        }
        else
        {
            Text.text = ("Name : " + Terminal.GetComponent<Kitchen_Device_Info>().Name + "\n" +
                         "Address : " + Terminal.GetComponent<Kitchen_Device_Info>().Address + "\n" +
                         "Virtual Device 1 : " + Terminal.GetComponent<Kitchen_Device_Info>().Virtual_Device_1 + "\n" +
                         "Virtual Device 2 : " + Terminal.GetComponent<Kitchen_Device_Info>().Virtual_Device_2 + "\n" +
                         "Status : " + Terminal.GetComponent<Kitchen_Device_Info>().Status + "\n" +
                         "Packet Loss : " + Terminal.GetComponent<Kitchen_Device_Info>().Packet_Loss + "\n" +
                         "Bump Bar : " + Terminal.GetComponent<Kitchen_Device_Info>().Bump_Bar);

            Terminal_Image = Panel.GetComponent<Pannel_Info_Storage>().Step_Image;
            Terminal_Image.GetComponent<Image>().sprite = Terminal.GetComponent<KMX_Image_Changer>().Image;

        }

        Help_Button = Instantiate(Resources.Load("Help_Button") as GameObject);
        Help_Button.transform.SetParent(Panel.transform, false);
        Help_Button.GetComponent<Parent>().Parent_Object = Panel;
        Panel.GetComponent<Pannel_Info_Storage>().Help_Button = Help_Button;
        Panel.GetComponent<Pannel_Info_Storage>().Terminal = Terminal;

    }

    public void Click_Close_Button()
    {
        Panel = gameObject.GetComponent<Parent>().Parent_Object;
        Destroy(Panel);

    }

    public void Click_Error_Help_Button()
    {

        Panel = GameObject.Find("Panel");
        if (Panel.GetComponent<Solutions>().TS_Finished != true)
        {
            ++Clicks;
            Panel.GetComponent<Solutions>().Clicks = Clicks - 1;
        }
        if (Panel.GetComponent<Solutions>().TS_Finished == true)
        {
            //Data_Saver.GetComponent<Save_Data>().Submit_Ticket();
            //Destroy(Panel);
            GameObject Confirmation;
            Confirmation = Instantiate(Resources.Load("Confirmation") as GameObject);
            Confirmation.transform.SetParent(Panel.transform, false);
            Confirmation.name = "Confirmation";
            Confirmation.GetComponent<Parent>().Parent_Object = Panel;
        }





            switch (Clicks)
        {
            case 1:

                Panel.GetComponent<Solutions>().Last_Start_time = Panel.GetComponent<Solutions>().Start_Time;
                Panel.GetComponent<Solutions>().Start_Time = Time.time;


                Terminal = Panel.GetComponent<Pannel_Info_Storage>().Terminal;
                Panel.GetComponent<Solutions>().GetText();
                Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>().text = Panel.GetComponent<Solutions>().Solution_Text;

                Error_Help_Button = Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
                Number_Of_Steps = Panel.GetComponent<Solutions>().Number_Of_Steps;


                if (Panel.GetComponent<Solutions>().TS_Finished == true)
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Done";
                }
                else
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Next Step";
                }

                break;

            case 2:
                Panel.GetComponent<Solutions>().Last_Start_time = Panel.GetComponent<Solutions>().Start_Time;
                Panel.GetComponent<Solutions>().Start_Time = Time.time;



                Terminal = Panel.GetComponent<Pannel_Info_Storage>().Terminal;
                Panel.GetComponent<Solutions>().GetText();
                Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>().text = Panel.GetComponent<Solutions>().Solution_Text;

                Error_Help_Button = Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
                Number_Of_Steps = Panel.GetComponent<Solutions>().Number_Of_Steps;

                if (Panel.GetComponent<Solutions>().TS_Finished == true)
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Done";
                }
                else
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Next Step";
                }

                break;


            case 3:

                Panel.GetComponent<Solutions>().Last_Start_time = Panel.GetComponent<Solutions>().Start_Time;
                Panel.GetComponent<Solutions>().Start_Time = Time.time;

                Terminal = Panel.GetComponent<Pannel_Info_Storage>().Terminal;
                Panel.GetComponent<Solutions>().GetText();
                Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>().text = Panel.GetComponent<Solutions>().Solution_Text;

                Error_Help_Button = Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
                Number_Of_Steps = Panel.GetComponent<Solutions>().Number_Of_Steps;

                    if (Panel.GetComponent<Solutions>().TS_Finished == true)
                    {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Done";
                    }
                    else
                    {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Next Step";
                    }

                break;


            case 4:

                Panel.GetComponent<Solutions>().Last_Start_time = Panel.GetComponent<Solutions>().Start_Time;
                Panel.GetComponent<Solutions>().Start_Time = Time.time;

                Terminal = Panel.GetComponent<Pannel_Info_Storage>().Terminal;
                Panel.GetComponent<Solutions>().GetText();
                Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>().text = Panel.GetComponent<Solutions>().Solution_Text;

                Error_Help_Button = Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
                Number_Of_Steps = Panel.GetComponent<Solutions>().Number_Of_Steps;

                if (Panel.GetComponent<Solutions>().TS_Finished == true)
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Done";
                }
                else
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Next Step";
                }

                break;


            case 5:

                Panel.GetComponent<Solutions>().Last_Start_time = Panel.GetComponent<Solutions>().Start_Time;
                Panel.GetComponent<Solutions>().Start_Time = Time.time;

                Terminal = Panel.GetComponent<Pannel_Info_Storage>().Terminal;
                Panel.GetComponent<Solutions>().GetText();
                Panel.GetComponent<Pannel_Info_Storage>().Text.GetComponent<Text>().text = Panel.GetComponent<Solutions>().Solution_Text;

                Error_Help_Button = Panel.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
                Number_Of_Steps = Panel.GetComponent<Solutions>().Number_Of_Steps;

                if (Panel.GetComponent<Solutions>().TS_Finished == true)
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Done";
                }
                else
                {
                    Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>().text = "Next Step";
                }



                break;
        }


    }

    public void Click_Help_Button()
    {
        Panel = GameObject.Find("Panel");

        GameObject Ticket;
        Ticket = Instantiate(Resources.Load("Ticket") as GameObject);
        Ticket.transform.SetParent(Panel.transform, false);
        Ticket.name = "Ticket";
        Ticket.GetComponent<Customer_Input>().Panel = Panel;

    }

    public void More_Issues_No()
    {
        Data_Saver.GetComponent<Save_Data>().Resolution = "Issue Resolved";
        Data_Saver.GetComponent<Save_Data>().Submit_Ticket();
        Data_Saver.GetComponent<Save_Data>().Scrub_Variables();
        Data_Saver.GetComponent<Save_Data>().Refresh_Terminals();
    }

    public void More_Issues_Yes()
    {
        GameObject Ticket;
        GameObject Confirmation = gameObject.GetComponent<Parent>().Parent_Object;
        Panel = Confirmation.GetComponent<Parent>().Parent_Object;
        Data_Saver.GetComponent<Save_Data>().Resolution = "Unresolved";
        Ticket = Instantiate(Resources.Load("Ticket") as GameObject);
        Ticket.transform.SetParent(Panel.transform, false);
        Ticket.name = "Ticket";
        Ticket.GetComponent<Customer_Input>().Panel = Panel;
        Destroy(Confirmation, 0f);
    }

    void Start()
    {
        Canvas = GameObject.Find("Canvas");


    }

}
