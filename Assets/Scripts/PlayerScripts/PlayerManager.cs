using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private CharAttack m_attack;
    private CharMove m_move;
    private Hp m_hp;
    private Rigidbody2D rigid;

    [SerializeField] 
    private PlayerAnimation m_playerAnimation;

    [SerializeField]
    private float playerDashStamina = 10;

    [SerializeField]
    private GameObject g_Interaction;

    public float playerHp = 5;

    private float Interaction_CurTime;
    private float Interaction_coolTime = 0.5f;
    void Init()
    {
        m_attack = GetComponent<CharAttack>();
        m_move = GetComponent<CharMove>();
        m_hp = GetComponent<Hp>();
        rigid = GetComponent<Rigidbody2D>();

        m_attack.Init(rigid);
        m_move.Init(rigid);
    }
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        m_hp.health = playerHp;
        m_move.Move();
        m_attack.Attack();
        m_move.Dash(playerDashStamina);
        m_hp.PlayerHpUI();
        Interaction();
    }
    public void TakeDamage(float damage)
    {
        playerHp = playerHp - damage;
        float x = transform.position.x;
        if (x < 0)
            x = 1;
        else
            x = -1;
    }

    private void Interaction()
    {
        if (Interaction_CurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                g_Interaction.SetActive(true);
                Interaction_CurTime = Interaction_coolTime;
            }
            else
            {
                g_Interaction.SetActive(false);
            }
        }
        
        
        else
        {
            Interaction_CurTime -= Time.deltaTime;
        }
    }



}
