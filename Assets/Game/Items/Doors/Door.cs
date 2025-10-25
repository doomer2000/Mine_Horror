using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public string doorNameRUS;
    public string doorNameENG;
    public string doorNameESP;

    private bool isClosed;

    public Key openKey;

    public Button useButton;

    public Animator doorAnimator;

    public UIManager UIManager;

    public AudioSource openSound;
    public AudioSource closedSound;

    public void Start()
    {
        isClosed = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isClosed)
        {
            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString(doorNameRUS, doorNameENG, doorNameESP));
            var playerInfo = GameObject.Find("Player").GetComponent<PlayerController>();
            if (playerInfo.CheckKey(openKey))
            {
                useButton.interactable = true;
                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(OpenDoor);
            }
            else
            {
                useButton.interactable = true;
                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(DoorClosed);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.ItemNameHideText();
            useButton.interactable = false;
            useButton.onClick.RemoveAllListeners();
        }
    }

    private void OpenDoor()
    {
        UIManager.ItemNameHideText();
        openSound.Play();
        doorAnimator.Play("Open");
        isClosed = false;
    }

    private void DoorClosed()
    {
        if (!openKey.HintLighth.enabled) 
        { 
            openKey.HintLighth.enabled = true; 
        }
        closedSound.Play();
        UIManager.SubtitlesShowText(LeanguageManager.GetLeanguageString("Закрыто.", "Its closed.", "Esta cerrado."));
    }
}
