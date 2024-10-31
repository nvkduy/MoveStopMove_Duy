using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Bot> characters = new Dictionary<Collider, Bot>();

    public static Bot GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Bot>());
        }

        return characters[collider];
    }
}
