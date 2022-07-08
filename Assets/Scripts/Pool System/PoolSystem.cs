using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    ObjectPool<BasePoolObject> m_pool;
    [SerializeField] BasePoolObject CubePrefab;
    [SerializeField] float SpawnInterval;
    [SerializeField] int Capacity;
    [SerializeField] bool UseInterval;
    [SerializeField] bool UseCapacity;
    [SerializeField] Vector3 SectorDimensions;

    float m_interval = 0f;
    Vector3 m_center;
    // Start is called before the first frame update
    void Start()
    {
        m_pool = new ObjectPool<BasePoolObject>(CreateObject,
                                                GetObject,
                                                ReleaseObject,
                                                DestroyObject);
        m_center = transform.position;
    }

    // Update is called once per frame
    void Update() {
        m_interval = UseInterval ? m_interval + Time.deltaTime : 0f;

        if(m_interval >= SpawnInterval)
        {
            if(UseCapacity)
            {
                if(m_pool.CountActive == Capacity)
                {
                    return;
                }
            }
            m_pool.Get();
            m_interval = 0f;
        }    
    }

    BasePoolObject CreateObject()
    {
        BasePoolObject obj =  Instantiate<BasePoolObject>(CubePrefab);
        obj.SetPool(m_pool);
        if(UseCapacity && UseInterval)
        {
            obj.MaxLifeTime = (SpawnInterval * Capacity);
        }
        obj.Init(m_center, SectorDimensions);
        return obj;
    }

    void GetObject(BasePoolObject pool) => pool.Init(m_center, SectorDimensions);

    void ReleaseObject(BasePoolObject pool) => pool.ResetObject();

    void DestroyObject(BasePoolObject pool) => Destroy(pool.gameObject);
}
