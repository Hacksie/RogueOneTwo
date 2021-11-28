using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioListener listener;
        [Header("SFX")]
        [SerializeField] private AudioSource killSFX;
        [SerializeField] private AudioSource collideSFX;
        [SerializeField] private AudioSource propSFX;
        [SerializeField] private AudioSource pickupSFX;
        [SerializeField] private AudioSource testSFX;
        [Header("Music")]
        [SerializeField] private AudioSource menuMusic;
        [SerializeField] private AudioSource playMusic;
        [SerializeField] private AudioSource rampageMusic;
        [SerializeField] private AudioSource invulnMusic;
        // [SerializeField] private AudioSource introMusic;
        // [SerializeField] private AudioSource incomingMusic;
        // [SerializeField] private AudioSource attackMusic;
        // [SerializeField] private AudioSource intermissionMusic;
        // [SerializeField] private AudioSource deadMusic;
        // [SerializeField] private AudioSource successMusic;

        public static AudioManager Instance { get; private set; }
        public AudioMixer Mixer { get => mixer; private set => mixer = value; }
        public AudioListener Listener { get => listener; set => listener = value; }

        AudioManager()
        {
            Instance = this;
        }

        public void SetMasterVolume(float value)
        {
            Mixer.SetFloat("Master", value);
        }

        public float GetMasterVolume()
        {
            float result = 0;
            Mixer.GetFloat("Master", out result);
            return result;
        }        

        public void SetSFXVolume(float value)
        {
            Mixer.SetFloat("SFX", value);
            //PlayTest();
        }

        public void SetMusicVolume(float value)
        {
            Mixer.SetFloat("Music", Mathf.Log10(value) * 20);
        }

        public void PlayKillSFX()
        {
            if (killSFX != null && !killSFX.isPlaying)
            {
                killSFX.Play();
            }
        }       

        public void PlayCollideSFX()
        {
            if (collideSFX != null && !collideSFX.isPlaying)
            {
                collideSFX.Play();
            }
        }                

        public void PlayPropSFX()
        {
            if (propSFX != null && !propSFX.isPlaying)
            {
                propSFX.Play();
            }
        }  

        public void PlayPickupSFX()
        {
            if (pickupSFX != null && !pickupSFX.isPlaying)
            {
                pickupSFX.Play();
            }
        }  


        public void PlayTest()
        {
            if (testSFX != null)
            {
                testSFX.Play();
            }
        }                 

        
        public void PlayMenuMusic()
        {
            if (menuMusic != null)
            {
                menuMusic.Play();
            }
        }

        public void StopMenuMusic()
        {
            if (menuMusic != null)
            {
                menuMusic.Stop();
            }
        }

        public void PlayPlayMusic()
        {
            if (playMusic != null)
            {
                playMusic.Play();
            }
        }

        public void StopPlayMusic()
        {
            if (playMusic != null)
            {
                playMusic.Stop();
            }
        }        

        public void PlayRampageMusic()
        {
            if (rampageMusic != null)
            {
                rampageMusic.Play();
            }
        }

        public void StopRampageMusic()
        {
            if (rampageMusic != null)
            {
                rampageMusic.Stop();
            }
        }     

        public void PlayInvulnMusic()
        {
            if (invulnMusic != null)
            {
                invulnMusic.Play();
            }
        }

        public void StopInvulnMusic()
        {
            if (invulnMusic != null)
            {
                invulnMusic.Stop();
            }
        }        

        public void StopAllMusic()
        {
            StopRampageMusic();
            StopInvulnMusic();
            StopMenuMusic();
            StopPlayMusic();
        }             
    }
}