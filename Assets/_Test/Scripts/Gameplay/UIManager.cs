using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField]private TMP_Text _notificationText;
    [SerializeField]private TMP_Text _weaponsText;
    [SerializeField]private TMP_Text _instrumentsText;

    [Header("UI Settings")]
    [SerializeField]private float _fadeVelocity=1f;
    [SerializeField]private float _notificationShownTime=3f;

    private bool _notificationShown;
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_notificationShown )
            return;

        if(Time.time-_time > _notificationShownTime)
        {
            if(_notificationText.color.a > 0f)
                _notificationText.color = 
                    new Color(_notificationText.color.r,
                        _notificationText.color.g,
                        _notificationText.color.b,
                        _notificationText.color.a - Time.deltaTime*_fadeVelocity);
            else
                _notificationShown = false;
        }
    }

    public void ShowNotification(string notification)
    {
        _notificationShown = true;
        _notificationText.text = notification;
        _notificationText.color = new Color(1f,1f,1f,1f);
        _time = Time.time;
    }

    public void ShowWeapons(bool isShowing)
    {
        if(isShowing)
        {
            _weaponsText.text = "--Weapons--";

            //get all weapons from inventory
            var weapons = Inventory.Instance.GetAllWeapons();
            if(weapons == null)
                return;
            foreach(InventoryItem weapon in weapons)
                _weaponsText.text += $"\n{weapon.ItemName}";
        }
        else
            _weaponsText.text = "Weapons";
    }

    public void ShowInstruments(bool isShowing)
    {
        if(isShowing)
        {
            _instrumentsText.text = "--Instruments--";

            //get all weapons from inventory
            var weapons = Inventory.Instance.GetAllInstruments();
            if(weapons == null)
                return;
            foreach(InventoryItem weapon in weapons)
                _instrumentsText.text += $"\n{weapon.ItemName}";
        }
        else
            _instrumentsText.text = "Instruments";
    }
}
