using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource music,sfx;
    public float audioMixerVol {get; set;}

    public static AudioManager Instance {get; private set;}

    private void Awake() {
        if (Instance!=null && Instance!=this){
            Destroy(this);
        }
        else{
            Instance=this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start() {
        PlayMusic(music.clip);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.M)) MuteMusic();
    }

    public void PlayMusic(AudioClip clip){
         music.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip){
         sfx.PlayOneShot(clip);
    }

    private void MuteMusic(){
         music.mute = !music.mute;
    }
}
