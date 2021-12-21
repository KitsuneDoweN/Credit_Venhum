using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : UI_View
{
    public override void init()
    {
        base.init();
        toggle(false);
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }


    public void gameStart()
    {
        GameManager.Instance.goIngame();
    }

    public void option()
    {
        GameManager.Instance.cUIManager.cUI_Option.toggle(true);
    }


    public void exit()
    {
        Application.Quit();
    }

}
