using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActionDependentOnObjects : MonoBehaviour
{
    public bool isActivated;

    public List<InteractableItem> itemsToActivate;

    public UnityEvent actionAfterItemsActivated;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!itemsToActivate.Any(x => !x.isPlayed))
        {
            actionAfterItemsActivated.Invoke();
            isActivated = true;
        }
    }
}
