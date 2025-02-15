using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] soundEffects;

    [SerializeField] private AudioSource bgm, levelEndMusic;

    public void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //func gets a sound to play, stops current sound playing, changes pitch a bit, plays input sound
    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        soundEffects[soundToPlay].Play();
    }
    //func stops music and plays level end jingle
    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }
}
