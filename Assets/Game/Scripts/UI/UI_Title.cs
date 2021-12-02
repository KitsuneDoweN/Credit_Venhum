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
        GameManager.instance.goIngame();
    }

    public void option()
    {
        GameManager.instance.cUIManager.cUI_Option.toggle(true);
    }


    public void exit()
    {
        Application.Quit();
    }

}
