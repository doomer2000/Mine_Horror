using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("Info")]
    [SerializeField]
    public string KeyNameENG;
    [SerializeField]
    public string KeyNameRUS;
    [SerializeField]
    public string KeyNameESP;
    [SerializeField]
    public string keyCode;

    [SerializeField]
    public Light HintLighth;

    [SerializeField]
    public Button UseButton;

    [SerializeField]
    public AudioSource KeyGetSound;

    [SerializeField]
    public UIManager UIManager;

    [SerializeField]
    public PlayerController playerInfo;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString(KeyNameRUS, KeyNameENG, KeyNameESP));

            UseButton.interactable = true;
            UseButton.onClick.RemoveAllListeners();
            UseButton.onClick.AddListener(() => {
                UIManager.ItemNameHideText();
                KeyGetSound.Play();
                playerInfo.AddKey(this);
                this.gameObject.SetActive(false);
                UseButton.interactable = false;
            });
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.ItemNameHideText();
            UseButton.interactable = false;
            UseButton.onClick.RemoveAllListeners();
        }
    }
}