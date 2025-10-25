using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableItem : MonoBehaviour
{
    public string ItemNameENG;
    public string ItemNameRUS;
    public string ItemNameESP;
    public UIManager UIManager;
    public PlayerController playerInfo;

    public string dontHaveItemsTextENG;
    public string dontHaveItemsTextRUS;
    public string dontHaveItemsTextESP;

    public bool isPlayed;
    public bool canReplay;

    public float interactTimeInSeconds;

    public UnityEvent playableEvent;

    public List<string> itemsNeededToInteract;

    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (canReplay || !isPlayed))
        {
            UIManager.ItemNameShowText(LeanguageManager.GetLeanguageString(ItemNameRUS, ItemNameENG, ItemNameESP));

            
            UIManager.useButton.interactable = true;
            UIManager.useButton.onClick.RemoveAllListeners();
            var playerHasItems = true;
            if (itemsNeededToInteract.Count > 0 )
            {
                foreach (var item in itemsNeededToInteract)
                {
                    if(!playerInfo.playerItems.Contains(item))
                    {
                        playerHasItems = false;
                        break;
                    }
                }
            }
            if (playerHasItems)
            {
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
            else
            {
                UIManager.useButton.onClick.AddListener(() =>
                {
                    UIManager.SubtitlesShowText(LeanguageManager.GetLeanguageString(dontHaveItemsTextRUS, dontHaveItemsTextENG, dontHaveItemsTextESP));
                    UIManager.useButton.interactable = false;
                    UIManager.useButton.onClick.RemoveAllListeners();
                });
            }
        }
    }

    public void Interact()
    {
        isPlayed = true;
        playableEvent.Invoke();
        UIManager.ItemNameHideText();
        UIManager.useButton.interactable = false;
        UIManager.useButton.onClick.RemoveAllListeners();
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
