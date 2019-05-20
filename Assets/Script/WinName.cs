using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinName : MonoBehaviour {

    public Text t;
    public static string winName = "";
    private void Start()
    {
        t.text = winName + " WON";

    }
}
