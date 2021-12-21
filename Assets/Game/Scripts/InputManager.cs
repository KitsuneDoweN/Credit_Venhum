using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IUpdate
{
    private PlayerUnit m_cPlayer = null;

    private bool m_bPushMove;

    

    public bool isPushMove
    {
        get
        {
            return m_bPushMove;
        }
    }

    private bool m_bPushAttack;

    public bool isPushAttack
    {
        set
        {
            m_bPushAttack = value;
            //Debug.Log("Push: " + isPushAttack);
        }
        get
        {
            return m_bPushAttack;
        }
    }

   

    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    public void init()
    {
        m_cPlayer = null;
        m_bPushMove = false;
        UpdateManager.Instance.addProcesses(this);
    }

    public void setPlayer(PlayerUnit cPlayer)
    {
        m_cPlayer = cPlayer;
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        if (!(GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME ||
            GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_CLEAR))
            return;


        m_cPlayer.v2NextLookDir = context.ReadValue<Vector2>();
        m_cPlayer.v2NextMoveDir = context.ReadValue<Vector2>();

    }



    public void OnNomalAttack(InputAction.CallbackContext context)
    {
        if (!(GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME ||
            GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_CLEAR))
            return;



        if (context.canceled)
        {
            isPushAttack = false;
        }

        if (!m_cPlayer.isControl)
            return;

        if (context.started)
        {
            if (m_cPlayer.eGripWeapon != PlayerWeapons.E_Weapon.E_SWORD)
                isPushAttack = m_cPlayer.switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
            else
                isPushAttack = true;

            m_cPlayer.attack();

  
        }

        
    }

    public void OnDush(InputAction.CallbackContext context)
    {
        if (!(GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME ||
            GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_CLEAR))
            return;
        if (!m_cPlayer.isControl)
            return;


        if (context.started)
            m_cPlayer.dashAction();
    }


    public void OnThrowAttack(InputAction.CallbackContext context)
    {
        if (!(GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME ||
            GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_CLEAR))
            return;

        if (context.canceled)
        {
            isPushAttack = false;
        }

        if (!m_cPlayer.isControl)
            return;


        if (context.started)
        {
            if (m_cPlayer.eGripWeapon != PlayerWeapons.E_Weapon.E_KNIF_THROW)
                isPushAttack = m_cPlayer.switchWeapon(PlayerWeapons.E_Weapon.E_KNIF_THROW);
            else
                isPushAttack = true;

            m_cPlayer.attack();

            
        }

    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        if (context.started)
        {
            if (GameManager.Instance.cUIManager.cUI_InGame.cUI_InteractionText.getToggle())
            {
                GameManager.Instance.cUIManager.cUI_InGame.cUI_InteractionText.toggle(false);
            }

            if (GameManager.Instance.cInteraction != null)
            GameManager.Instance.interActionEvent();



        }
    }

    public void OnOption(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        if (context.started)
        {
            GameManager.Instance.cUIManager.cUI_Option.toggle(!GameManager.Instance.cUIManager.cUI_Option.isToggle);
        }
    }



    public void updateProcesses()
    {
        if (m_cPlayer == null || !(GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME ||
    GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_CLEAR))
            return;

        if (m_cPlayer.isControl && isPushAttack)
        {
            m_cPlayer.attack();
        }
    }
}
