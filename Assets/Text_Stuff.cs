using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_Stuff : MonoBehaviour {

    public Text text;
    public string text2;
    public float thing;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
        InvokeRepeating("Count", 0f, .1f);
	
	}

    void Count()
    {
        ++thing;
        text.text = (" " + thing);

        if (thing == 20)
        {
            text2 = ("Victor is the man!");
            text.text = text2;
            CancelInvoke("Count");
        }
    }
}
