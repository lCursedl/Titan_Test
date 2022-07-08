using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class BasePoolObject : MonoBehaviour
{
    protected IObjectPool<BasePoolObject> m_pool;
    protected Transform m_transform;

    [SerializeField]
    public float MaxLifeTime;
    protected float m_lifeTime;

    void Awake()
    {
        m_transform = transform;
    }

    public virtual void Init(Vector3 center, Vector3 offset) 
    {
        gameObject.SetActive(true);
    }

    public void SetPool(IObjectPool<BasePoolObject> pool) => m_pool = pool;

    public virtual void ResetObject() {
        gameObject.SetActive(false);
    }
}
