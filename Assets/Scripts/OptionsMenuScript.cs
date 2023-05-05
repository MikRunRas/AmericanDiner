using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI volumePercent;


    void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    public void OnSliderValueChanged()
    {
        AudioListener.volume = slider.value;
        volumePercent.text = ((int)(slider.value * 100)).ToString() + "%";
        Save();
    }
}
