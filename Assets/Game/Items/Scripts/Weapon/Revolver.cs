using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    [SerializeField]
    public PlayerController controller;

    [SerializeField]
    public Camera playerCamera;

    [SerializeField]
    public GunData GunData;

    private Animator animator;

    [SerializeField]
    private Animator handsAnimator;

    [SerializeField]
    private AudioSource shotSound;

    [SerializeField]
    private AudioSource reloadSound;

    [SerializeField]
    private AudioSource emptySound;

    float timeSinceLastShot;

    public Transform AimLine;

    public void Start()
    {
        GunData.IsReloading = false;
        GunData.CurrentAmmo = 4;
        PlayerShoot.shootAction += Shoot;
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    void FixedUpdate()
    {
    }

    private bool CanShoot() => controller.CanShoot && !GunData.IsReloading && timeSinceLastShot > 1f / (GunData.FireRate / 60f);

    public void Shoot()
    {
        if (GunData.CurrentAmmo > 0 && CanShoot())
        {
            Vector3 cameraOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(cameraOrigin, playerCamera.transform.forward, out RaycastHit hitInfo, GunData.MaxDistance))
            {
                var hitObject = hitInfo.transform.gameObject;
                Debug.Log(hitInfo.transform.gameObject.name);
                if (hitObject.CompareTag("BlackMonster"))
                {
                    var blackMonster = hitObject.GetComponent<BlackMonster>();
                    blackMonster.TakeDamage(Convert.ToInt32(GunData.Damage));
                }
            }
            GunData.CurrentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }
        else if (GunData.CurrentAmmo == 0 && !GunData.IsReloading && CanShoot())
        {
            if (controller.AmmoCount > 0)
            {
                OnReload();
                if (controller.AmmoCount > GunData.MagSize)
                {
                    GunData.CurrentAmmo += GunData.MagSize;
                    controller.AmmoCount -= GunData.MagSize;
                }
                else
                {
                    GunData.CurrentAmmo += controller.AmmoCount;
                    controller.AmmoCount = 0;
                }
            }
            else
            {
                emptySound.Play();
            }
        }

    }

    private void StopReloading()
    {
        GunData.IsReloading = false;
    }

    private void OnReload()
    {
        reloadSound.Play();
        GunData.IsReloading = true;
        handsAnimator.Play("Reload_Revolver");
        Invoke("StopReloading", 2.5f);
    }

    private void OnGunShot()
    {
        handsAnimator.Play("Fire_Revolver");
        animator.Play("Fire");
        shotSound.Play();
    }
}