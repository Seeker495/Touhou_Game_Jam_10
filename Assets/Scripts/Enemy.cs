using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private List<Sprite> m_sprites;
    private bool m_isAttack = false;
    private GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Parameter.TOTAL_DISTANCE >= 50) {
        transform.DOMoveX(m_player.transform.position.x + 5.0f, 0.5f);
		transform.DOMoveY(m_player.transform.position.y, 0.5f);
		}
    }

    void Attack()
    {

    }
}
