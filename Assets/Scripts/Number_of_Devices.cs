using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Number_of_Devices : MonoBehaviour
{
    private string jsonstring;
    private JsonData Terminal_Data;

    public int Number_of_POS;
    public int Number_of_Kitchen;

    public GameObject POS_Terminal;
    public GameObject Kitchen_Device;

    public Vector2 Terminal_Position;
    public float X_Pos;
    public Vector2 Kitchen_Position;

    public GameObject Canvas;
    public GameObject Info_Button;

    // Use this for initialization
    void Start()
    {
        jsonstring = File.ReadAllText(Application.dataPath + "/Resources/SystemInfo.json");
        Terminal_Data = JsonMapper.ToObject(jsonstring);
        Number_of_POS = Terminal_Data["POS"].Count;
        Number_of_Kitchen = Terminal_Data["Kitchen"].Count;
        X_Pos = -500;
        Canvas = GameObject.Find("Canvas");



        for (int i = 0; i < Number_of_POS; i++)
        {
            POS_Terminal = Instantiate(Resources.Load("computer") as GameObject);
            Info_Button = Instantiate(Resources.Load("Info_Button") as GameObject);
            Info_Button.transform.SetParent(POS_Terminal.transform);
            Info_Button.GetComponent<Parent>().Parent_Object = POS_Terminal;
            POS_Terminal.transform.SetParent(Canvas.transform);

            POS_Terminal.GetComponent<RectTransform>().localPosition = new Vector3(X_Pos, 100, 0);
            //Info_Button.GetComponent<RectTransform>().localPosition = new Vector3(POS_Terminal.transform.position.x, POS_Terminal.transform.position.y - 90, POS_Terminal.transform.position.z);
            X_Pos =X_Pos + 200f;


            POS_Terminal.name = Terminal_Data["POS"][i]["Name"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Name = Terminal_Data["POS"][i]["Name"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().IP_Address = Terminal_Data["POS"][i]["IP Address"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Packet_Loss = Terminal_Data["POS"][i]["Packet Loss"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Uptime = Terminal_Data["POS"][i]["Uptime"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Run_Level = Terminal_Data["POS"][i]["Run Level"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Motherboard = Terminal_Data["POS"][i]["Motherboard"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Motherboard_Manufacturer = Terminal_Data["POS"][i]["Motherboard Manufacturer"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Printer_Configured = Terminal_Data["POS"][i]["Printer Configured"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Printer_Device_Node = Terminal_Data["POS"][i]["Printer Device Node"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Printer_Attached = Terminal_Data["POS"][i]["Printer Attached"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Printer_SimLink = Terminal_Data["POS"][i]["Printer SimLink"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Cash_Drawer_Configured = Terminal_Data["POS"][i]["Cash Drawer Configured"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Touch_Screen = Terminal_Data["POS"][i]["Touch Screen"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Drive_Health = Terminal_Data["POS"][i]["Drive Health"].ToString();
            POS_Terminal.GetComponent<POS_Device_Info>().Printer_Errors = Terminal_Data["POS"][i]["Printer Errors"].ToString();

            Info_Button.GetComponentInChildren<Info_Button_text>().Name = POS_Terminal.name;
        }


        X_Pos = -500;

        for (int i = 0; i < Number_of_Kitchen; i++)
        {
            Kitchen_Device = Instantiate(Resources.Load("kitchen") as GameObject);
            Kitchen_Device.transform.SetParent(Canvas.transform);

            Info_Button = Instantiate(Resources.Load("Info_Button") as GameObject);
            Info_Button.transform.SetParent(Kitchen_Device.transform);
            Info_Button.GetComponent<Parent>().Parent_Object = POS_Terminal;

            Kitchen_Device.GetComponent<RectTransform>().localPosition = new Vector3(X_Pos, -100f, 0);
            X_Pos = X_Pos + 200;

            //Info_Button.GetComponent<RectTransform>().localPosition = new Vector3(Kitchen_Device.transform.position.x, Kitchen_Device.transform.position.y - 90, Kitchen_Device.transform.position.z);

            Kitchen_Device.name = Terminal_Data["Kitchen"][i]["Name"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Name = Terminal_Data["Kitchen"][i]["Name"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Address = Terminal_Data["Kitchen"][i]["Address"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Virtual_Device_1 = Terminal_Data["Kitchen"][i]["Virtual Device 1"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Virtual_Device_2 = Terminal_Data["Kitchen"][i]["Virtual Device 2"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Status = Terminal_Data["Kitchen"][i]["Status"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Packet_Loss = Terminal_Data["Kitchen"][i]["Packet Loss"].ToString();
            Kitchen_Device.GetComponent<Kitchen_Device_Info>().Bump_Bar = Terminal_Data["Kitchen"][i]["Bump Bar"].ToString();

            Info_Button.GetComponentInChildren<Info_Button_text>().Name = Kitchen_Device.name;


        }


    }
}
