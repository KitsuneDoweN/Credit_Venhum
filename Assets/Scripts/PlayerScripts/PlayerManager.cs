using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerAttack m_attack;
    private PlayerMove m_move;
    private Hp m_hp;
    private Rigidbody2D rigid;

    [SerializeField]
    private PlayerAnimation anim;

    [SerializeField]
    private float playerDashStamina = 10;

    [SerializeField]
    private GameObject g_Interaction;
    public GameObject GameOverUI;
    public float playerHp = 5;

    private float Interaction_CurTime;
    private float Interaction_coolTime = 0.5f;

    private float death_coolTime = 1.25f;
    private float death_curTime;
    void Init()
    {
        m_attack = GetComponent<PlayerAttack>();
        m_move = GetComponent<PlayerMove>();
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
        GameOver();
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

    public void GameOver()
    {
        if(playerHp <= 0)
        {
            death_curTime += Time.deltaTime;
            anim.animator.SetBool("Death", true);
            GameOverUI.SetActive(true);
            if(death_curTime >= death_coolTime)
            {
                Time.timeScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                GameOverUI.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }
    }

}
