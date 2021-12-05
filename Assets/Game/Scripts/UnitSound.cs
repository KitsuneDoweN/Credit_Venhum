using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strHit;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strDeath;

    [FMODUnity.EventRef]
    [SerializeField]
    private string[] m_strAttack;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strFootStep;

    private FMOD.Studio.EventInstance[] m_attack;

    private FMOD.Studio.EventInstance m_hit;

    private void Start()
    {
        m_attack = new FMOD.Studio.EventInstance[m_strAttack.Length];

        for (int i = 0; i < m_strAttack.Length; i++)
        {
            m_attack[i] = FMODUnity.RuntimeManager.CreateInstance(m_strAttack[i]);
        }

        m_hit = FMODUnity.RuntimeManager.CreateInstance(m_strHit);


    }

    public void hitPlayOnce()
    {
        m_hit.start();
    }

    public void deathPlayOnce()
    {
        foreach (FMOD.Studio.EventInstance attack in m_attack)
        {
            attack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        FMODUnity.RuntimeManager.PlayOneShot(m_strDeath, transform.position);
    }

    public void attackPlayOnce(int i)
    {
        m_attack[i].start();
    }

    public void footStepPlayOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strFootStep, transform.position);
    }

    public void hitPlayOnceIndex(float fData)
    {
        m_hit.setParameterByName("mode", fData);
        m_hit.start();
    }
}
