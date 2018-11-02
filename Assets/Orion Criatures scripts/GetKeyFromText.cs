using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetKeyFromText : MonoBehaviour
{
    Text txt;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();   
        //  txt.text = Input.inp;
    }

    

    public void DetectInput()
    {
        foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vkey))
            {
                
                   txt.text = vkey.ToString(); //this saves the key being pressed               
                   // bDetectKey = false;
                    
            }
        }
    }

}
