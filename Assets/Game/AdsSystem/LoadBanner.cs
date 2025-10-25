using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadBanner : MonoBehaviour
{
    public string adUnitId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetBannerPosition()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
    }

    public void LoadBannerMain()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadedError
        };
        Advertisement.Banner.Load(adUnitId ,options);
    }

    public void OnBannerLoaded()
    {
        showBannerAdd();
        Debug.Log("OnBannerLoaded");
    }

    private void showBannerAdd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = OnBannerShow,
            clickCallback = OnBannerClick,
            hideCallback = OnBannerHidden
        };
        Advertisement.Banner.Show(adUnitId, options);
    }

    private void OnBannerClick()
    {
    }

    private void OnBannerShow()
    {
    }

    public void HideBannerAdd()
    {
        Advertisement.Banner.Hide();
    }

    public void OnBannerLoadedError(string error)
    {
        Debug.Log("OnBannerLoadedError");
    }

    public void OnBannerHidden()
    {
        Debug.Log("OnBannerHidden");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
