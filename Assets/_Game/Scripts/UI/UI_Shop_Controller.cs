using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_Controller : MonoBehaviour,IDataSave
{
    [SerializeField] Transform weaponShopTransform,hatShopTranform,pantShopTranform;
    [SerializeField] GameObject weaponShopObj,hatShopObj,pantShopObj;
    [SerializeField] Transform shopParrent;
    [SerializeField] Button[] btnTabMenus;
    [SerializeField] Transform[] tabShops;
    List<int> weaponsStatus = new List<int>();
    List<int> hatStatus = new List<int>();
    List<int> pantStatus = new List<int>();
    List<UI_SlotWeaponItem> listUIWeapon = new List<UI_SlotWeaponItem>();
    List<UI_SlotHatItem> listUIHat = new List<UI_SlotHatItem>();
    List<UI_SlotPantItem> listUIPant = new List<UI_SlotPantItem>();
    HatData hatData;
    PantData pantData;
    WeaponData weaponData;
    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        for (int i = 0; i < btnTabMenus.Length; i++)
        {
            int index = i;
            btnTabMenus[i].onClick.AddListener(() => SwitchTo(tabShops[index].gameObject));
        }
        StartCoroutine(CheckNull());
    }

    IEnumerator CheckNull()
    {
        yield return new WaitUntil(()=>DataManager.instance);
        
        
        for (int i = 0; i < weaponData.data.Count; i++)
        {
            UI_SlotWeaponItem data = Instantiate(weaponShopObj, weaponShopTransform).GetComponent<UI_SlotWeaponItem>();
            data.SetUpItem(weaponData.data[i].icon, weaponData.data[i].nameItem, weaponData.data[i].price, weaponsStatus[i], weaponData.data[i].weaponType,i,(a,b)=>UpdateStatusWeapon(a,b));
            listUIWeapon.Add(data);
        }
        for (int i = 0; i < hatData.data.Count; i++)
        {
            UI_SlotHatItem data = Instantiate(hatShopObj, hatShopTranform).GetComponent<UI_SlotHatItem>();
            data.SetUpItem(hatData.data[i].icon, hatData.data[i].nameItem, hatData.data[i].price, hatStatus[i], hatData.data[i].hatType, i, (a, b) =>UpdateStatusHat(a,b));
            listUIHat.Add(data);
        }
        for (int i = 0; i < pantData.data.Count; i++)
        {
            UI_SlotPantItem data = Instantiate(pantShopObj, pantShopTranform).GetComponent<UI_SlotPantItem>();
            data.SetUpItem(pantData.data[i].icon, pantData.data[i].nameItem, pantData.data[i].price, pantStatus[i], pantData.data[i].pantType, i, (a, b) => UpdateStatusPant(a, b));
            listUIPant.Add(data);
        }
    }
    public void SwitchTo(GameObject _memu)
    {
        for (int i = 0; i < shopParrent.childCount; i++)
        {
                shopParrent.GetChild(i).gameObject.SetActive(false);
        }
        if (_memu != null)
        {
            _memu.SetActive(true);
        }
    }

    public void Save(ref DataPlayer data)
    {
       
        data.weaponsStatus = weaponsStatus;
        data.hatStatus = hatStatus;
        data.pantStatus = pantStatus;
    }

    public void Load(DataPlayer data)
    {
        hatData = DataManager.instance.GetHatDataOS();
        pantData = DataManager.instance.GetPantDataOS();
        weaponData = DataManager.instance.GetWeaponDataOS();
        LoadWeaponStatus(data);
        LoadHatStatus(data);
        LoadPantStatus(data);
    }

    private void LoadWeaponStatus(DataPlayer data)
    {
        if (data.weaponsStatus == null || data.weaponsStatus.Count <= 0)
        {
            for (int i = 0; i < weaponData.data.Count; i++)
            {
                weaponsStatus.Add(0);
            }
        }
        else
        {
            weaponsStatus.Clear();
            weaponsStatus.AddRange(data.weaponsStatus);
        }
    }
    private void LoadPantStatus(DataPlayer data)
    {
        if (data.pantStatus == null || data.pantStatus.Count <= 0)
        {
            for (int i = 0; i < pantData.data.Count; i++)
            {
                pantStatus.Add(0);
            }
        }
        else
        {
            pantStatus.Clear();
            pantStatus.AddRange(data.pantStatus);
        }
    }
    private void LoadHatStatus(DataPlayer data)
    {
        if (data.hatStatus == null || data.hatStatus.Count <= 0)
        {
            for (int i = 0; i < hatData.data.Count; i++)
            {
                hatStatus.Add(0);
            }
        }
        else
        {
            hatStatus.Clear();
            hatStatus.AddRange(data.hatStatus);
        }
    }
    private void UpdateStatusWeapon(int status, int index)
    {
        weaponsStatus[index] = status;
        for(int i = 0;i < weaponsStatus.Count; i++)
        {
            if (index == i)continue;
            if (weaponsStatus[i] > 1)
            {
                listUIWeapon[i].UpdateStatus(1);
                weaponsStatus[i] = 1;
            }
        }
        DataManager.instance.SaveIDataSave();

    }
    private void UpdateStatusHat(int status, int index)
    {
        hatStatus[index] = status;
        for (int i = 0; i < hatStatus.Count; i++)
        {
            if (index == i) continue;
            if (hatStatus[i] > 1)
            {
                listUIHat[i].UpdateStatus(1);
                hatStatus[i] = 1;
            }
        }
        DataManager.instance.SaveIDataSave();
    }
    private void UpdateStatusPant(int status, int index)
    {
        pantStatus[index] = status;
        for (int i = 0; i < pantStatus.Count; i++)
        {
            if (index == i) continue;
            if (pantStatus[i] > 1)
            {
                listUIPant[i].UpdateStatus(1);
                pantStatus[i] = 1;
            }
        }
        DataManager.instance.SaveIDataSave();
    }
}
