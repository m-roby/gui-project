using UnityEngine;
using System.Collections;

public class Pannel_Info_Storage : MonoBehaviour {

    public GameObject Terminal;
    public int Error_Code;
    public string Terminal_Summary;

    public GameObject Step_Image;
    public GameObject Text;
    public GameObject Close_Button;
    public GameObject Help_Button;
    public GameObject Error_Help_Button;

	// Use this for initialization
	public void Start() {

        if (Terminal.GetComponent<Error_Codes>() != null)
        {
            Error_Code = Terminal.GetComponent<Error_Codes>().Error_Code_Number;
            gameObject.GetComponent<Solutions>().Error_Code_Number = Error_Code;
            if (Terminal.GetComponent<POS_Error_Check>() != null)
            {
                Terminal_Summary = Terminal.GetComponent<POS_Error_Check>().Terminal_Summary;
            }
            else
            {
                Terminal_Summary = Terminal.GetComponent<KMX_Error_Check>().Terminal_Summary;
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {

    }
}
