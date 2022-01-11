using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManger : MonoBehaviour
{
    [SerializeField]
    private CSV m_cPlayerSaveData;

    private string m_strStageID = "Stage";
    private string m_strPlayerPosX = "Pos_x";
    private string m_strPlayerPosY = "Pos_y";
  

    private string m_strPlayerHP = "HP";
    private string m_strStamina = "Stamina";

    public void init()
    {
        m_cPlayerSaveData.init();
    }

    public int getStage()
    {
        return (int)m_cPlayerSaveData.getData( m_strStageID, 0);
    }

    public Vector2 getPlayerPos()
    {
        Vector2 v2Pos = new Vector2(.0f, .0f);
        object xValue = m_cPlayerSaveData.getData(m_strPlayerPosX, 0);
        object yValue = m_cPlayerSaveData.getData(m_strPlayerPosY, 0);



        v2Pos.x = Utility.convertObjectData(xValue);
        v2Pos.y = Utility.convertObjectData(yValue);


        return v2Pos;
    }

    private int getPlayerHp()
    {
        return (int)m_cPlayerSaveData.getData(m_strPlayerHP, 0);
    }
    private float getPlayerStamina()
    {
        object staminaValue = m_cPlayerSaveData.getData(m_strStamina, 0);
        return Utility.convertObjectData(staminaValue);
    }

    public void playerDataLoad(PlayerUnit player)
    {
        player.nHP = getPlayerHp();
        player.fStamina = getPlayerStamina();
        player.v2UnitPos = getPlayerPos();
    }

   

}
