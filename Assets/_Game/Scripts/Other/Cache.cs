using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> characterCache = new Dictionary<Collider, Character>();
    private static Dictionary<GameObject, Bot> botCache = new Dictionary<GameObject, Bot>();
    private static Dictionary<GameObject,Level> levelCache = new Dictionary<GameObject, Level>();
    public static Character GetCharacter(Collider collider)
    {
        if (!characterCache.ContainsKey(collider))
        {
            characterCache.Add(collider, collider.GetComponent<Character>());
            
        }

        return characterCache[collider];
    }

    // metho to creat cache Get component Bot from objcet không sử dụng collider
    public static Bot GetBot(GameObject gameObject)
    {
        if (!botCache.ContainsKey(gameObject))
        {
            botCache.Add(gameObject, gameObject.GetComponent<Bot>());
        }
        return botCache[gameObject];
    }

    public static Level GetLevel(GameObject gameObject)
    {
        if (!levelCache.ContainsKey(gameObject))
        {
            levelCache.Add(gameObject, gameObject.GetComponent<Level>());
        }
        return levelCache[gameObject];
    }




}
