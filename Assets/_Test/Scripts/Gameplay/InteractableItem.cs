using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class InteractableItem : MonoBehaviour,IItem
{

    public OnInteractEvent OnInteractEvent;

    public bool CanInteract {get => _interactable; set => _interactable = value;}
    public string ItemName => _itemName;
    public ItemType ItemType => _itemType;

    [Header("Interactable Item Settings")]
    [SerializeField]protected bool _interactable = true;
    [SerializeField]protected bool _disappearAfterInteraction=true;
    [SerializeField]protected float _hoverVelocity = 1f;
    [SerializeField]protected string _hoverText;
    [SerializeField]protected TMP_Text textHolder;
    [SerializeField]protected AudioClip _interactSound;
    [SerializeField]protected ParticleSystem pickParticle;

    [SerializeField]protected string _itemName;
    [SerializeField]protected ItemType _itemType;

    private bool isHover;
    protected AudioSource _source;
    

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        textHolder.text =_hoverText;
        textHolder.color = new Color(textHolder.color.r,textHolder.color.g,textHolder.color.b,0f);
    }

    private void Update()
    {
        if(isHover && textHolder.color.a<1f)
            textHolder.color = new Color(textHolder.color.r,textHolder.color.g,textHolder.color.b,textHolder.color.a + Time.deltaTime*_hoverVelocity);
        else if(!isHover && textHolder.color.a > 0f)
            textHolder.color = new Color(textHolder.color.r,textHolder.color.g,textHolder.color.b,textHolder.color.a - Time.deltaTime*_hoverVelocity);
    }

    public virtual void ShowItem()
    {
        if(!CanInteract)
            return;
        isHover = true;
    }

    public virtual void HideItem()
    {
        if(!CanInteract)
            return;
        isHover = false;
    }

    public virtual void PickItem()
    {
        if(!CanInteract)
            return;
        if(pickParticle != null)
            pickParticle.Play();
        //add item to inventory
        Inventory.Instance.AddItem(this);
        if(_disappearAfterInteraction)
            gameObject.GetComponent<Renderer>().enabled=false;
        _source.PlayOneShot(_interactSound);
        HideItem();
        OnInteractEvent.Invoke();
        CanInteract=false;
    }
}

[System.Serializable]
public class OnInteractEvent : UnityEvent { };