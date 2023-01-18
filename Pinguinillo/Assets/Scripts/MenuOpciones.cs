using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject fill;

    private void Start() {
        //CambiarTamañoFill(AudioManager.Instance.audioMixerVol + 0.5f);
    }

    public void CambiarVolumen(float volumen){
        //volumen *= 10;
        audioMixer.SetFloat("volumen",volumen);
        AudioManager.Instance.audioMixerVol = volumen;
        //CambiarTamañoFill(Mathf.Abs(volumen /200));
        //tamaño = fill.transform.right;
    }

    public void CambiarTamañoFill(float volumen){
        fill.transform.localScale = new Vector3(volumen,0.15f,this.transform.localScale.z);
    }
}
