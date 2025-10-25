using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivatableObject : MonoBehaviour
{
    public UnityEvent activationEvent;
    public bool isRepeatable;
    
    private bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isRepeatable || !isActivated)
            {
                activationEvent.Invoke();
                isActivated = true;
            }
        }
    }
}
