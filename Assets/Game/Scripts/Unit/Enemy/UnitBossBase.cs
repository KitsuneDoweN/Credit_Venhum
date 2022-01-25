using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBossBase : UnitBase
{
    [SerializeField]
    protected Sprite m_bossIcon;

    protected UnitBase m_cPlayer;

    [SerializeField]
    protected ProductionBase m_cdeathProducion;

    

    public virtual void handleWakeUp(UnitBase cPlayer)
    {
        m_cPlayer = cPlayer;
        isControl = true;
        isLookAble = true;
        isMoveAble = false;


        GameManager.Instance.cUIManager.cUI_InGame.cUI_BossHp.toggle(true);
        GameManager.Instance.cUIManager.cUI_InGame.cUI_BossHp.draw(m_bossIcon, nHP, m_cStatus.nMaxHp, m_cStatus.strName);
    }


}
