using UnityEngine;

public enum HatsType
{
    CowBoy = 0,
    Crown = 1,
    Ear = 2,
    None = 100
}
[CreateAssetMenu(fileName = "HatsItemData", menuName = "ScriptableObject/HatsItemData", order = 1)]
public class HatsItemData : ItemData
{
    [SerializeField] HatsType hatsType;
    [SerializeField] Hats hats;

    public Hats Hats => hats;
    public HatsType HatType => hatsType;
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
        return $"Hat_{hatsType}_Unlocked";
    }
}
