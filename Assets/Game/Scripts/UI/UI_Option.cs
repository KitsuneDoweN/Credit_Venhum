using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : UI_View
{



    //SoundImfect
    private FMOD.Studio.VCA m_vacGamePlayController;
    //BGM
    private FMOD.Studio.VCA m_vacMusicController;
    //
    private FMOD.Studio.VCA m_vacUIController;

    private string m_strMaster;
    private string m_strBGM;
    private string m_strSFX;

    private float m_fMaster_Volume;

    private float m_fBGM_Volume;

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

    private float fBGM_Volume
    {
        set
        {
            m_fBGM_Volume = value;
            m_bgumVoulume.value = m_fBGM_Volume;
        }
        get
        {
            return m_fBGM_Volume;
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
    private Slider m_bgumVoulume;
    [SerializeField]
    private Slider m_sfxVoulume;


    public override void init()
    {
        base.init();
        m_strMaster = "gameplay_sfx";
        m_strBGM = "music";
        m_strSFX = "ui_sfx";

        m_vacGamePlayController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strMaster);
        m_vacMusicController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strBGM);
        m_vacUIController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strSFX);

        fMaster_Volume = 1.0f;
        fBGM_Volume = 1.0f;
        fSFX_Volume = 1.0f;

        setGamePlayVoulume();
        setMusicVoulume();
        setUIVoulume();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);

        if (bToggle)
        {
            GameManager.instance.pause();

            m_masterVoulume.value = fMaster_Volume;
            m_bgumVoulume.value = fBGM_Volume;
            m_sfxVoulume.value = fSFX_Volume;

        }
        else
        {
            GameManager.instance.resume();
        }
    }



    public void setGamePlayVoulume()
    {
        m_vacGamePlayController.setVolume(m_masterVoulume.value);
        m_vacGamePlayController.getVolume(out m_fMaster_Volume);
    }

    public void setMusicVoulume()
    {
        m_vacMusicController.setVolume(m_bgumVoulume.value);
        m_vacMusicController.getVolume(out m_fBGM_Volume);
    }
    public void setUIVoulume()
    {
        m_vacUIController.setVolume(m_sfxVoulume.value);
        m_vacUIController.getVolume(out m_fSFX_Volume);
    }


}
