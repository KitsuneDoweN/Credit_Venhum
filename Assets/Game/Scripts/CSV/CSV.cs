using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class CSV : MonoBehaviour
{
    [SerializeField]
    private string m_strFileName;

    private Dictionary<string, List<object>> m_dataList = new Dictionary<string, List<object>>();

    private List<string> m_dataIDList = new List<string>();

    private static string m_strSplitRe = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static string m_strLineSprlitRe = @"\r\n|\n\r|\n|\r";
    private static char[] TRIM_CHARS = { '\"' };
    private static string m_strDelimiter = ",";

    private string m_strResourcePath;





    public void init()
    {
        m_strResourcePath = Application.dataPath + "/Resources/";

        if(m_strResourcePath == null)
        {
            Debug.LogError("Not Find File Data : " + m_strFileName);
        }


        loadData(m_strFileName);

        //drawAllData(m_dataList);
    }


    public void setData(string strID, int nIndex, object data)
    {
        m_dataList[strID][nIndex] = data;
    }

    public object getData(string strID, int nIndex)
    {
        return m_dataList[strID][nIndex];
    }


    public void saveData()
    {
        StringBuilder strSaveData = new StringBuilder();

        string strDelimiter = ",";

        int nDataListIndex = 0;

        int nIDIndex = 0;

        int nLastStringIndex = 0;


        foreach(string strID in m_dataIDList)
        {
            strSaveData.Append(strID);
            strSaveData.Append(m_strDelimiter);
        }

        nLastStringIndex = strSaveData.Length - 1;
        strSaveData.Remove(nLastStringIndex, 1);
        strSaveData.Append("\n");



        while (nDataListIndex < m_dataList.Count)
        {
            nIDIndex = 0;
            while (nIDIndex < m_dataIDList.Count)
            {
                strSaveData.Append(m_dataList[m_dataIDList[nIDIndex]][nDataListIndex].ToString());
                strSaveData.Append(m_strDelimiter);

                nIDIndex++;
            }

            nLastStringIndex = strSaveData.Length - 1;
            strSaveData.Remove(nLastStringIndex, 1);
            strSaveData.Append("\n");

            nDataListIndex++;
        }

        string strFinalSaveData = CryptoAES256.encrypt(strSaveData.ToString());

        using(StreamWriter sw = new StreamWriter(m_strResourcePath + m_strFileName + ".csv"))
        {
            sw.Write(strFinalSaveData);
            sw.Close();
        }
    }


    private void loadData(string strFileName)
    {
        int nData;
        float fData;

        TextAsset cryptoData = Resources.Load(strFileName) as TextAsset;

        Debug.Log(cryptoData.text);


        // string strSaveData = CryptoAES256.decrypt(cryptoData.text);

        string strSaveData = cryptoData.text;

        var strlines = Regex.Split(strSaveData, m_strLineSprlitRe);
        
        if (strlines.Length <= 1)
        {
            Debug.LogError("NO CSV Data");
            return;
        }

        var strHaders = Regex.Split(strlines[0], m_strSplitRe);

        foreach(string strHader in strHaders)
        {
            if (strHader == "")
                continue;

            m_dataIDList.Add(strHader);
            m_dataList.Add(strHader, new List<object>());
        }

        for (int i = 1; i < strlines.Length; i++)
        {


            var strValues = Regex.Split(strlines[i], m_strSplitRe);

            if (strValues.Length == 0 || strValues[0] == "") continue;

            

            for (int j = 0; j < strHaders.Length && j < strValues.Length; j++)
            {
                string value = strValues[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                object finalValue = value;

                if (int.TryParse(value, out nData))
                {
                    finalValue = nData;
                }
                else if (float.TryParse(value, out fData))
                {
                    finalValue = fData;
                }

                m_dataList[strHaders[j]].Add(finalValue);
            }
            
        }
        //drawAllData();
    }

    private void drawAllData()
    {
        Debug.Log("===========================");
        Debug.Log("Draw All Data");
        foreach (string id in m_dataIDList)
        {
            Debug.Log("----------------------------------");
            Debug.Log("ID " + id  + " Datas");
            for (int i = 0; i < m_dataList[id].Count; i++)
            {
                Debug.Log(m_dataList[id][i]);
            }
        }
        Debug.Log("===========================");

    }


    public int nListCount
    {
        get
        {
            return m_dataList.Count;
        }
    }



}
