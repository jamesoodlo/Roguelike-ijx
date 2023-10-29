using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFx : MonoBehaviour
{
    public float volumeSfx;
    public AudioSource hurtSfx;
    public AudioSource slashSfx;
    public AudioSource EatApple;
    public AudioSource footStepSfx;
    public AudioSource pickUpSfx;
    public AudioSource blockingSfx;
    public AudioSource rollingSfx;
    public AudioSource buffSfx;
    public AudioSource slashbuffSfx;
    public AudioSource woodBoxSfx;

    void Update()
    {
        if(hurtSfx != null) hurtSfx.volume = volumeSfx;
        if(slashSfx != null) slashSfx.volume = volumeSfx;
        if(EatApple != null) EatApple.volume = volumeSfx;
        if(footStepSfx != null) footStepSfx.volume = volumeSfx;
        if(pickUpSfx != null) pickUpSfx.volume = volumeSfx;
        if(blockingSfx != null) blockingSfx.volume = volumeSfx;
        if(rollingSfx != null) rollingSfx.volume = volumeSfx;
        if(buffSfx != null) buffSfx.volume = volumeSfx;
        if(slashbuffSfx != null) slashbuffSfx.volume = volumeSfx;
        if(woodBoxSfx != null) woodBoxSfx.volume = volumeSfx;
    }
}
