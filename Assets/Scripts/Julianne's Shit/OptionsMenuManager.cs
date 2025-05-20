using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, vfxVol;
    public AudioMixer mainAudioMixer; 
    public void ChangeGraphicsQuality()
    {
//        QualitySettings.SetQualityLevel(GraphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }
    public void ChangeVfxVolume()
    {
        mainAudioMixer.SetFloat("VfxVol", vfxVol.value);
    }
}
