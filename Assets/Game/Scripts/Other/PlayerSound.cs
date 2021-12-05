using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
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


    private void Start()
    {
        for (int i = 0; i < m_strAttack.Length; i++)
        {
            m_attack[i] = FMODUnity.RuntimeManager.CreateInstance(m_strAttack[i]);
        }


    }

    public void hitPlayOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strHit, transform.position);
    }

    public void deathPlayOnce()
    {
        foreach(FMOD.Studio.EventInstance attack in m_attack)
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
}
