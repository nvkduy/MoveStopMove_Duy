using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantsType
{
    BatMan = 0,
    Skull = 1,
    Onion = 2,

}
[CreateAssetMenu(fileName = "PantItemData", menuName = "ScriptableObject/PantItemData", order = 1)]
public class PantItemData : ItemData
{
    [SerializeField] private PantsType pantsType;
    [SerializeField] Material material;
    public Material Material => material;
    public PantsType PantsType => pantsType;
    private void OnEnable()
    {
        isUnlocked = PlayerPrefs.GetInt(GetPlayerPrefsKey(), 0) == 1;
    }
    public override void SaveUnlockState()
    {
        PlayerPrefs.SetInt(GetPlayerPrefsKey(), isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }
    private string GetPlayerPrefsKey()
    {
        return $"Pant_{pantsType}_Unlocked";
    }
}
