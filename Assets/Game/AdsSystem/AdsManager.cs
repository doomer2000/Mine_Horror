using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager1 : MonoBehaviour
{

    public bool isLoadBanner;

    public LoadBanner loadBanner;

    // Start is called before the first frame update
    void Start()
    {
        if(isLoadBanner)
        {
            loadBanner.SetBannerPosition();
            loadBanner.LoadBannerMain();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
