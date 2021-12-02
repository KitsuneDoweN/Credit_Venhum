using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class TalkData 
{
    [SerializeField]
    private Sprite m_sprite;
    [SerializeField]
    private string m_strName;
    [SerializeField]
    private string m_strTalk;

    public Sprite sprite
    {
        get
        {
            return m_sprite;
        }
        
    }

    public string strTalk
    {
        get
        {
            return m_strTalk;
        }
    }
    public string strName
    {
        get
        {
            return m_strName;
        }
    }
}
