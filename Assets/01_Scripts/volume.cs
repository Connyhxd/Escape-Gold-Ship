using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sfx;
    public Slider bgm;

    private void Update()
    {
        mixer.SetFloat("BGMVolume", bgm.value);
        mixer.SetFloat("SFXVolume", sfx.value);
    }
}
