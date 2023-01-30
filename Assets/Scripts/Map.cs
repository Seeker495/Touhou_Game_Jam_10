using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject m_copyObject;
    [SerializeField] private Tilemap m_tileMap;
    [SerializeField] private List<Tile> m_tiles;
    private int m_startPosition = -12;
    private bool m_canGenerate = false;
    private const long GENERATE_DISTANCE = 100;
    private int m_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //RandomGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Parameter.TOTAL_DISTANCE % (GENERATE_DISTANCE) == GENERATE_DISTANCE - 10)
        {
            m_canGenerate = true;
            RandomGenerate();
            m_canGenerate = false;
        }
    }

    private void FixedUpdate()
    {
        //if (m_canGenerate) return;


    }

    void RandomGenerate()
    {
        m_count++;
        Instantiate(m_copyObject, new Vector3((GENERATE_DISTANCE + 1) * m_count, 0.0f, 0.0f), Quaternion.identity, transform);
    }
}
