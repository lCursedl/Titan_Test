using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PO_Grid : BasePoolObject
{
    Grid m_gridObject;
    Vector2Int m_tilePos;

    public override void Reuse()
    {
        base.Reuse();
        do
        {
            m_tilePos.Set(Random.Range(0, 3),
                          Random.Range(0, 3));
        }
        while (m_tilePos == m_gridObject.GetLastTile());
        Vector3 spawnPos = m_gridObject.TileToWorldPos(m_tilePos);
        m_transform.localPosition = new Vector3(spawnPos.x, m_center.y, spawnPos.z);
        gameObject.SetActive(true);
    }

    public override void Init(Vector3 center, Vector3 offset)
    {
        base.Init(center, offset);
        m_gridObject = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        Vector3 spawnPos = m_gridObject.TileToWorldPos(new Vector2Int(Random.Range(0, 3),
                                                                      Random.Range(0, 3)));
        m_transform.localPosition = new Vector3(spawnPos.x, center.y, spawnPos.z);
        gameObject.SetActive(true);
    }

    public override void ResetObject()
    {
        base.ResetObject();
        gameObject.SetActive(false);
        m_gridObject.SetLastTile(m_gridObject.WorldToTilePos(m_transform.position));
    }

    void OnMouseDown()
    {
        m_pool.Release(this);
    }
}
