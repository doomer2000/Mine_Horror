using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFallScreamer : MonoBehaviour
{
    public Rigidbody fallObjectRb;

    public AudioSource screamerSound;

    public Light light;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && fallObjectRb.useGravity == false)
        {
            fallObjectRb.useGravity = true;
            screamerSound.Play();
            light.enabled = true;
        }
    }
}
