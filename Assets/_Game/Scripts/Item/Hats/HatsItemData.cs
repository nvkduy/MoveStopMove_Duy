using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HatType
{
    CowBoy = 0,
    Crown = 1,
    Ear = 2,
    None = 100
}
[CreateAssetMenu(fileName = "HatsItemData", menuName = "ScriptableObject/HatsItemData", order = 1)]
public class HatsItemData : ItemData
{
    [SerializeField] HatType hatType;
    [SerializeField] Character character;

    public Character Character =>character;
    public HatType HatType => hatType;
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
        return $"Hats_{hatType}_Unlocked";
    }
}
