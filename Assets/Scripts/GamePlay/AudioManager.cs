using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public ThirdPersonController thirdPersonController;

    public float volumeBGM;
    public float volumeSFX;

    public AudioSource[] BGMEnvi;

    public Slider sliderBGM;
    public Slider sliderSFX;

    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;

    string SaveBGM = "SaveBGM";
    string SaveSFX = "SaveSFX";

    string StartSaveVolume = "StartSaveVolume";

    public AudioClip bgmHome, bgmIngame, bgmBoss;
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

        audioSourceBGM.PlayOneShot(bgmHome);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSourceBGM.PlayOneShot(bgmIngame);
    }

    private void Update()
    {
        for(int i = 0; i < BGMEnvi.Length; i++)
        {
            BGMEnvi[i].volume = audioSourceBGM.volume;
        }
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
