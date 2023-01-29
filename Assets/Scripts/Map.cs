using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap m_tileMap;
    [SerializeField] private List<Tile> m_tiles;
    private int m_startPosition = -12;
    private bool m_canGenerate = false;
    private const long GENERATE_DISTANCE = 20;
    // Start is called before the first frame update
    void Start()
    {
        RandomGenerate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (m_canGenerate) return;

        if(Parameter.TOTAL_DISTANCE % (GENERATE_DISTANCE / 2) == 0)
        {
            m_canGenerate = true;
            RandomGenerate();
            m_canGenerate = false;
        }
    }

    void RandomGenerate()
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        List<Tile> tiles = new List<Tile>();
        for(int i= 0; i < GENERATE_DISTANCE; ++i)
        {
            m_startPosition++;
            positions.Add(new Vector3Int(m_startPosition, -7));
            tiles.Add(m_tiles[4]);
            positions.Add(new Vector3Int(m_startPosition, -6));
            tiles.Add(m_tiles[1]);

        }

        m_tileMap.SetTiles(positions.ToArray(), tiles.ToArray());
    }
}
