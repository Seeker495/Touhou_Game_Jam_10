using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [SerializeField] private GameObject m_soundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundPlayer.SetUp(Instantiate(m_soundManager, null));
        SoundPlayer.PlayBGM(eBGM.TITLE);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
