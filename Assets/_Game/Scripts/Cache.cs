using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<GameObject, Character> characters = new Dictionary<GameObject, Character>();
    public static Character GetCharacter(GameObject obj)
    {
        if (!characters.ContainsKey(obj))
        {
            Character character = obj.GetComponent<Character>();
            if (character != null) // Chỉ thêm nếu thành phần tồn tại
            {
                characters.Add(obj, character);
            }
        }

        return characters.ContainsKey(obj) ? characters[obj] : null;
    }
}
