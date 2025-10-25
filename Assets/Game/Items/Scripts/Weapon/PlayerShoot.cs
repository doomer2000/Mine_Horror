using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootAction;

    public Button shootButton;
    // Start is called before the first frame update
    void Start()
    {
        shootButton.onClick.AddListener(Shoot);
    }

    private void Shoot()
    {
        shootAction.Invoke();
    }
}
