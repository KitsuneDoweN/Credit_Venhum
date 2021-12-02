using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAnimationHandle : MonoBehaviour
{
    public void gameover()
    {
        GameManager.instance.eGameState = GameManager.E_GAMESTATE.E_OVER;
    }
}
