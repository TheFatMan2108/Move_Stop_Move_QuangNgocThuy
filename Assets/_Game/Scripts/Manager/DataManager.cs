using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance {  get; private set; }
    List<IDataSave> datas = new List<IDataSave>();
    DataPlayer data;
    WeaponData weaponData;
    HatData hatData;
    PantData pantData;
    ColorData colorData;
    private void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(gameObject);
        string dataString = PlayerPrefs.GetString(CacheString.KEY_DATA,"");
        if(dataString.Equals(""))
            data = new DataPlayer();
        else data = JsonUtility.FromJson<DataPlayer>(dataString);
        datas = FindAllDataPersistenceObjects();
        
    }
    private void Start()
    {
        LoadIDataSave();
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
    //
    public void AddIDataSave(IDataSave dataSave)=>datas.Add(dataSave);
    public void RemoveIDataSave(IDataSave dataSave)=>datas.Remove(dataSave);

    public void SaveIDataSave()
    {
        foreach (var item in datas)
        {
            item.Save(ref data);
        }
        string dataString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(CacheString.KEY_DATA,dataString);
    }
    public void LoadIDataSave()
    {
        foreach (var item in datas)
        {
            item.Load(data);
        }
    }
    private List<IDataSave> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataSave> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataSave>();

        return new List<IDataSave>(dataPersistenceObjects);
    }
    public int GetMaxScore()=>data.highScore;
}
