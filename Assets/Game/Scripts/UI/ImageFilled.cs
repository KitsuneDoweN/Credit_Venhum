using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFilled : MonoBehaviour
{
    [SerializeField]
    private Image m_fillImage;


    public void draw(float fFillAmount)
    {
        m_fillImage.fillAmount = fFillAmount;
    }
}
