using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameClear : UI_View
{
    public override void init()
    {
        base.init();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }

    public void goTitle()
    {
        GameManager.instance.goTitle();
    }

}
