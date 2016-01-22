using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KMX_Image_Changer : MonoBehaviour {

    public Sprite Image;
    public string Name;
    public string Address;
    public int Address_Int;
    public string DevString;
    public string IP_Device;


    // Use this for initialization
    void Start () {

        Name = gameObject.GetComponent<Kitchen_Device_Info>().Name;
        Address = gameObject.GetComponent<Kitchen_Device_Info>().Address;
        IP_Device = gameObject.GetComponent<Kitchen_Device_Info>().Packet_Loss;

        DevString = "/dev/ttyp";


        if (IP_Device == "Non-IP Device")
        {
            Image = Resources.Load<Sprite>("Sprites/Printer");
            GetComponent<Image>().sprite = Image;
        }
        else
        {
            Address = Address.Replace(DevString, "");
            Address_Int = int.Parse(Address);

            if (Address_Int >= 90 && Address_Int < 100)
            {
                Image = Resources.Load<Sprite>("Sprites/Printer");
                GetComponent<Image>().sprite = Image;
            }

            if (Address_Int >= 100)
            {
                Image = Resources.Load<Sprite>("Sprites/KMX");
                GetComponent<Image>().sprite = Image;
            }


        }

        if (Name.Contains("Printer") || Name.Contains("Prt") || Name.Contains("Ptr"))
        {
            Image = Resources.Load<Sprite>("Sprites/Printer");
            GetComponent<Image>().sprite = Image;
        }





    }
	
}
