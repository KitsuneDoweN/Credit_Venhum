using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class CSV : MonoBehaviour
{
    private List<Dictionary<string, object>> m_dataList = new List<Dictionary<string, object>>();
    private List<string> m_dataIDList = new List<string>();

    private static string m_strSplitRe = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static string m_strLineSprlitRe = @"\r\n|\n\r|\n|\r";
    private static char[] TRIM_CHARS = { '\"' };
    private static string m_strDelimiter = ",";

    private string m_strResourcePath;

    private string m_strFileName;


    public void init(string strFileName)
    {
        m_strResourcePath = Application.dataPath + "/Resources/";

        m_strFileName = strFileName;

        loadData(m_strFileName);

        drawAllData(m_dataList);


        setData(0, m_dataIDList[0], (object)123);

        saveData();
    }


    public void setData(int nIndex, string strID, object data)
    {
        m_dataList[nIndex][strID] = data;
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
                strSaveData.Append(m_dataList[nDataListIndex][m_dataIDList[nIDIndex]].ToString());
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


        string strSaveData = CryptoAES256.decrypt(cryptoData.text);
        


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
        }

        for (int i = 1; i < strlines.Length; i++)
        {


            var strValues = Regex.Split(strlines[i], m_strSplitRe);

            if (strValues.Length == 0 || strValues[0] == "") continue;

            Dictionary<string, object> entry = new Dictionary<string, object>();

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
                entry[strHaders[j]] = finalValue;
            }
            m_dataList.Add(entry);
        }

    }

    private void drawAllData(List<Dictionary<string, object>> dataList)
    {
        foreach(Dictionary<string, object> data in dataList)
        {
            Debug.Log("ID : " + data["ID"] + "   Text : " + data["Text"]);
        }
    }




}
