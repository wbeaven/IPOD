using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public string tag;
    public GameObject objectToPool;
    public int amountToPool;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<PoolItem> itemsToPool;
    private Dictionary<string, List<GameObject>> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();

        foreach (PoolItem item in itemsToPool)
        {
            List<GameObject> objectList = new List<GameObject>();

            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject tmp = Instantiate(item.objectToPool);
                tmp.SetActive(false);
                objectList.Add(tmp);
            }

            pooledObjects.Add(item.tag, objectList);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        if (!pooledObjects.ContainsKey(tag))
        {
            Debug.LogWarning("No pool with tag " + tag);
            return null;
        }

        List<GameObject> objects = pooledObjects[tag];

        foreach (var obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Optional: expand pool if needed
        PoolItem item = itemsToPool.Find(x => x.tag == tag);
        if (item != null)
        {
            GameObject tmp = Instantiate(item.objectToPool);
            tmp.SetActive(false);
            objects.Add(tmp);
            return tmp;
        }

        return null;
    }
}
