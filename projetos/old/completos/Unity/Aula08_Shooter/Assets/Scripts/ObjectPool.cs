using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;

    private Queue<GameObject> queue;
    private int poolSize;

    public ObjectPool(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        this.poolSize = poolSize;
        queue = new Queue<GameObject>();

        for(int i = 0; i < this.poolSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject obj = queue.Peek();
        if(obj.activeSelf)
            return null;

        queue.Dequeue();
        queue.Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
