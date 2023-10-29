using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private EnemyMelee[] enemyMelee;
    public int enemyCount;
    public SettingData settingData;

    public SoundFx[] soundFx;

    public AudioClip musicClip, battleClip;

    public AudioSource ambientSound, musicSound, effectSound;

    void Start()
    {
        musicSound.clip = musicClip;
        musicSound.Play();
    }

    void Update()
    {
        enemyMelee = FindObjectsOfType<EnemyMelee>();
        soundFx = FindObjectsOfType<SoundFx>();

        enemyCount = enemyMelee.Length;

        if (enemyCount > 0)
        {
            if (musicSound.clip != battleClip)
            {
                musicSound.clip = battleClip;
                musicSound.Play();
            }
        }
        else
        {
            if (musicSound.clip != musicClip)
            {
                musicSound.clip = musicClip;
                musicSound.Play();
            }
        }

        for (int i = 0; i < soundFx.Length; i++)
        {
            if(soundFx != null)
                soundFx[i].volumeSfx = settingData.effectSound;
        }
    }
}
