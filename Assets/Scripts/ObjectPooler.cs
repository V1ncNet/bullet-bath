using System.Collections.Generic;
using UnityEngine;

/*
 * script manages object pooling
 * 
 * placed on:       Game.Ground
 * author:          Johannes Mueller
 * last changed:    20.09.2021
 */

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("More than one Object Pooler in the Scene.");
            return;
        }
        instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // filling each pool
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            // enqueuing the desired amount of objects
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objPool);
        }
    }

    // the fancy way to Instantiate()
    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        //Debug.Log("Object " + tag + " spawned from Pool");

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool " + tag + "doesnt exist.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }
}
