using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class BasePoolObject : MonoBehaviour
{
    [SerializeField]
    public float MaxLifeTime;
    protected float m_lifeTime;

    protected IObjectPool<BasePoolObject> m_pool;
    protected Transform m_transform;

    protected Vector3 m_center;
    protected Vector3 m_extents;
    void Awake()
    {
        m_transform = transform;
    }

    public virtual void Reuse() 
    {
        
    }

    public virtual void Init(Vector3 center, Vector3 offset) 
    {
        m_center = center;
        m_extents = offset;
        Reuse();
    }

    public void SetPool(IObjectPool<BasePoolObject> pool) => m_pool = pool;

    public virtual void ResetObject() {
        
    }
}
