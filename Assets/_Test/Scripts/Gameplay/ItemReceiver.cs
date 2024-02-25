using UnityEngine;

public class ItemReceiver : InteractableItem
{

    public override void PickItem()
    {
        if(!CanInteract)
            return;
        if(!Inventory.Instance.HasItem(this))
            return;
        //remove item from inventory
        Inventory.Instance.RemoveItem(this);
        //default behaviour
        if(pickParticle != null)
            pickParticle.Play();
        if(_disappearAfterInteraction)
            gameObject.GetComponent<Renderer>().enabled=false;
        HideItem();
        OnInteractEvent.Invoke();
        CanInteract=false;
    }

}
