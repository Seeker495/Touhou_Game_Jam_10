using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody2D;
    [SerializeField] private List<Sprite> m_playerSprites;
    [SerializeField] private List<Sprite> m_snowBallSprites;
    [SerializeField] private SpriteRenderer m_spriteRenderer_Player;
    [SerializeField] private SpriteRenderer m_spriteRenderer_SnowBall;
    private Vector2 m_startPosition;
    private Vector2 m_initVelocity;

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        m_initVelocity = Vector2.right;
        m_rigidBody2D.velocity = m_initVelocity;
        m_startPosition = m_rigidBody2D.position;
        m_rigidBody2D.velocity = Vector2.right * 10;

        m_rigidBody2D.AddForce(Vector2.right * 10, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        Parameter.TOTAL_DISTANCE = (long)(m_rigidBody2D.position.x - m_startPosition.x);
        Debug.Log(Parameter.TOTAL_DISTANCE);
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.velocity = new Vector2(Mathf.Clamp(m_rigidBody2D.velocity.x, 1, m_rigidBody2D.velocity.x), m_rigidBody2D.velocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        m_rigidBody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }

    private void Descend(InputAction.CallbackContext context)
    {

    }
}
