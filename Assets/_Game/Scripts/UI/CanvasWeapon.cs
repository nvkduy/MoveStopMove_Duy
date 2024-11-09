using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWeapon : UICanvas
{
    public GameObject[] weapons;
    public int selectedCharacter;
    public WeaponItemData[] weaponItem;

    public void ChangeNext()
    {
        weapons[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter == weapons.Length)
        {
            selectedCharacter = 0;
        }
        weapons[selectedCharacter].SetActive(true);
        if (weaponItem[selectedCharacter].isUnlocked)
        {

        }
    }
}
