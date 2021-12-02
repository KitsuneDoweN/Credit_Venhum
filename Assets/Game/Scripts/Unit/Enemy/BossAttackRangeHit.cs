using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRangeHit : MonoBehaviour
{
    [SerializeField] 
    private UnitBase m_cBoss;

    [SerializeField]
    private GameObject[] m_goHits;

    private delegate void hitEvent();
    private hitEvent [] m_delEvents;

    [SerializeField]
    private Vector2[] m_v2SweepPoints;

    public enum E_Type
    {
        E_NONE = -1,
        E_RAKE,
        E_CHOPPING,
        E_SWEEP,
        E_TOTAL
    }

    public void init()
    {
        m_delEvents = new hitEvent[(int)E_Type.E_TOTAL];

        m_delEvents[(int)E_Type.E_RAKE] = rackEvent;
        m_delEvents[(int)E_Type.E_CHOPPING] = choppingEvent;
        m_delEvents[(int)E_Type.E_SWEEP] = sweepEvent;

        draw(E_Type.E_NONE);
    }

    public void draw(E_Type eType)
    {
        foreach(GameObject go in m_goHits)
        {
            go.SetActive(false);
        }

        if (eType == E_Type.E_NONE)
            return;

        m_goHits[(int)eType].SetActive(true);
        m_delEvents[(int)eType]();
    }

    private void rackEvent()
    {
        m_goHits[(int)E_Type.E_RAKE].transform.position =
            new Vector3(m_cBoss.cGrip.transform.position.x,
            m_goHits[(int)E_Type.E_RAKE].transform.position.y,
            .0f);
    }

    private void choppingEvent()
    {

    }

    private void sweepEvent()
    {

    }

    public void sweepSetting(BossTheCosastofHand.E_Sweep eSweep)
    {
        m_goHits[(int)E_Type.E_SWEEP].transform.localPosition
            = m_v2SweepPoints[(int)eSweep];
    }


}
