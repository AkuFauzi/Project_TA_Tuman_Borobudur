using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public float volumeBGM;
    public float volumeSFX;

    public Slider sliderBGM;
    public Slider sliderSFX;

    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;

    string SaveBGM = "SaveBGM";
    string SaveSFX = "SaveSFX";

    string StartSaveVolume = "StartSaveVolume";

    public AudioClip bgmHome, bgmIngame;
    public AudioClip sfxButton;

    public void Awake()
    {
        Instance = this;

        if(PlayerPrefs.GetFloat(StartSaveVolume) == 0)
        {
            PlayerPrefs.SetFloat(StartSaveVolume, 1);

            PlayerPrefs.SetFloat(SaveBGM, 0.6f);
            PlayerPrefs.SetFloat(SaveSFX, 0.6f);

            print("SAVE NIH");
        }

        volumeBGM = PlayerPrefs.GetFloat(SaveBGM);
        volumeSFX = PlayerPrefs.GetFloat(SaveSFX);

        print("audioBGM" + PlayerPrefs.GetFloat(SaveBGM));

        audioSourceBGM.volume = volumeBGM;
        audioSourceSFX.volume = volumeSFX;

        sliderBGM.value = volumeBGM;
        sliderSFX.value = volumeSFX;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void VolumeValueBGM(float value)
    {
        volumeBGM = value;
        audioSourceBGM.volume = value;
        PlayerPrefs.SetFloat(SaveBGM, volumeBGM);
    }
    public void RefrensBGM(Slider slider)
    {
        sliderBGM = slider;
        sliderBGM.value = volumeBGM;
    }
    public void VolumeValueSFX(float value)
    {
        volumeSFX = value;
        audioSourceSFX.volume = value;
        PlayerPrefs.SetFloat(SaveSFX, volumeSFX);
    }
    public void RefrensSFX(Slider slider)
    {
        sliderSFX = slider;
        sliderSFX.value = volumeSFX;
    }

}
