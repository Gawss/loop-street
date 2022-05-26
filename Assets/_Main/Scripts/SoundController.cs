using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopStreet.Game.Main
{
    public class SoundController : MonoBehaviour
    {
        public List<AudioClip> audioClips;
        public AudioSource audioSource;

        public void PlaySoulChange()
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }
}