using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    Dictionary<Key.KeyType, int> keyList;
    private void Awake()
    {
        keyList = new Dictionary<Key.KeyType, int>();
        keyList.Add(Key.KeyType.Gold, 0);
        keyList.Add(Key.KeyType.Red, 0);
    }

    public void AddKey(Key.KeyType keyType)
    {
        keyList[keyType]++;
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList[keyType]--;
        if (keyList[keyType] < 0)
        {
            keyList[keyType] = 0;
        }
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList[keyType] > 0;
    }
}