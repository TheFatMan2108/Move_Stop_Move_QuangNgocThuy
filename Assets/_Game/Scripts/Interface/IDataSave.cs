using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSave 
{
    void Save(ref DataPlayer data);
    void Load(DataPlayer data);
}
