using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> characterCache = new Dictionary<Collider, Character>();
    private static Dictionary<GameObject, Bot> botCache = new Dictionary<GameObject, Bot>();
    private static Dictionary<GameObject, Character> characterObjCache = new Dictionary<GameObject, Character>();
    private static Dictionary<GameObject, Rigidbody> rigidbodyCache = new Dictionary<GameObject, Rigidbody>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characterCache.ContainsKey(collider))
        {
            characterCache.Add(collider, collider.GetComponent<Character>());
        }

        return characterCache[collider];
    }

    // Method to create cache Get component Bot from object without using collider
    public static Bot GetBot(GameObject gameObject)
    {
        if (!botCache.ContainsKey(gameObject))
        {
            botCache.Add(gameObject, gameObject.GetComponent<Bot>());
        }
        return botCache[gameObject];
    }

    public static Character GetCharacterObj(GameUnit gameUnit)
    {
        GameObject gameObject = gameUnit.gameObject;
        if (!characterObjCache.ContainsKey(gameObject))
        {
            characterObjCache.Add(gameObject, gameObject.GetComponent<Character>());
        }
        return characterObjCache[gameObject];
    }

    public static Rigidbody GetRigidbody(GameUnit gameUnit)
    {
        GameObject gameObject = gameUnit.gameObject;
        if (!rigidbodyCache.ContainsKey(gameObject))
        {
            rigidbodyCache.Add(gameObject, gameObject.GetComponent<Rigidbody>());
        }
        return rigidbodyCache[gameObject];
    }
}
