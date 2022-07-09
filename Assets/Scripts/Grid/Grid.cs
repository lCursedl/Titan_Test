using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    Tile TilePrefab;

    int Width = 4;
    int Height = 4;
    Tile[,] m_grid;
    Vector2Int m_lastTile;
    Vector3 tileSize;
    // Start is called before the first frame update
    void Start() {
        m_lastTile.Set(-1, -1);
        tileSize = TilePrefab.gameObject.transform.localScale;
        m_grid = new Tile[Width, Height];

        for(int x = 0; x < Width; ++x)
        {
            for(int y = 0; y < Height; ++y)
            {
                m_grid[x, y] = Instantiate(TilePrefab,
                                           new Vector3(tileSize.x * x,
                                                       transform.position.y - 5f,
                                                       tileSize.z * y),
                                           Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLastTile(Vector2Int tilePos)
    {
        m_lastTile = tilePos;
    }

    public Vector2Int GetLastTile()
    {
        return m_lastTile;
    }

    public Vector3 TileToWorldPos(Vector2Int tilePos)
    {
        if (tilePos.x < 0 && tilePos.x >= Width &&
           tilePos.y < 0 && tilePos.y >= Height)
        {
            return new Vector3(-1f, -1f, -1f);
        }
        return m_grid[tilePos.x, tilePos.y].transform.position;
    }

    public Vector2Int WorldToTilePos(Vector3 worldPos)
    {
        Vector2 mapCoord = new Vector2(worldPos.x / tileSize.x,
                                       worldPos.z / tileSize.z);
        return Vector2Int.RoundToInt(mapCoord);
    }
}
