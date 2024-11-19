using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotPantItem : UI_SlotItem
{
    public PantType pantType;
    public void SetUpItem(Sprite icon, string nameItem, int price, int status, PantType pantType, int index, Action<int, int> updateStatus)
    {
        this.icon.sprite = icon;
        txtNameItem.text = nameItem;
        txtPrice.text = price.ToString("n");
        this.status = status;
        this.price = price;
        this.pantType = pantType;
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
        Player.instance.ChangePant(pantType);
        DataManager.instance.SaveIDataSave();
    }
}
