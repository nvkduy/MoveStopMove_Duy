using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorSkinType
{
    Red = 0, Green = 1, Blue = 2,
}

[CreateAssetMenu(fileName = "Colorskin", menuName = "ScriptableObject/ColorSkin", order = 1)]
public class ColorSkin : ScriptableObject
{
    //[SerializeField] Material 
}
