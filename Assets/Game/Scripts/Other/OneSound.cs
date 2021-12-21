using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSound : MonoBehaviour
{


    //[FMODUnity.EventRef]
    [SerializeField]
    private FMODUnity.EventReference m_soundReference;


    private FMOD.Studio.EventInstance m_sound;

    public void init()
    {
        m_sound = FMODUnity.RuntimeManager.CreateInstance(m_soundReference);
    }


    public void playOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_soundReference, transform.position);
    }

    public void play()
    {
        m_sound.start();
    }

    public void stop()
    {
        m_sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
