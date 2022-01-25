using System;
using UnityEngine;

[Serializable]
public class SpawnEntity
{
    [SerializeField]
    private string m_strEntityID;
    [SerializeField]
    private int m_nAmount;

    public string strEntiyID
    {
        get
        {
            return m_strEntityID;
        }
    }

    public int nAmount 
    {
        get
        {
            return m_nAmount;
        }
    }


    public void init(string entityID, int amount)
    {
        m_strEntityID = entityID;
        m_nAmount = amount;
    }

}
