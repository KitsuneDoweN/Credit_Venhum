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


    public void gameStart(string stageName)
    {
        GameManager.instance.gameStart(stageName);
    }

    public void exit()
    {
        Application.Quit();
    }

}
