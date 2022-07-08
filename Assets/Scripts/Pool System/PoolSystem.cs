using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    ObjectPool<PoolObject> m_pool;
    [SerializeField] PoolObject CubePrefab;
    [SerializeField] float SpawnInterval;
    [SerializeField] int Capacity;
    [SerializeField] bool UseInterval;
    [SerializeField] bool UseCapacity;
    float m_interval = 0f;
    int m_capacity = 0;
    // Start is called before the first frame update
    void Start() {
        m_pool = new ObjectPool<PoolObject>(CreateObject,
                                            GetObject,
                                            ReleaseObject,
                                            DestroyObject);
    }

    // Update is called once per frame
    void Update() {
        m_interval = UseInterval ? m_interval + Time.deltaTime : 0f;

        if(m_interval >= SpawnInterval)
        {
            if(Capacity > 0)
            {
                if(m_capacity < Capacity)
                {
                    m_pool.Get();
                    ++m_capacity;
                }
                else
                {
                    return;
                }
            }
            else
            {
                m_pool.Get();
            }
        }    
    }

    PoolObject CreateObject()
    {
        PoolObject obj =  Instantiate<PoolObject>(CubePrefab);        
        obj.SetPool(m_pool);
        obj.RandomTransform();
        return obj;
    }

    void GetObject(PoolObject pool)
    {
        pool.RandomTransform();
        pool.gameObject.SetActive(true);
    }

    void ReleaseObject(PoolObject pool)
    {
        pool.gameObject.SetActive(false);
        pool.ResetObject();
    }

    void DestroyObject(PoolObject pool)
    {
        Destroy(pool.gameObject);
    }
}
