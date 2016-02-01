using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class POS_Image_Changer : MonoBehaviour {

    public Sprite Image;
    public Sprite Back_Image;
    public string Manufacturer;
    public string Model;


	// Use this for initialization
	void Start () {

        Manufacturer = gameObject.GetComponent<POS_Device_Info>().Motherboard_Manufacturer;
        Model = gameObject.GetComponent<POS_Device_Info>().Motherboard;

        if (Manufacturer.Contains("WINCOR"))
        {
            Debug.Log("Wincor");

            if (Model.Contains("D2-CPU"))
            {
                Debug.Log("Beetle");
                Image = Resources.Load<Sprite>("Sprites/Beetle");
                Back_Image = Resources.Load<Sprite>("TSImages/Beetle_PC_Back");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Beetle";
            }

            if (Model.Contains("PH03"))
            {
                Debug.Log("Fusion");
                Image = Resources.Load<Sprite>("Sprites/Fusion");
                Back_Image = Resources.Load<Sprite>("TSImages/Fusion_Rear_Cables");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Fusion";
            }

            if (Model.Contains("PD359"))
            {
                Debug.Log("Express");
                Image = Resources.Load<Sprite>("Sprites/Express");
                Back_Image = Resources.Load<Sprite>("TSImages/Express_Cables");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Fusion";
            }
        }



        if (Manufacturer == "GIGABYTE" && Model.Contains("M7V90PI-V"))
        {
            Image = Resources.Load<Sprite>("Sprites/VXL");
            Back_Image = Resources.Load<Sprite>("TSImages/VXL_Back");
            GetComponent<Image>().sprite = Image;
            gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "VXL";
        }



        if (Manufacturer.Contains("PARTECH"))
        {
            if (Model.Contains("C56"))
            {
                Image = Resources.Load<Sprite>("Sprites/Everserv500");
                Back_Image = Resources.Load<Sprite>("TSImages/Everserv_500_ports");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Touch_Screen = "PAR";
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "EverServ500";
            }

            if (Model.Contains("C36") || Model.Contains("945GSE"))
            {
                Image = Resources.Load<Sprite>("Sprites/Everserv2000");
                Back_Image = Resources.Load<Sprite>("TSImages/EverServ_2000_Ports");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Touch_Screen = "PAR";
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "EverServ2000";
            }

        }


        if (Manufacturer.Contains("DELL"))
        {
            if (Model.Contains("01KD4V"))
            {
                Image = Resources.Load<Sprite>("TSImages/Optiplex_Xe");
                Back_Image = Resources.Load<Sprite>("TSImages/Optiplex_XE_Back_POS");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Optiplex_XE";
            }

            //this is actually the optiplex 740 enhanced
            if (Model.Contains("0YP693"))
            {
                Image = Resources.Load<Sprite>("TSImages/Optiplex_Xe");
                Back_Image = Resources.Load<Sprite>("TSImages/Optiplex_XE_Back_POS");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Optiplex_XE";
            }

            // this is actually the optiplex 3010
            if (Model.Contains("0T10XW"))
            {
                Image = Resources.Load<Sprite>("TSImages/Optiplex_Xe");
                Back_Image = Resources.Load<Sprite>("TSImages/Optiplex_XE_Back_POS");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Optiplex_XE";
            }

        }

        if (Manufacturer.Contains("INTEL"))
        {
            // this is the cybertron pentium
            if (Model.Contains("DH61WW"))
            {
                Image = Resources.Load<Sprite>("TSImages/Optiplex_XE");
                Back_Image = Resources.Load<Sprite>("TSImages/Optiplex_XE_Back_POS");
                GetComponent<Image>().sprite = Image;
                gameObject.GetComponent<POS_Device_Info>().Terminal_Type = "Optiplex_XE";
            }

        }





    }
	

}
