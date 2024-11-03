using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="color",menuName ="Data/Color")]
public class ColorItemData : ScriptableObject
{
    public Color color;
    public Material material;
}
