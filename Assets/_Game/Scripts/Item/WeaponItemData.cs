using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    
    Axe0 = 0,
    Axe1 = 1,
    Boomerang= 2,
    None = 100
}

[CreateAssetMenu(fileName = "WeaponItemData",menuName = "ScriptableObject/WeaponItemData",order =1)]
public class WeaponItemData:ItemData
{
    [SerializeField] Weapon weapon;
    [SerializeField] WeaponType weaponType;

    public Weapon Weapon => weapon;
    public WeaponType WeaponType => weaponType;
    private void OnEnable()
    {
        isUnlocked = PlayerPrefs.GetInt(GetPlayerPrefsKey(), 0) == 1;
    }
    public override void SaveUnlockState()
    {
        PlayerPrefs.SetInt(GetPlayerPrefsKey(),isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }
    private string GetPlayerPrefsKey()
    {
        return $"Weapon_{weaponType}_Unlocked";
    }
}

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public int price;
    public bool isUnlocked;
    public Sprite icon;
    // Phương thức trừu tượng để lưu trạng thái mở khóa
    // sử dụng cho lớp cơ sở khi muốn định nghĩa khuôn mẫu chung cho lớp con
    public abstract void SaveUnlockState();
}