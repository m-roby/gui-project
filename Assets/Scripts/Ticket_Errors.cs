using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ticket_Errors : MonoBehaviour {

    public GameObject Ticket_Error_Pannel;
    public GameObject Ticket_Panel;
    public string Error_Text;




    public void Start()
    {
        Ticket_Error_Pannel = GameObject.Find("Ticket_Errors");
        Ticket_Panel = GameObject.Find("Ticket");
        Error_Text = Ticket_Panel.GetComponent<Customer_Input>().Ticket_Error;

        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = Error_Text;
        }

    }
	

	public void Close_Panel () {

        Destroy(Ticket_Error_Pannel, 0f);
	
	}
}
