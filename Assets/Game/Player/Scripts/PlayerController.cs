using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public bool CanShoot;
    public int AmmoCount;

    public BlackMonster killedBy;

    public List<string> playerItems;

    [SerializeField]
    public UIManager UIManager;

    [SerializeField]
    public Animator HandsAnimator;

    [SerializeField]
    public Animator HearthUIAnimation;

    [SerializeField]
    public float AnxietyStartAfter;
    public float AnxietyLevel;
    public float MaxAnxietyLevel;

    public int PhotoReloadInSeconds;

    [HideInInspector]
    public bool IsPlayerHasShovel;

    [HideInInspector]
    public bool IsPhotoReloading;

    [SerializeField]
    public GameObject catScreamerCanvas;
    [SerializeField]
    public VideoPlayer catScreamerClip;

    [Header("Sounds")]
    [SerializeField]
    public AudioSource photoSound;
    [SerializeField]
    public AudioSource HearthbeatSound;
    [SerializeField]
    public AudioSource BackgroundMusic;
    [SerializeField]
    public AudioSource hearthPop;

    private bool isPlayerDead;

    public Transform playerRevivePosition;

    private List<Key> playerKeys;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = new List<string>();
        playerKeys = new List<Key>();
        IsPhotoReloading = false;
        isPlayerDead = false;
        CanShoot = true;
        PlayerPhoto.photoAction += Photo;
        InvokeRepeating("AnxietyUp", AnxietyStartAfter, 0.6f);
    }

    public void AddPlayerItem(string itemName)
    {
        if (!playerItems.Contains(itemName))
        {
            playerItems.Add(itemName);
        }
    }

    public void KillByBlackMonsterAnimationPlay()
    {
        UIManager.HidePlayerUI();
        HandsAnimator.Play("KilledByBlackMonster");
    }

    public void Revive()
    {
        isPlayerDead = false;
        UIManager.ShowPlayerUI();
        UIManager.HideGameOverScreen();
        UIManager.ResumeGame();
        transform.position = playerRevivePosition.position;
        killedBy.SetSleep(0);
    }
    private void Photo()
    {
        if (!IsPhotoReloading)
        {
            HearthbeatSound.volume = 0.15f;
            BackgroundMusic.volume = 0.0f;
            IsPhotoReloading = true;
            CanShoot = false;
            photoSound.Play();
            HandsAnimator.Play("Photo");
            Invoke("PhotoStop", 11);
            Invoke("PhotoReloadComplete", PhotoReloadInSeconds);
            UIManager.SetPhotoButtonInteractable(false);
        }
        else
        {
            Debug.Log("Photo reloading.");
        }
    }
    private void PhotoStop()
    {
        HearthbeatSound.volume = 1f;
        BackgroundMusic.volume = 0.60f;
        CanShoot = true;
        AnxietyLevel = 0;
    }

    private void PhotoReloadComplete()
    {
        IsPhotoReloading = false;
        UIManager.SetPhotoButtonInteractable(true);
    }

    public bool CheckKey(Key key)
    {
        return playerKeys.Any(x => x.keyCode == key.keyCode);
    }

    public void AddKey(Key key)
    {
        if(!playerKeys.Any(x => x.keyCode == key.keyCode))
        {
            playerKeys.Add(key);
        }
    }

    private void AnxietyUp()
    {
        if (!isPlayerDead)
        {
            AnxietyLevel += 0.4f;
            if (AnxietyLevel >= MaxAnxietyLevel)
            {
                hearthPop.Play();
                HandsAnimator.Play("Hands_HearthPop");
                Invoke("PlayCatScreamer", 4);
                Invoke("PlayerDied", 5);
                isPlayerDead = true;
            }
            var fogDensityResult = AnxietyLevel * 10 / 3000;
            HearthbeatSound.pitch = fogDensityResult * 2.3f;
            RenderSettings.fogDensity = fogDensityResult;
            HearthUIAnimation.speed = fogDensityResult;
            if (AnxietyLevel < 30)
            {
                HearthUIAnimation.speed = 0.8f;
                RenderSettings.fogDensity = 0.1f;
            }
        }
    }

    public void KillPlayer(float killTime)
    {
        Invoke("PlayCatScreamer", killTime);
        Invoke("PlayerDied", killTime + 0.3f);
    }

    public void PlayerDied()
    {
        UIManager.ShowGameOverScreen();
        UIManager.PauseGame();
    }

    public void PlayCatScreamer()
    {
        catScreamerCanvas.SetActive(true);
        catScreamerClip.Play();
        UIManager.HidePlayerUI();
    }

    void Update()
    {
        
    }
}
