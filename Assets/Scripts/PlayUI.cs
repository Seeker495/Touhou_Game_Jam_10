using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_scoreText.SetText($"DISTANCE: {string.Format("{0,20}",Parameter.TOTAL_DISTANCE.ToString("#,#"))}m");
    }
}
