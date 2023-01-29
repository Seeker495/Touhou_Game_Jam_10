using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_totalUI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Display());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Display()
    {
        const string text = "YOUR SCORE IS...";
        foreach (var t in text)
        {
            m_totalUI.text += t;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        m_totalUI.text += $"{Parameter.TOTAL_DISTANCE}m!!";
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Parameter.TOTAL_DISTANCE);
        yield return null;
    }
}
