using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Utility
{
    public static bool getClipLenth(Animator animator, string clipName, out float fClipLenth)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        int i = 0;
        bool isSerch = false;
        fClipLenth = 0.0f;

        while (clips.Length > i && !isSerch)
        {
            

            if(split(clips[i].name,'_')[1] == clipName)
            {
                fClipLenth = clips[i].length;
                isSerch = true;
            }
            else
            {
                i++;
            }
        }

        return isSerch;


    }


    public static string[] split(string str, char cSplit)
    {
        return  str.Split(cSplit);
    }

    public static void KillTween(Tween tween)
    {
        if (tween == null) return;
        tween.Kill();
        tween = null;
    } 



    public static float getHorizontalAtBetweenAngle(Vector2 v2Dir)
    {
        float fResult;
        float fDot = Vector2.Dot(v2Dir, Vector2.right);
        int nReverse = 1;
        if (v2Dir.y < 0)
            nReverse *= -1;

        fResult = Mathf.Acos(fDot) * Mathf.Rad2Deg * nReverse;

        return fResult;
    }

    public static float convertObjectData(object value)
    {
        return float.Parse(value.ToString());
    }
}
