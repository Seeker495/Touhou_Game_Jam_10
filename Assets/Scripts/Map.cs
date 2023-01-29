using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap m_tileMap;
    [SerializeField] private List<Tile> m_tiles;
    // Start is called before the first frame update
    void Start()
    {
        m_tileMap.SetTile(new Vector3Int(-11, -5, 0), m_tiles[16]);
        m_tileMap.SetTile(new Vector3Int(-9, -5, 0), m_tiles[3]);
        m_tileMap.SetTile(new Vector3Int(-8, -5, 0), m_tiles[3]);
        m_tileMap.SetTile(new Vector3Int(-7, -5), m_tiles[3]);
        m_tileMap.SetTile(new Vector3Int(-6, -5), m_tiles[13]);

        for (int i= 0; i < 22;++i)
        {
        }
        //m_tileMap.SetTile(new Vector3Int(-4, -5, 0), m_tiles[4]);
        //m_tileMap.SetTile(new Vector3Int(-5, -5, 0), m_tiles[3]);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
