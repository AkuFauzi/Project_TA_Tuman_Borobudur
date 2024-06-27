using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void setlevel(float slidervalue)
    {
        mixer.SetFloat("MyExposedParam", Mathf.Log10(slidervalue)*20);
    }

}
