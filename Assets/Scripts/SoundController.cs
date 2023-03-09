using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    public void ButtonSound()
    {
        if (buttonSound.isPlaying) buttonSound.Pause();

        buttonSound.Play();
    }
}
