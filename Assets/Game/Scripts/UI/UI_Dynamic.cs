using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dynamic : UI_View
{
    [SerializeField] UI_InteractionIcon m_cInterIcon;


    public override void init()
    {
        base.init();
        m_cInterIcon.init();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
        if (!bToggle)
            m_cInterIcon.drawOff();
    }

    public void drawInteractionIncon(Vector3 v3GamePos)
    {
        m_cInterIcon.drawOn(v3GamePos);
    }

}
