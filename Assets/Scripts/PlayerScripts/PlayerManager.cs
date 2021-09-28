using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private CharAttack m_attack;
    private CharMove m_move;
    private Rigidbody2D rigid;

    [SerializeField] 
    private PlayerAnimation m_playerAnimation;

    [SerializeField]
    private float playerDashStamina = 10;

    [SerializeField]
    private GameObject g_Interaction;

    public float playerHp = 100.0f;

    private float Interaction_CurTime;
    private float Interaction_coolTime = 0.5f;
    void Init()
    {
        m_attack = GetComponent<CharAttack>();
        m_move = GetComponent<CharMove>();
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
        m_move.Move();
        m_attack.Attack();
        m_move.Dash(playerDashStamina);
        Interaction();
    }
    public void TakeDamage(float damage)
    {
        playerHp = playerHp - damage;
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
