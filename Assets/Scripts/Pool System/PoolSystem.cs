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

    Vector3[] m_frontSectorFace;
    Vector3[] m_backSectorFace;
    // Start is called before the first frame update
    void Start()
    {
        m_pool = new ObjectPool<BasePoolObject>(CreateObject,
                                                GetObject,
                                                ReleaseObject,
                                                DestroyObject);
        m_center = transform.position;
        //Clock-wise creation
        m_frontSectorFace = new Vector3[4];
        //Upper right
        m_frontSectorFace[0].x = m_center.x + SectorDimensions.x;
        m_frontSectorFace[0].y = m_center.y + SectorDimensions.y;
        m_frontSectorFace[0].z = m_center.z - SectorDimensions.z;
        //Lower right
        m_frontSectorFace[1].x = m_frontSectorFace[0].x;
        m_frontSectorFace[1].y = m_center.y - SectorDimensions.y;
        m_frontSectorFace[1].z = m_frontSectorFace[0].z;
        //Lower left
        m_frontSectorFace[2].x = m_center.x - SectorDimensions.x;
        m_frontSectorFace[2].y = m_frontSectorFace[1].y;
        m_frontSectorFace[2].z = m_frontSectorFace[0].z;
        //Upper Left
        m_frontSectorFace[3].x = m_frontSectorFace[2].x;
        m_frontSectorFace[3].y = m_frontSectorFace[0].y;
        m_frontSectorFace[3].z = m_frontSectorFace[0].z;

        m_backSectorFace = new Vector3[4];
        //Upper right
        m_backSectorFace[0].x = m_frontSectorFace[0].x;
        m_backSectorFace[0].y = m_frontSectorFace[0].y;
        m_backSectorFace[0].z = m_center.z + SectorDimensions.z;
        //Lower right
        m_backSectorFace[1].x = m_backSectorFace[0].x;
        m_backSectorFace[1].y = m_frontSectorFace[1].y;
        m_backSectorFace[1].z = m_backSectorFace[0].z;
        //Lower left
        m_backSectorFace[2].x = m_frontSectorFace[2].x;
        m_backSectorFace[2].y = m_backSectorFace[1].y;
        m_backSectorFace[2].z = m_backSectorFace[0].z;
        //Upper Left
        m_backSectorFace[3].x = m_backSectorFace[2].x;
        m_backSectorFace[3].y = m_backSectorFace[0].y;
        m_backSectorFace[3].z = m_backSectorFace[0].z;
    }

    // Update is called once per frame
    void Update() {
        //Draw spawn area
        //Front
        Debug.DrawLine(m_frontSectorFace[0], m_frontSectorFace[1], Color.green);
        Debug.DrawLine(m_frontSectorFace[1], m_frontSectorFace[2], Color.green);
        Debug.DrawLine(m_frontSectorFace[2], m_frontSectorFace[3], Color.green);
        Debug.DrawLine(m_frontSectorFace[3], m_frontSectorFace[0], Color.green);
        //Back
        Debug.DrawLine(m_backSectorFace[0], m_backSectorFace[1], Color.green);
        Debug.DrawLine(m_backSectorFace[1], m_backSectorFace[2], Color.green);
        Debug.DrawLine(m_backSectorFace[2], m_backSectorFace[3], Color.green);
        Debug.DrawLine(m_backSectorFace[3], m_backSectorFace[0], Color.green);
        //Intersections
        Debug.DrawLine(m_frontSectorFace[0], m_backSectorFace[0], Color.green);
        Debug.DrawLine(m_frontSectorFace[1], m_backSectorFace[1], Color.green);
        Debug.DrawLine(m_frontSectorFace[2], m_backSectorFace[2], Color.green);
        Debug.DrawLine(m_frontSectorFace[3], m_backSectorFace[3], Color.green);

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

    void GetObject(BasePoolObject pool) => pool.Reuse();

    void ReleaseObject(BasePoolObject pool) => pool.ResetObject();

    void DestroyObject(BasePoolObject pool) => Destroy(pool.gameObject);
}
