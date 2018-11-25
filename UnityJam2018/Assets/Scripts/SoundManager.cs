using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip musicInGame;
    public AudioClip musicMenu;
    public AudioClip musicEndGame;

    AudioSource currentAudioSource;

    float volumeScaleDecrease = 5f;
    float volumeScaleIncrease = 2f;

    public static SoundManager instance;
	// Use this for initialization
	void Start () {

        if (!instance)
            instance = this;

        currentAudioSource = GetComponent<AudioSource>();
        LaunchMenuMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LaunchInGameMusic()
    {
        currentAudioSource.Stop();
        currentAudioSource.clip = musicInGame;
        currentAudioSource.Play();
    }

    public void LaunchMenuMusic()
    {
        currentAudioSource.Stop();
        currentAudioSource.clip = musicMenu;
        currentAudioSource.Play();
    }

    public void LaunchEndGameMusic()
    {
        currentAudioSource.Stop();
        currentAudioSource.clip = musicEndGame;
        currentAudioSource.Play();
    }

    public void VolumeDown()
    {
        if (currentAudioSource.volume > 0.1)
            currentAudioSource.volume -= Time.deltaTime * volumeScaleDecrease;
        else
            currentAudioSource.volume = 0.1f;
    }

    public void VolumeUp()
    {
        if (currentAudioSource.volume < 1)
            currentAudioSource.volume += Time.deltaTime * volumeScaleIncrease;
        else
            currentAudioSource.volume = 1;
    }
}
