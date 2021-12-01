using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Option : UI_View
{
    public override void init()
    {
        base.init();

    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);

        if (bToggle)
        {
            GameManager.instance.pause();
        }
        else
        {
            GameManager.instance.resume();
        }
    }

 
}
