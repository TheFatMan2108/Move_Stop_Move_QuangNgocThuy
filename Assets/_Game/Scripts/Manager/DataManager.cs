using System.Collections;

using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance {  get; private set; }
    WeaponData weaponData;
    HatData hatData;
    PantData pantData;
    ColorData colorData;
    private void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(gameObject);
    }
    public WeaponData GetWeaponDataOS()
    {
        if (weaponData == null)
            weaponData   =  Resources.Load<WeaponData>(string.Format("Data/WeaponsData/{0}", CacheString.DataWeaponsAddress));
        return weaponData;
    }
    public HatData GetHatDataOS()
    {
        if (hatData == null)
            hatData = Resources.Load<HatData>(string.Format("Data/HatData/{0}", CacheString.DataHatAddress));
        return hatData;
    }
    public PantData GetPantDataOS()
    {
        if (pantData == null)
            pantData = Resources.Load<PantData>(string.Format("Data/PantData/{0}", CacheString.DataPantAddress));
        return pantData;
    }
    public ColorData GetColorDataOS()
    {
        if (colorData == null)
            colorData = Resources.Load<ColorData>(string.Format("Data/ColorData/{0}", CacheString.DataColorAddress));
        return colorData;
    }

}
