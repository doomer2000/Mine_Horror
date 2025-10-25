using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HideSpot : MonoBehaviour
{
    private readonly string ItemNameENG = "LOCKER";
    private readonly string ItemNameRUS = "ØÊÀÔ×ÈÊ";
    private readonly string ItemNameESP = "CASILLERO";
    public UIManager UIManager;
    public PlayerController playerInfo;

    public Camera hideSpotCamera;

    public float interactTimeInSeconds;

    public UnityEvent playableEvent;

    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString(ItemNameRUS, ItemNameENG, ItemNameESP));


            UIManager.useButton.interactable = true;
            UIManager.useButton.onClick.RemoveAllListeners();

            UIManager.useButton.onClick.AddListener(() =>
            {
                if (interactTimeInSeconds != 0)
                {
                    UIManager.HidePlayerUI();
                    UIManager.PlayInteractionSlider(interactTimeInSeconds);
                }
                Invoke("Interact", interactTimeInSeconds + 0.1f);
            });
        }
    }

    public void Interact()
    {
        UIManager.ItemNameHideText();
        UIManager.useButton.interactable = false;
        UIManager.useButton.onClick.RemoveAllListeners();
        UIManager.stopHidingButton.interactable = true;
        UIManager.stopHidingButton.onClick.RemoveAllListeners();
        UIManager.stopHidingButton.onClick.AddListener(StopHiding);
        UIManager.stopHidingButton.gameObject.SetActive(true);
        playerInfo.gameObject.SetActive(false);
        hideSpotCamera.gameObject.SetActive(true);
        UIManager.HidePlayerUI();
    }

    public void StopHiding()
    {
        UIManager.stopHidingButton.gameObject.SetActive(false);
        playerInfo.gameObject.SetActive(true);
        hideSpotCamera.gameObject.SetActive(false);
        UIManager.ShowPlayerUI();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.ItemNameHideText();
            UIManager.useButton.interactable = false;
            UIManager.useButton.onClick.RemoveAllListeners();
        }
    }
}
