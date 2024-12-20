using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewHatsData",menuName = "ScriptableObject/HatsData",order = 1)]
public class HatsData : ScriptableObject
{
    [SerializeField] List <HatsItemData> hatsItemDatas;
    public int CountHats => hatsItemDatas.Count;
    public HatsItemData GetHat(HatsType hatsType)
    {
        return hatsItemDatas[(int)hatsType];
    }
}
