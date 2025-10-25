using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;

    [SerializeField]
    public GunData mainGun;

    [Header("CodeUI")]
    [SerializeField]
    public GameObject codeUI;
    [SerializeField]
    public Button codeEnterButton;

    [Header("Sliders")]
    [SerializeField]
    public Slider interactTimeSlider;
    [SerializeField]
    public Slider anxietySlider;

    [Header("Buttons")]
    public Button photoButton;
    [SerializeField]
    public Button useButton;
    [SerializeField]
    public Button stopHidingButton;

    [Header("GameOverScreen")]
    public GameObject gameOverScreen;
    [SerializeField]
    public GameObject playerUI;

    [Header("GameOverScreen")]
    public GameObject winScreen;

    [Header("Ads")]
    public InterstatialAds interstatialAds;

    [Header("Text")]
    [SerializeField]
    public Text subtitlesText;
    [SerializeField]
    public Text itemNameText;
    [SerializeField]
    public Text gameOverText;
    [SerializeField]
    public Text reviveText;
    [SerializeField]
    public Text restartText;
    [SerializeField]
    public Text exitText;
    [SerializeField]
    public Text ammoText;

    [SerializeField]
    public Text exitTextWinScreen;
    [SerializeField]
    public Text exitTextSettings;

    [SerializeField]
    public Text continueTextSettings;

    [SerializeField]
    public Text restartTextSettings;


    public string gameOverTextRUS;
    public string reviveTextRUS;
    public string restartTextRUS;
    public string exitTextRUS;
    public string continueTextRUS;

    public string gameOverTextENG;
    public string reviveTextENG;
    public string restartTextENG;
    public string exitTextENG;
    public string continueTextENG;

    public string gameOverTextESP;
    public string restartTextESP;
    public string exitTextESP;
    public string continueTextESP;

    void Start()
    {
        PlayerShoot.shootAction += OnShoot;
        gameOverText.text = LeanguageManager.GetLeanguageString(gameOverTextRUS, gameOverTextENG, gameOverTextESP);
        reviveText.text = LeanguageManager.GetLeanguageString(reviveTextRUS, reviveTextENG, reviveTextENG);
        restartText.text = LeanguageManager.GetLeanguageString(restartTextRUS, restartTextENG, restartTextESP);
        exitText.text = LeanguageManager.GetLeanguageString(exitTextRUS, exitTextENG, exitTextESP);
        exitTextSettings.text = LeanguageManager.GetLeanguageString(exitTextRUS, exitTextENG, exitTextESP);
        exitTextWinScreen.text = LeanguageManager.GetLeanguageString(exitTextRUS, exitTextENG, exitTextESP);
        restartTextSettings.text = LeanguageManager.GetLeanguageString(restartTextRUS, restartTextENG, restartTextESP);
        continueTextSettings.text = LeanguageManager.GetLeanguageString(continueTextRUS, continueTextENG, continueTextESP);
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = $"{mainGun.CurrentAmmo}/{mainGun.MagSize}";
        anxietySlider.value = playerController.AnxietyLevel;
    }

    public void HidePlayerUI()
    {
        playerUI.SetActive(false);
    }

    public void ShowPlayerUI()
    {
        playerUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        HidePlayerUI();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        HidePlayerUI();
        ShowAd();
    }


    public void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void ShowAd()
    {
        interstatialAds.LoadAd();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SetPhotoButtonInteractable(bool interactable)
    {
        photoButton.interactable = interactable;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SubtitlesShowTooDarkText()
    {
        var tooDarkTextRUS = "«ƒ≈—‹ —À»ÿ ŒÃ “≈ÃÕŒ, Õ”∆ÕŒ œŒ◊»Õ»“‹ —¬≈“.";
        var tooDarkTextENG = "IT'S TOO DARK IN HERE, WE NEED TO FIX THE LIGHTS.";
        var tooDarkTextESP = "ESTA DEMASIADO OSCURO AQUI, TENEMOS QUE ARREGLAR LAS LUCES.";
        SubtitlesShowText(LeanguageManager.GetLeanguageString(tooDarkTextRUS, tooDarkTextENG, tooDarkTextESP));
    }

    public void SubtitlesShowText(string text)
    {
        subtitlesText.text = text;
        subtitlesText.enabled = true;
        Invoke("SubtitlesHideText", text.Length*0.2f);
    }

    public void SubtitlesHideText()
    {
        subtitlesText.text = string.Empty;
        subtitlesText.enabled = false; 
    }

    public void ShowCodeUI()
    {
        codeUI.SetActive(true);
    }
    public void HideCodeUI()
    {
        codeUI.SetActive(false);
    }

    public void ItemNameShowText(string text)
    {
        itemNameText.text = text;
        itemNameText.enabled = true;
    }

    public void ItemNameHideText()
    {
        itemNameText.text = string.Empty;
        itemNameText.enabled = false;
    }

    public void PlayInteractionSlider(float interactionTime)
    {
        interactTimeSlider.gameObject.SetActive(true);
        interactTimeSlider.maxValue = interactionTime;
        interactTimeSlider.value = 0;
        InvokeRepeating("InteractionSliderIncrese", 0, 0.25f);
        Invoke("SliderStop", interactionTime);
    }

    public void SliderStop()
    {
        interactTimeSlider.gameObject.SetActive(false);
        CancelInvoke("InteractionSliderIncrese");
    }

    public void InteractionSliderIncrese()
    {
        interactTimeSlider.value += 0.25f;
    }

    private void OnShoot()
    {

    }
}
