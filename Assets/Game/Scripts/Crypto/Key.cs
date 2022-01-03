using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Key 
{
    private static byte[] m_SECRET_KEY = { 0X0,0X1, 0X2, 0X3, 0X4, 0X5, 0X6, 0X7, 0X8, 0X9, 0XA, 0XB, 0XC, 0XD, 0XE, 0XF, 0X10, 0X11, 0X12, 0X13, 0X14, 0X15, 0X16, 0X17, 0X18, 0X19, 0X1A, 0X1B, 0X1C, 0X1D, 0X1E, 0X1F };
    private static byte[] m_KEY_IV = { 0X0, 0X1, 0X2, 0X3, 0X4, 0X5, 0X6, 0X7, 0X8, 0X9, 0XA, 0XB, 0XC, 0XD, 0XE, 0XF };

    public static byte[] SECRET_KEY
    {
        get
        {
            return m_SECRET_KEY;
        }
    }

    public static byte[] KEY_IV
    {
        get
        {
            return m_KEY_IV;
        }
    }
}
