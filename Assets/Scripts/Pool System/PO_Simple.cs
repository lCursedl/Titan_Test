using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PO_Simple : BasePoolObject
{
    // Update is called once per frame
    void Update()
    {
        m_lifeTime += Time.deltaTime;
        if (m_lifeTime >= MaxLifeTime)
        {
            m_pool.Release(this);
        }
    }

    public override void Reuse()
    {
        base.Reuse();
        RandomTransform(m_center, m_extents);
        gameObject.SetActive(true);
    }

    public override void Init(Vector3 center, Vector3 offset)
    {
        base.Init(center, offset);
    }

    public override void ResetObject()
    {
        base.ResetObject();
        gameObject.SetActive(false);
        m_lifeTime = 0f;
        m_transform.localScale = Vector3.one;
        m_transform.localPosition = m_center;
        m_transform.localRotation = Quaternion.identity;
    }

    void RandomTransform(Vector3 center, Vector3 offset)
    {
        m_transform.localScale = new Vector3(Random.Range(.1f, 1f),
                                             Random.Range(.1f, 1f),
                                             Random.Range(.1f, 1f));
        m_transform.localRotation = Random.rotation;
        m_transform.localPosition = new Vector3(Random.Range(center.x - offset.x, center.x + offset.x),
                                                Random.Range(center.y - offset.y, center.y + offset.y),
                                                Random.Range(center.z - offset.z, center.z + offset.z));
    }
}
