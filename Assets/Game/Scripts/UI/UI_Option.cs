using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : UI_View
{



    //SoundImfect
    private FMOD.Studio.VCA m_vacMaster;
    //BGM
    private FMOD.Studio.VCA m_vacMusic;
    //
    private FMOD.Studio.VCA m_vacSFX;

    private string m_strMaster;
    private string m_strMusic;
    private string m_strSFX;

    private float m_fMaster_Volume;

    private float m_fMusic_Volume;

    private float m_fSFX_Volume;

    private float fMaster_Volume
    {
        set
        {
            m_fMaster_Volume = value;
            m_masterVoulume.value = m_fMaster_Volume;
        }
        get
        {
            return m_fMaster_Volume;
        }
    }

    private float fMusic_Volume
    {
        set
        {
            m_fMusic_Volume = value;
            m_musicVoulume.value = m_fMusic_Volume;
        }
        get
        {
            return m_fMusic_Volume;
        }
    }

    private float fSFX_Volume
    {
        set
        {
            m_fSFX_Volume = value;
            m_sfxVoulume.value = m_fSFX_Volume;
        }
        get
        {
            return m_fSFX_Volume;
        }
    }



    [SerializeField]
    private Slider m_masterVoulume;
    [SerializeField]
    private Slider m_musicVoulume;
    [SerializeField]
    private Slider m_sfxVoulume;


    public override void init()
    {
        base.init();
        m_strMaster = "Master";
        m_strMusic = "Music";
        m_strSFX = "SFX";

        m_vacMaster = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strMaster);
        m_vacMusic = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strMusic);
        m_vacSFX = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strSFX);

        fMaster_Volume = 1.0f;
        fMusic_Volume = 1.0f;
        fSFX_Volume = 1.0f;

        setMaster();
        setMusic();
        setSFX();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);

        if (bToggle)
        {
            GameManager.instance.pause();

            m_masterVoulume.value = fMaster_Volume;
            m_musicVoulume.value = fMusic_Volume;
            m_sfxVoulume.value = fSFX_Volume;

        }
        else
        {
            GameManager.instance.resume();
        }
    }



    public void setMaster()
    {
        m_vacMaster.setVolume(m_masterVoulume.value);
        m_vacMaster.getVolume(out m_fMaster_Volume);
    }

    public void setMusic()
    {
        m_vacMusic.setVolume(m_musicVoulume.value);
        m_vacMusic.getVolume(out m_fMusic_Volume);
    }
    public void setSFX()
    {
        m_vacSFX.setVolume(m_sfxVoulume.value);
        m_vacSFX.getVolume(out m_fSFX_Volume);
    }


}
