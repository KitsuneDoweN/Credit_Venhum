using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private CharAttack m_attack;
    private CharMove m_move;
    private Rigidbody2D rigid;
    //private PlayerAnimation m_playerAnimation;
    [SerializeField] 
    private PlayerAnimation m_playerAnimation;

    [SerializeField]
    private float playerDashStamina = 10;
    public float playerHp = 100.0f;
    
    void Init()
    {
        m_attack = GetComponent<CharAttack>();
        m_move = GetComponent<CharMove>();
        rigid = GetComponent<Rigidbody2D>();
        //m_playerAnimation = GetComponent<PlayerAnimation>();

        m_attack.Init(rigid);
        m_move.Init(rigid);
    }
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        m_move.Move();
        m_attack.Attack();
        m_move.Dash(playerDashStamina);
        //m_playerAnimation.Animator();
    }
    public void TakeDamage(float damage)
    {
        playerHp = playerHp - damage;
    }



    
}
