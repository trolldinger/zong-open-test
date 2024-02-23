using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : InteractableItem
{

    public override void PickItem()
    {
        if(!CanInteract)
            return;
        //remove item from inventory
        Inventory.Instance.RemoveItem(this);
        //default behaviour
        if(pickParticle != null)
            pickParticle.Play();
        if(_disappearAfterInteraction)
            gameObject.GetComponent<Renderer>().enabled=false;
        HideItem();
        CanInteract=false;
    }

}
