using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotHatItem : UI_SlotItem
{
    public HatType hatType;
    public void SetUpItem(Sprite icon, string nameItem, int price, int status, HatType hatType, int index, Action<int, int> updateStatus)
    {
        this.icon.sprite = icon;
        txtNameItem.text = nameItem;
        txtPrice.text = price.ToString("n");
        this.status = status;
        this.price = price;
        this.hatType = hatType;
        this.index = index;
        this.updateStatus = updateStatus;
        // add event 
        UpdateStatus(status);
    }

    public override void Equitment()
    {
        base.Equitment();
        status = 2;
        UpdateStatus(status);
        updateStatus?.Invoke(status, index);
        Player.instance.ChangeHat(hatType);
        DataManager.instance.SaveIDataSave();
    }
}
