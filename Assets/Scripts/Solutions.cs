using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class Solutions : MonoBehaviour {
    public GameObject Data_Saver;
    public GameObject Error_Help_Button;
    public Text Error_Button_Text;

    public int Error_Code_Number;

    public int Number_Of_Steps;
    public string Solution_Text;

    public string[] Step_Text;
    public Sprite[] Step_Image = null;
    public float[] Time_between_Clicks = null;

    public float Start_Time;
    public float Last_Start_time;

    public GameObject Image;

    public int Clicks;
    public bool TS_Finished;

    public bool Script_Running;
    public bool has_reset;

    void Start()
    {
        if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>() != null)
        {
            Image.GetComponent<Image>().sprite = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
        }
        else
        {
            Image.GetComponent<Image>().sprite = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
        }


        Time_between_Clicks = new float[10];
        Data_Saver = GameObject.Find("Data_Save");


        if (gameObject.GetComponent<Pannel_Info_Storage>().Error_Help_Button != null)
        {
            Error_Help_Button = gameObject.GetComponent<Pannel_Info_Storage>().Error_Help_Button;
            Error_Button_Text = Error_Help_Button.GetComponent<Child>().Child_Object.GetComponent<Text>();
        }

    }


    public void GetText() {


        switch (Error_Code_Number)
        {

            case 2:  /*IO Errors*/
                Number_Of_Steps = 1;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];

                Step_Image[0] = Resources.Load<Sprite>("Sprites/Error_1");



                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;

                Step_Text[0] = "Warning" + "\n" + "I/O Errors have been detected on " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + ". I/O errors are the result of damage to the internal drive of the computer and will require a reaplcement terminal." + Environment.NewLine + Environment.NewLine + "A ticket was automatically created and submitted as soon as this error was detected. For more information please contact the Help Desk";          

                if (Clicks <= (Step_Text.Length - 1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }
                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished");
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine;
                }

                break;

            case 3:  /*Printer port Errors*/
                Number_Of_Steps = 2;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Motherboard.Contains("D2-CPU"))
                {
                    Step_Image[0] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Back_KMX");
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Front");
                }
                else
                {
                    Step_Image[0] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Front");
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Ports");

                }


                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;

                Step_Text[0] = "Step 1:" + "\n" + "Locate Terminal " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + " and it's attached printer.";
                Step_Text[1] = "Step 2:" + "\n" + "Power off the printer and disconnect the power cable from the back. Wait for 1 minute and then Reinsert the power cable.";

                if (Clicks <= (Step_Text.Length - 1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }
                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished");
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine +
                        Step_Text[1] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[1] + Environment.NewLine + Environment.NewLine;
                }

                break;


            case 6:  /*Printer attached but not configured*/
                Number_Of_Steps = 5;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Motherboard.Contains("D2-CPU"))
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Back_KMX");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Front");
                    Step_Image[3] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Back_Image;

                }
                else
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Front");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Ports");
                    Step_Image[3] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Back_Image;
                }


                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;

                Step_Text[0] = "If this terminal does NOT have a printer attached, close this window and contact the helpdesk.";
                Step_Text[1] = "Step 1:" + "\n" + "Locate Terminal " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + " and it's attached printer.";
                Step_Text[2] = "Step 2:" + "\n" + "Power off the printer, and disconnect the data cable from the back. wait 30 seconds and then reattach. Do NOT power the printer back on";
                Step_Text[3] = "Step 3:" + "\n" + "Remove the printer data cable from the back of the terminal, wait 30 seconds, then reattach. You may now power on the printer.";

                if (Clicks <= (Step_Text.Length - 1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }
                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished");
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine +
                        Step_Text[1] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[1] + Environment.NewLine + Environment.NewLine +
                        Step_Text[2] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[2] + Environment.NewLine + Environment.NewLine +
                        Step_Text[3] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[3] + Environment.NewLine + Environment.NewLine;

                }

                break;







            case 8: /*Touch Screen Not Connected*/
                Number_Of_Steps = 3;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];


                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Terminal_Type == "Optiplex_XE")
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/ELO_Touch_Bottom_Inputs");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/Optiplex_XE_Back_POS");
                }

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Terminal_Type == "VXL")
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/ELO_Touch_Bottom_Inputs");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/VXL_Back");
                }

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Terminal_Type == "Beetle")
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/3M_Microtouch_Ports");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/Beetle_PC_Back");
                }

                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;

                Step_Text[0] = "Step 1:" + "\n" + "Locate Terminal " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + " and it's Touch Screen. Turn the power to both devices off." + "\n";
                Step_Text[1] = "Step 2:" + "\n" + "On the back or bottom of the touch screen, there is a USB port that should connect the touch screen to the computer. Unplug this cable from the monitor, wait for 30 seconds and then plug it back in." + "\n" + "If this cable is missing, please attempt to locate it and plug it back in." + "\n";
                Step_Text[2] = "Step 3:" + "\n" + "On the back of the terminal, the other end of the USB cable should be plugged in to a USB port. Please disconnect the cable for 30 seconds and then reconnect it. If the cable is NOT connected, plug it in to an available USB port." + "\n" + "You may now power the monitor and terminal back on." + "\n";

                if (Clicks <= (Step_Text.Length - 1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }
                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished");
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine +
                        Step_Text[1] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[1] + Environment.NewLine + Environment.NewLine +
                        Step_Text[2] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[2] + Environment.NewLine + Environment.NewLine;
                }

                break;




            case 9: /* non live kitchen Device */

                Number_Of_Steps = 5;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<Kitchen_Device_Info>().Bump_Bar == "Non-bump device")
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/EpsonTmt88_KMX");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/EpsonTmt88_KMX");
                    Step_Image[3] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                    Step_Image[4] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                }
                else
                {
                    Step_Image[0] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/VXL_Back");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/VXL_Back");
                    Step_Image[3] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                    Step_Image[4] = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<KMX_Image_Changer>().Image;
                }

                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;

                Step_Text[0] = "Step 1:" + "\n" + "Locate Kitchen Device " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + " and power it off" + "\n";
                Step_Text[1] = "Step 2:" + "\n" + "On the back of the device, there will be a network cable attached. Please disconnect the network cable for 30 seconds and then reconnect. Do not power the Device back on." + "\n";
                Step_Text[2] = "Step 3:" + "\n" + "Follow the network cable (if possible) to where it connects to a wall outlet. Disconnect the network cable from the outlet for 30 seconds then reconnect it" + "\n";
                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<Kitchen_Device_Info>().Bump_Bar == "Non-bump device")
                {
                    Step_Text[3] = "Step 4:" + "\n" + "Verify printer has a sufficient supply of paper, then power the device back on." + "\n";
                }
                else
                {
                    Step_Text[3] = "Step 4:" + "\n" + "You may now power on the device" + "\n" + "When Device has finished powering on, press the 'Done' button, and we will attempt to restore connectivity to this device";
                }


                Step_Text[4] = "Step 5:" + "\n" + "Please remain patient while we reset your device... " + "\n";



                if (Clicks <= (Step_Text.Length - 1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }

                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished" + Step_Text.Length + Clicks);
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine +
                        Step_Text[1] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[1] + Environment.NewLine + Environment.NewLine +
                        Step_Text[2] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[2] + Environment.NewLine + Environment.NewLine +
                        Step_Text[3] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[3] + Environment.NewLine + Environment.NewLine +
                        Step_Text[4] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[4] + Environment.NewLine + Environment.NewLine;

                    if (has_reset == false)
                    {
                        Data_Saver.GetComponent<Save_Data>().Refresh_Kitchen();
                        InvokeRepeating("Reset_Progress_Check", 0f, 1f);
                    }
                }

                
                break;



            case 11:  /*Printer out of paper*/
                Number_Of_Steps = 3;
                Step_Text = new string[Number_Of_Steps];
                Step_Image = new Sprite[Number_Of_Steps];


                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Printer_Attached.Contains("Serial"))
                {
                    Step_Image[0] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Front");
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Front");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/Wincor_aiohm_Front");
                    Debug.Log(gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Printer_Attached);
                }

                if (gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Printer_Attached.Contains("USB"))
                {
                    Step_Image[0] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Front");
                    Step_Image[1] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Front");
                    Step_Image[2] = Resources.Load<Sprite>("TSImages/EpsonTmt88_USB_Front");
                    Debug.Log(gameObject.GetComponent<Pannel_Info_Storage>().Terminal.GetComponent<POS_Device_Info>().Printer_Attached);
                }


                Time_between_Clicks[Clicks] = Start_Time - Last_Start_time;


                Step_Text[0] = "Step 1:" + "\n" + "Locate Terminal " + gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name + " and it's attached printer.";
                Step_Text[1] = "Step 2:"  + "\n" + "Open the printer and replace current paper roll with new roll, then close the lid.";
                Step_Text[2] = "Step 3:" + "\n" + "press the Feed button and verify printer able to feed paper";

                if (Clicks <= (Step_Text.Length -1))
                {
                    Solution_Text = Step_Text[Clicks];
                    Image.GetComponent<Image>().sprite = Step_Image[Clicks];
                }
                if (Clicks == (Step_Text.Length - 1))
                {
                    TS_Finished = true;
                    Debug.Log("TS Finished");
                    Data_Saver.GetComponent<Save_Data>().Name = gameObject.GetComponent<Pannel_Info_Storage>().Terminal.name;
                    Data_Saver.GetComponent<Save_Data>().Terminal_Summary = gameObject.GetComponent<Pannel_Info_Storage>().Terminal_Summary;
                    Data_Saver.GetComponent<Save_Data>().Error_Code = gameObject.GetComponent<Pannel_Info_Storage>().Error_Code;
                    Data_Saver.GetComponent<Save_Data>().TroubleShooting_Steps =
                        Step_Text[0] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[0] + Environment.NewLine + Environment.NewLine +
                        Step_Text[1] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[1] + Environment.NewLine + Environment.NewLine +
                        Step_Text[2] + Environment.NewLine +
                        "Time On Step " + Time_between_Clicks[2] + Environment.NewLine + Environment.NewLine;
                }

                break;


          
        }
	
	}

    public void Reset_Progress_Check()
    {
        string Command_File_Name = "/Command_Cache.txt";
        string Path = Application.dataPath + Command_File_Name;

        string Script_Output_Path = Application.dataPath + "/Script_Output.txt";

        if (File.Exists(Path) == true)
        {
            string[] lines = File.ReadAllLines(Path);
            if (lines[lines.Length - 1] != "Done")
            {
                Script_Running = true;
                Error_Help_Button.GetComponent<Button>().interactable = false;
                Error_Button_Text.text = "Restarting";

            }
            else
            {
                Script_Running = false;
                Error_Help_Button.GetComponent<Button>().interactable = true;
                CancelInvoke("Reset_Progress_Check");
                has_reset = true;
                Error_Button_Text.text = "Done";
                string Script_Output_Text = System.IO.File.ReadAllText(Script_Output_Path);
                Data_Saver.GetComponent<Save_Data>().Script_Output = Script_Output_Text;
            }
        }
    }
	
}
