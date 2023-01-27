using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject m_soundManager;
    [SerializeField] private GameObject m_fadeObject;
    // Start is called before the first frame update
    void Start()
    {
        SoundPlayer.SetUp(Instantiate(m_soundManager, null));
        SoundPlayer.PlayBGM(eBGM.TITLE);
        Fade.SetUp(m_fadeObject);
        Fade.FadeIn(2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
