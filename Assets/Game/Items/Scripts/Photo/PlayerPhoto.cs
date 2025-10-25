using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhoto : MonoBehaviour
{
    public static Action photoAction;

    public Button photoButton;
    void Start()
    {
        photoButton.onClick.AddListener(Shoot);
    }

    private void Shoot()
    {
        photoAction.Invoke();
    }
}
