using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;

    public static ObjectPoolItem newItem(int amountToPool, GameObject objectToPool, bool shouldExpand) {
        var newPoolItem = new ObjectPoolItem();
        newPoolItem.amountToPool = amountToPool;
        newPoolItem.objectToPool = objectToPool;
        newPoolItem.shouldExpand = shouldExpand;
        return newPoolItem;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public AttackHandler attackHandler;

    public AbilityHandler abilityHandler;

    public static ObjectPooler SharedInstance;

    public List<ObjectPoolItem> itemsToPool;

    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();

        if (attackHandler != null)
        {
            foreach (RangedAttacks rangedAttack in attackHandler.rangedAttacks)
            {
                ObjectPoolItem newPoolItem = ObjectPoolItem.newItem(3, rangedAttack.gameObject, true);
                itemsToPool.Add(newPoolItem);
            }
        }

        if (abilityHandler != null)
        {
            foreach (Abilities ability in abilityHandler.Abilities)
            {
                if (ability != null)
                {
                    ObjectPoolItem newPoolItem = ObjectPoolItem.newItem(2, ability.gameObject, true);
                    itemsToPool.Add(newPoolItem);
                }
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(this.transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    obj.transform.SetParent(this.transform);
                    return obj;
                }
            }
        }
        return null;
    }
}
