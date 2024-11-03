using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Colors",menuName ="Data/DataOS/ColorData")]
public class ColorData : ScriptableObject
{
    public List<ColorItemData> data;

    public ColorItemData GetColor(ColorType type)
    {
        return data[(int)type];
    }

}
