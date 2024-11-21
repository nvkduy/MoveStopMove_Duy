using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPantData", menuName = "ScriptableObject/PantData", order = 1)]
public class PantData : ScriptableObject
{
    [SerializeField] List<PantItemData> pantItemDatas;

    public int CountPant => pantItemDatas.Count;

    public PantItemData GetPant(PantsType pantsType)
    {
        return pantItemDatas[(int)pantsType];
    }
}
