using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] float MaxLifeTime;
    float m_lifeTime = 0f;

    IObjectPool<PoolObject> m_pool;

    Transform m_transform;
    // Start is called before the first frame update
    void Awake()
    {
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_lifeTime += Time.deltaTime;
        if(m_lifeTime >= MaxLifeTime)
        {
            m_pool.Release(this);
        }
    }

    public void SetPool(IObjectPool<PoolObject> pool) => m_pool = pool;

    public void ResetObject()
    {
        m_lifeTime = 0f;
        m_transform.localScale = Vector3.one;
        m_transform.localPosition = Vector3.zero;
        m_transform.localRotation = Quaternion.identity;
    }

    public void RandomTransform()
    {
        m_transform.localScale = new Vector3(Random.Range(.5f, 2f),
                                             Random.Range(.5f, 2f),
                                             Random.Range(.5f, 2f));
        m_transform.localRotation = Random.rotation;
        m_transform.localPosition = new Vector3(Random.Range(-10f, 10f),
                                                Random.Range(-10f, 10f),
                                                Random.Range(-10f, 10f));
    }
}
