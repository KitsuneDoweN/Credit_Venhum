using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
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

    public void init(PlayerUnit cPlayer)
    {
        setPlayer(cPlayer);
        m_bPushMove = false;
    }

    public void setPlayer(PlayerUnit cPlayer)
    {
        m_cPlayer = cPlayer;
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        
            m_cPlayer.move(context.ReadValue<Vector2>());
        
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;


        if (context.started)
        m_cPlayer.attack();
    }

    public void OnDush(InputAction.CallbackContext context)
    {
        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        if (context.started)
            m_cPlayer.dushAction();
    }


    public void OnSwitchWeapon_1(InputAction.CallbackContext context)
    {
        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        if (context.started)
            m_cPlayer.switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
    }

    public void OnSwitchWeapon_2(InputAction.CallbackContext context)
    {
        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME) return;

        //if (context.started)
        //    m_cPlayer.switchWeapon(PlayerWeapons.E_Weapon.E_KNIF_THROW);
    }


}
