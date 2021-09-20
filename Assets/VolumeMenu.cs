using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        musicSlider.value = FindObjectOfType<MusicManager>().MusicVolume;
        SFXSlider.value = FindObjectOfType<SFXManager>().SFXVolume;
        musicSlider.gameObject.SetActive(true);
        SFXSlider.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<MusicManager>().MusicVolume = musicSlider.value;
        FindObjectOfType<MusicManager>().UpdateMusicVolume();
        FindObjectOfType<SFXManager>().SFXVolume = SFXSlider.value;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SFXManager>().PlayClick();
        musicSlider.gameObject.SetActive(false);
        SFXSlider.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
