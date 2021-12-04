using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySound : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strHit;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strDeath;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strAttack;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strFootStep;

    private FMOD.Studio.EventInstance m_attack;


    private void Start()
    {
        m_attack = FMODUnity.RuntimeManager.CreateInstance(m_strAttack);
    }

    public void hitPlayOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strHit, transform.position);
    }

    public void deathPlayOnce()
    {
        m_attack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FMODUnity.RuntimeManager.PlayOneShot(m_strDeath, transform.position);
    }

    public void attackPlayOnce()
    {
        m_attack.start();
    }

    public void footStepPlayOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strFootStep, transform.position);
    }


}
