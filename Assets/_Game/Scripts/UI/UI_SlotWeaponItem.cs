using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotWeaponItem : UI_SlotItem
{
    public WeaponType weaponType;
    public void SetUpItem(Sprite icon, string nameItem, int price, int status,WeaponType weaponType,int index,Action<int,int> updateStatus)
    {
        this.icon.sprite = icon;
        txtNameItem.text = nameItem;
        txtPrice.text = price.ToString("n");
        this.status = status;
        this.price = price;
        this.weaponType = weaponType;
        this.index = index;
        this.updateStatus = updateStatus;
        // add event 
        Player.instance.ChangeWeapon(weaponType);
        UpdateStatus(status);
    }

    public override void Equitment()
    {
        base.Equitment();
        status = 2;
        Player.instance.ChangeWeapon(weaponType);
        updateStatus?.Invoke(status, index);
        UpdateStatus(status);
        DataManager.instance.SaveIDataSave();
    }
}
