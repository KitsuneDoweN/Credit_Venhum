using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string m_strSound;

    public void playOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_strSound, transform.position);
    }
}
