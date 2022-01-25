using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManger : MonoBehaviour
{
    [SerializeField]
    private CSV m_cPlayerSaveData;
    [SerializeField]
    private CSV m_cStageEventData;

    private string m_strSenceID = "SenceID";
    private string m_strStage = "Stage";
    private string m_strPlayerPosX = "Pos_x";
    private string m_strPlayerPosY = "Pos_y";
  

    private string m_strPlayerHP = "HP";
    private string m_strPlayerStamina = "Stamina";

    private string m_strStageEventIndex = "EventIndex";
    private string m_strStageEventClear = "Clear";
    private string m_strStageEventType = "EventType";

    private IEnumerator m_saveProcessesCoroutine;

    public void init()
    {
        m_cPlayerSaveData.init();
        m_cStageEventData.init();
    }



    public Vector2 getPlayerPos()
    {
        Vector2 v2Pos = new Vector2(.0f, .0f);
        object xValue = m_cPlayerSaveData.getData( 0, m_strPlayerPosX);
        object yValue = m_cPlayerSaveData.getData( 0, m_strPlayerPosY);



        v2Pos.x = Utility.convertObjectToFloat(xValue);
        v2Pos.y = Utility.convertObjectToFloat(yValue);


        return v2Pos;
    }

    private int getPlayerHp()
    {
        return (int)m_cPlayerSaveData.getData( 0, m_strPlayerHP);
    }
    private float getPlayerStamina()
    {
        object staminaValue = m_cPlayerSaveData.getData( 0, m_strPlayerStamina);
        return Utility.convertObjectToFloat(staminaValue);
    }

    public void playerDataLoad(PlayerUnit player)
    {
        player.nHP = getPlayerHp();
        player.fStamina = getPlayerStamina();
        player.v2UnitPos = getPlayerPos();



    }

   private void playerDataSave(PlayerUnit player)
    {
        m_cPlayerSaveData.setData( 0, m_strSenceID, (object)(int)GameManager.Instance.eScene);
        m_cPlayerSaveData.setData(0, m_strStage, (object)GameManager.Instance.nStage);
        m_cPlayerSaveData.setData( 0, m_strPlayerPosX, (object)player.v2UnitPos.x);
        m_cPlayerSaveData.setData( 0, m_strPlayerPosY, (object)player.v2UnitPos.y);
        m_cPlayerSaveData.setData( 0, m_strPlayerHP, (object)player.nHP);
        m_cPlayerSaveData.setData( 0, m_strPlayerStamina, (object)player.fStamina);

        m_cPlayerSaveData.saveData();
    }

    public void stageEventDataSave(int nEventIndex, int nClear)
    {
        m_cStageEventData.setData(nEventIndex, m_strStageEventClear, (object)nClear);
        m_cStageEventData.saveData();
    }

    public bool isEventClear(int nEventIndex)
    {
        int nData = int.Parse(m_cStageEventData.getData(nEventIndex, m_strStageEventClear).ToString());

        return nData == 1;

    }

    public int getEventType(int nEventIndex)
    {
        int nData = int.Parse(m_cStageEventData.getData(nEventIndex, m_strStageEventType).ToString());

        return nData;
    }


    public void save()
    {
        if (m_saveProcessesCoroutine != null)
            StopCoroutine(m_saveProcessesCoroutine);

        m_saveProcessesCoroutine = saveProcess();

        StartCoroutine(m_saveProcessesCoroutine);
    }

    private IEnumerator saveProcess()
    {

        GameManager.Instance.cUIManager.cUI_Save.toggle(true);

        playerDataSave(GameManager.Instance.cStageManager.cPlayer);
       

        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.cUIManager.cUI_Save.toggle(false);

    }


    public GameManager.E_GAMESCENE getSenceID()
    {
        return (GameManager.E_GAMESCENE)int.Parse(m_cPlayerSaveData.getData(0, m_strSenceID).ToString());
    }

    public int getStage()
    {
        return int.Parse(m_cPlayerSaveData.getData(0, m_strStage).ToString());
    }

}
