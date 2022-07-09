using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PO_Move : BasePoolObject
{
    [SerializeField]
    float Speed;

    ExitCount TextCount;

    Rigidbody m_rb;
    Bounds m_bounds;

    void Start()
    {
        m_rb = gameObject.GetComponent<Rigidbody>();
        TextCount = GameObject.FindGameObjectWithTag("UI").GetComponent<ExitCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bounds.Contains(transform.position))
        {
            m_rb.velocity = transform.forward * Speed;
        }
        else
        {
            TextCount.AddCount();
            m_pool.Release(this);
        }
    }

    public override void Reuse()
    {
        base.Reuse();
        transform.position = m_center;
        Vector3 dir = Random.onUnitSphere + m_center;
        transform.LookAt(dir);
        gameObject.SetActive(true);
    }

    public override void Init(Vector3 center, Vector3 offset)
    {
        base.Init(center, offset);
        m_bounds = new Bounds(center, offset * 2);
        Reuse();
    }

    public override void ResetObject()
    {
        base.ResetObject();
        gameObject.SetActive(false);
        m_rb.velocity.Set(0f, 0f, 0f);
        m_rb.angularVelocity.Set(0f, 0f, 0f);
        transform.rotation = Quaternion.identity;
    }
}
