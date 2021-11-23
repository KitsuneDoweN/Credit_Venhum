using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gameover : UI_View
{
    public override void init()
    {
        base.init();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }

    public void gameContinue()
    {
        GameManager.instance.LoadEvent();
    }

    public void goTiltle()
    {
        GameManager.instance.goTitle();
    }

}
