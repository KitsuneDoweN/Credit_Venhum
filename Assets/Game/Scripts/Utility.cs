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

    public static void killSequence(ref Sequence sequence)
    {
        if (sequence == null) return;
        sequence.Kill();
        sequence = null;
    } 

    public static void resetColorSequence(ref Sequence sequence, Color resetColor, SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) return;

        killSequence(ref sequence);
        sequence = DOTween.Sequence();
        spriteRenderer.color = resetColor;
    }

}
