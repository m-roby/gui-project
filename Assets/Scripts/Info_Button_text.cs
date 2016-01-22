using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Info_Button_text : MonoBehaviour {

    public Text Text;
    public string Name;

	// Use this for initialization
	void Start () {
        Text = GetComponent<Text>();
        Text.text = Name;
	
	}
	
}
