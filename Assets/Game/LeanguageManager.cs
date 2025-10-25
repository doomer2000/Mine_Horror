using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeanguageManager
{
    public static string GetLeanguageString(string textRUS, string textENG, string textESP)
    {
        var selectedLeanguage = PlayerPrefs.GetString("Lang", "ENG");
        switch(selectedLeanguage)
        {
            case "RUS":
                return textRUS;
            case "ESP":
                return textESP;
            case "ENG":
                return textENG;
            default: 
                return textENG;
        }
    }
}
