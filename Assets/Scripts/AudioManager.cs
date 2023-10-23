using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip coinCollect;
    public AudioClip jumpSound;
    public AudioClip powerUp;
    public AudioClip powerDown;
    public AudioClip checkPoint;
    public AudioClip gameOver;
    public AudioClip levelPass;
    public AudioClip hitSound;
    public AudioClip lifeSound;
    public AudioClip musicSound;
    //function for playing button Audio
    public void ButtonAudio()
    {
        audioSource.PlayOneShot(buttonSound, 1f);
    }
    //function for playing HitSound
    public void HitSound()
    {
        audioSource.PlayOneShot(hitSound, 1f);
    }
}
