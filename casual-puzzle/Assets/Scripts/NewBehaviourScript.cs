using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using  UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    void Start()
    {
        string result = "";
        result = result + " " + PlayerPrefs.GetInt("rowNumber");
        result = result + " " + PlayerPrefs.GetInt("columnNumber");
        result = result + " " + PlayerPrefs.GetInt("colorNumber");
        result = result + " " + PlayerPrefs.GetInt("firstLevelLimit");
        result = result + " " + PlayerPrefs.GetInt("secondLevelLimit");
        result = result + " " + PlayerPrefs.GetInt("thirdLevelLimit");

        text.text = result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
