using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource aS;

    private void Start()
    {        
        aS = gameObject.GetComponent<AudioSource>();
        aS.clip = clip;
    }
    public void play()
    {
        if (aS != null)
        {
            aS.Play();
        }
    }
}
