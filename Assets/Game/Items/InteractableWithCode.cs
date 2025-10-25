using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableWithCode : MonoBehaviour
{
    public string code;
    public Note codeNote;

    public bool generateCode;

    public string ItemNameENG;
    public string ItemNameRUS;
    public string ItemNameESP;
    public UIManager UIManager;
    public PlayerController playerInfo;

    public string badPasswordTextENG;
    public string badPasswordTextRUS;
    public string badPasswordTextESP;

    public bool isPlayed;

    public float interactTimeInSeconds;

    public UnityEvent playableEvent;

    public AudioSource wrongNumberSound;

    public void Start()
    {
        if (generateCode)
        {
            code = Convert.ToInt32(UnityEngine.Random.Range(1111, 9999)).ToString();
            codeNote.TextENG = codeNote.TextENG.Replace("{code}", code);
            codeNote.TextRUS = codeNote.TextRUS.Replace("{code}", code);
            codeNote.TextENG = codeNote.TextENG.Replace("{code}", code);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayed)
        {
            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString(ItemNameRUS, ItemNameENG, ItemNameESP));

            UIManager.useButton.interactable = true;
            UIManager.useButton.onClick.RemoveAllListeners();
            UIManager.useButton.onClick.AddListener(() =>
            {
                if (interactTimeInSeconds != 0)
                {
                    UIManager.HidePlayerUI();
                    UIManager.ShowCodeUI();
                    UIManager.codeEnterButton.onClick.AddListener(CheckCode);
                }
            });
        }
    }

    public void CheckCode()
    {
        if (UIManager.codeUI.GetComponent<CodeUI>().code == code)
        {
            isPlayed = true;
            UIManager.ItemNameHideText();
            UIManager.useButton.interactable = false;
            UIManager.useButton.onClick.RemoveAllListeners();
            UIManager.codeEnterButton.onClick.RemoveAllListeners();
            UIManager.ShowPlayerUI();
            UIManager.HideCodeUI();
            playableEvent.Invoke();
        }
        else
        {
            wrongNumberSound.Play();
            UIManager.HideCodeUI();
            UIManager.ItemNameHideText();
            UIManager.useButton.interactable = false;
            UIManager.codeEnterButton.onClick.RemoveAllListeners();
            UIManager.useButton.onClick.RemoveAllListeners();
            UIManager.ShowPlayerUI();
            UIManager.SubtitlesShowText(LeanguageManager.GetLeanguageString(badPasswordTextRUS, badPasswordTextENG, badPasswordTextESP));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.HideCodeUI();
            UIManager.ItemNameHideText();
            UIManager.useButton.interactable = false;
            UIManager.codeEnterButton.onClick.RemoveAllListeners();
            UIManager.useButton.onClick.RemoveAllListeners();
            UIManager.ShowPlayerUI();
        }
    }
}
