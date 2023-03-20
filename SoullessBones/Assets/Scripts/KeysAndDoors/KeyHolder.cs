using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public Dictionary<Key.KeyType, int> keyList; // предмет(ключ) - количество
    [SerializeField] int golden, red;
    private void Update()
    {
        golden = keyList[Key.KeyType.Gold];
        red = keyList[Key.KeyType.Red];
    }
    public void AddKey(Key.KeyType keyType)
    {
        keyList[keyType]++;
        SceneLoader.instance.GetComponentInChildren<GoldKeyUI>().AddKey();
        SceneStats.keycounter++; 
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList[keyType]--;
        SceneStats.keycounter--;
        if (keyList[keyType] < 0)
        {
            keyList[keyType] = 0;
            SceneStats.keycounter = 0;
        }
        SceneLoader.instance.GetComponentInChildren<GoldKeyUI>().RemoveKey();
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList[keyType] > 0;
    }
}