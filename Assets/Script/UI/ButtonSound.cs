using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioClick;
    public AudioClip audioHover;

    public void HoverSound()
    {
        audio.PlayOneShot(audioHover);
    }

    public void ClickSound()
    {
        audio.PlayOneShot(audioClick);
    }
}
