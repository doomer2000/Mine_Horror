using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI startGameText;
    public TextMeshProUGUI optionsText;
    public TextMeshProUGUI exitText;
    public TextMeshProUGUI leanguageText;
    public TextMeshProUGUI optionsBackText;

    [Header("TranslationRussian")]
    public string startGameTextRUS;
    public string optionsTextRUS;
    public string exitTextRUS;
    public string leanguageTextRUS;
    public string optionsBackTextRUS;

    [Header("TranslationEnglish")]
    public string startGameTextENG;
    public string optionsTextENG;
    public string exitTextENG;
    public string leanguageTextENG;
    public string optionsBackTextENG;

    [Header("TranslationSpanish")]
    public string startGameTextESP;
    public string optionsTextESP;
    public string exitTextESP;
    public string leanguageTextESP;
    public string optionsBackTextESP;


    // Start is called before the first frame update
    void Start()
    {
        var leanguageCode = PlayerPrefs.GetString("Lang");
        switch (leanguageCode)
        {
            case "ENG":
                startGameText.SetText(startGameTextENG);
                optionsText.SetText(optionsTextENG);
                exitText.SetText(exitTextENG);
                leanguageText.SetText(leanguageTextENG);
                optionsBackText.SetText(optionsBackTextENG);
                break;
            case "RUS":
                startGameText.SetText(startGameTextRUS);
                optionsText.SetText(optionsTextRUS);
                exitText.SetText(exitTextRUS);
                leanguageText.SetText(leanguageTextRUS);
                optionsBackText.SetText(optionsBackTextRUS);
                break;
            case "ESP":
                startGameText.SetText(startGameTextESP);
                optionsText.SetText(optionsTextESP);
                exitText.SetText(exitTextESP);
                leanguageText.SetText(leanguageTextESP);
                optionsBackText.SetText(optionsBackTextESP);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangePlayerLeanguage(int leanguageId)
    {
        switch(leanguageId) 
        {
            case 0:
                PlayerPrefs.SetString("Lang", "ENG");
                startGameText.SetText(startGameTextENG);
                optionsText.SetText(optionsTextENG);
                exitText.SetText(exitTextENG);
                leanguageText.SetText(leanguageTextENG);
                optionsBackText.SetText(optionsBackTextENG);
                break;
            case 1:
                PlayerPrefs.SetString("Lang", "RUS");
                startGameText.SetText(startGameTextRUS);
                optionsText.SetText(optionsTextRUS);
                exitText.SetText(exitTextRUS);
                leanguageText.SetText(leanguageTextRUS);
                optionsBackText.SetText(optionsBackTextRUS);
                break;
            case 2:
                PlayerPrefs.SetString("Lang", "ESP");
                startGameText.SetText(startGameTextESP);
                optionsText.SetText(optionsTextESP);
                exitText.SetText(exitTextESP);
                leanguageText.SetText(leanguageTextESP);
                optionsBackText.SetText(optionsBackTextESP);
                break;
        }
    }
}
