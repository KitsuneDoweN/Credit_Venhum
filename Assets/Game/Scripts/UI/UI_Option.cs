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

    private string m_strGamePlay_sfx;
    private string m_strMusic;
    private string m_strUI_sfx;

    private float m_fVacGamePlayVolume;

    private float m_fVacMusicVolume;

    private float m_fVacUIVolume;

    private float fVacGamePlayVolume
    {
        set
        {
            m_fVacGamePlayVolume = value;
            m_masterVoulume.value = m_fVacGamePlayVolume;
        }
        get
        {
            return m_fVacGamePlayVolume;
        }
    }

    private float fVacMusicVolume
    {
        set
        {
            m_fVacMusicVolume = value;
            m_bgumVoulume.value = m_fVacMusicVolume;
        }
        get
        {
            return m_fVacMusicVolume;
        }
    }

    private float fVacUIVolume
    {
        set
        {
            m_fVacUIVolume = value;
            m_sfxVoulume.value = m_fVacUIVolume;
        }
        get
        {
            return m_fVacUIVolume;
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
        m_strGamePlay_sfx = "gameplay_sfx";
        m_strMusic = "music";
        m_strUI_sfx = "ui_sfx";

        m_vacGamePlayController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strGamePlay_sfx);
        m_vacMusicController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strMusic);
        m_vacUIController = FMODUnity.RuntimeManager.GetVCA("vca:/" + m_strUI_sfx);

        fVacGamePlayVolume = 1.0f;
        fVacMusicVolume = 1.0f;
        fVacUIVolume = 1.0f;

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

            m_masterVoulume.value = fVacGamePlayVolume;
            m_bgumVoulume.value = fVacMusicVolume;
            m_sfxVoulume.value = fVacUIVolume;

        }
        else
        {
            GameManager.instance.resume();
        }
    }



    public void setGamePlayVoulume()
    {
        m_vacGamePlayController.setVolume(m_masterVoulume.value);
        m_vacGamePlayController.getVolume(out m_fVacGamePlayVolume);
    }

    public void setMusicVoulume()
    {
        m_vacMusicController.setVolume(m_bgumVoulume.value);
        m_vacMusicController.getVolume(out m_fVacMusicVolume);
    }
    public void setUIVoulume()
    {
        m_vacUIController.setVolume(m_sfxVoulume.value);
        m_vacUIController.getVolume(out m_fVacUIVolume);
    }


}
