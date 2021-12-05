using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strSound;

    private FMOD.Studio.EventInstance m_sound;

    public void init()
    {
        m_sound = FMODUnity.RuntimeManager.CreateInstance(m_strSound);
    }


    public void playOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strSound, transform.position);
    }

    public void play()
    {
        m_sound.start();
    }
}
