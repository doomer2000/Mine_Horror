using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [TextArea(3, 10)]
    public string TextRUS;
    [TextArea(3, 10)]
    public string TextENG;
    [TextArea(3, 10)]
    public string TextESP;

    public GameObject NoteObject;
    public Text NoteText;

    public Button UseButton;
    public UIManager UIManager;

    public AudioSource NoteSound;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString("Записка", "Note", "Record"));
            UseButton.interactable = true;
            UseButton.onClick.RemoveAllListeners();
            UseButton.onClick.AddListener(() => {
                UIManager.ItemNameHideText();
                NoteSound.Play();
                NoteObject.SetActive(true);
                NoteText.text = LeanguageManager.GetLeanguageString(TextRUS, TextENG, TextESP);
                UseButton.interactable = false;
            });
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UseButton.interactable = false;
            UseButton.onClick.RemoveAllListeners();
            UIManager.ItemNameHideText();
            NoteObject.SetActive(false);
            UseButton.interactable = false;
        }
    }
}