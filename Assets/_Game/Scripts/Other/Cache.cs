using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Bot> bot = new Dictionary<Collider, Bot>();
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();
    public static Bot GetBot(Collider collider)
    {
        if (!bot.ContainsKey(collider))
        {
            bot.Add(collider, collider.GetComponent<Bot>());
        }

        return bot[collider];
    }
    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
            
        }

        return characters[collider];
    }


}
