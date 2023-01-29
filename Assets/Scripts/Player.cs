using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody2D;
    [SerializeField] private Animator m_playerAnimator;
    [SerializeField] private Animator m_snowBallAnimator;
    private Vector2 m_startPosition;
    private Vector2 m_initVelocity;
    private int m_life = 1;
    public enum ePlayerState
    {
        RUNNING,
        JUMP,
        FALL,
        DEAD,
    }

    private ePlayerState m_state = ePlayerState.RUNNING;

    private void OnEnable()
    {
        PlayerController.CONTROLLER.Player.Enable();
        PlayerController.CONTROLLER.Player.Jump.started += Jump;
        PlayerController.CONTROLLER.Player.Descend.performed += Descend;
        PlayerController.CONTROLLER.Player.Descend.canceled += DescendEnd;


    }

    private void OnDisable()
    {
        PlayerController.CONTROLLER.Player.Enable();
        PlayerController.CONTROLLER.Player.Jump.started -= Jump;
        PlayerController.CONTROLLER.Player.Descend.performed -= Descend;
        PlayerController.CONTROLLER.Player.Descend.canceled -= DescendEnd;
    }

    private void Awake()
    {
        SetAnimation();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_initVelocity = Vector2.right;
        m_rigidBody2D.velocity = m_initVelocity;
        m_startPosition = m_rigidBody2D.position;
        m_rigidBody2D.velocity = Vector2.right * Parameter.PLAYER_SPEED;

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
        if (m_rigidBody2D.velocity.y < 0) m_state = ePlayerState.FALL;
        m_rigidBody2D.velocity = new Vector2(Mathf.Clamp(m_rigidBody2D.velocity.x, Parameter.PLAYER_SPEED * 0.5f, m_rigidBody2D.velocity.x), m_rigidBody2D.velocity.y);
        SetAnimation();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        m_rigidBody2D.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        m_state = ePlayerState.JUMP;
    }

    private void Descend(InputAction.CallbackContext context)
    {
        if(m_rigidBody2D.velocity.y < 0)
            m_rigidBody2D.velocity = Vector2.right * 10 * 1.5f;
    }

    private void DescendEnd(InputAction.CallbackContext context)
    {
        m_rigidBody2D.velocity = Vector2.right * 10;
    }

    private void SetAnimation()
    {
        int isRunning = Animator.StringToHash("IsRunning");
        int isJump = Animator.StringToHash("IsJump");
        int isFall = Animator.StringToHash("IsFall");
        switch (m_state)
        {
            case ePlayerState.RUNNING:
                m_playerAnimator.speed = m_rigidBody2D.velocity.x / 10.0f;
                m_snowBallAnimator.speed = m_rigidBody2D.velocity.x / 10.0f;
                m_playerAnimator.SetBool(isRunning, true);
                m_playerAnimator.SetBool(isJump, false);
                m_playerAnimator.SetBool(isFall, false);
                break;
            case ePlayerState.JUMP:
                m_playerAnimator.speed = 0.2f;
                m_playerAnimator.SetBool(isRunning, false);
                m_playerAnimator.SetBool(isJump, true);
                m_playerAnimator.SetBool(isFall, false);
                break;
            case ePlayerState.FALL:
                m_playerAnimator.speed = 0.2f;
                m_playerAnimator.SetBool(isRunning, false);
                m_playerAnimator.SetBool(isJump, false);
                m_playerAnimator.SetBool(isFall, true);
                break;
        }
    }

    public bool IsAlive()
    {
        return 0 < m_life;
    }

    public bool IsDead()
    {
        return !IsAlive();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDead())
        {
            Dead();
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            m_life--;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
#if !UNITY_EDITOR
        Fade.FadeOut_with_Scene(this, "Title");
#endif
    }
}
