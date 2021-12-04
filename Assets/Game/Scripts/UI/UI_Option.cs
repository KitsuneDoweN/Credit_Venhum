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
        m_vacUIController = FMODUnity.RuntimeManager.GetVCA("vca/" + m_strUI_sfx);

        m_fVacGamePlayVolume = 0.0f;
        m_fVacMusicVolume = 0.0f;
        m_fVacUIVolume = 0.0f;

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

            m_masterVoulume.value = m_fVacGamePlayVolume;
            m_bgumVoulume.value = m_fVacMusicVolume;
            m_sfxVoulume.value = m_fVacUIVolume;

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
        m_vacMusicController.getVolume(out m_fVacGamePlayVolume);
    }
    public void setUIVoulume()
    {
        m_vacUIController.setVolume(m_sfxVoulume.value);
        m_vacUIController.getVolume(out m_fVacGamePlayVolume);
    }


}
