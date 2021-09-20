using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private float sfxVolume = 0.25f;
    [SerializeField] private AudioClip[] scratchSFX;
    [SerializeField] private AudioClip[] markerSFX;
    [SerializeField] private AudioClip[] eraserSFX;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip catMeowSFX;
    [SerializeField] private AudioClip chirpSFX;
    [SerializeField] private AudioClip flashSFX;
    [SerializeField] private AudioClip birdLandSFX;
    [SerializeField] private AudioClip birdTakeoffSFX;
    [SerializeField] private AudioClip birdPeckSFX;
    [SerializeField] private AudioClip catFlickSFX;
    [SerializeField] private AudioClip catStepSFX;

    [SerializeField] private AudioSource environnementAudioSource;
    [SerializeField] private AudioSource playerAudioSource;

    public static SFXManager instance;

    public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScratch()
    {
        playerAudioSource.volume = sfxVolume;
        playerAudioSource.clip = (scratchSFX[Random.Range(0, scratchSFX.Length)]);
        playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
        playerAudioSource.Play();
    }

    public void PlayMarker()
    {
        playerAudioSource.volume = sfxVolume;
        playerAudioSource.clip = (markerSFX[Random.Range(0, markerSFX.Length)]);
        playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
        playerAudioSource.Play();
    }

    public void PlayEraser()
    {
        playerAudioSource.volume = sfxVolume * 0.5f;
        playerAudioSource.clip = (eraserSFX[Random.Range(0, eraserSFX.Length)]);
        playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
        playerAudioSource.Play();

    }

    public void PlayClick()
    {
        playerAudioSource.volume = sfxVolume;
        playerAudioSource.clip = clickSFX;
        playerAudioSource.pitch = 1;
        playerAudioSource.Play();
    }

    public void PlayMeow()
    {
        playerAudioSource.volume = sfxVolume * 0.25f;
        playerAudioSource.clip = catMeowSFX;
        playerAudioSource.pitch = 1;
        playerAudioSource.Play();
    }

    public void PlayChirp()
    {
        playerAudioSource.volume = sfxVolume * 0.75f;
        playerAudioSource.clip = chirpSFX;
        playerAudioSource.pitch = 1;
        playerAudioSource.Play();
    }

    public void PlayFlash()
    {
        playerAudioSource.volume = sfxVolume;
        playerAudioSource.clip = flashSFX;
        playerAudioSource.pitch = 1;
        playerAudioSource.Play();
    }

    public void PlayBirdLand()
    {
        environnementAudioSource.volume = sfxVolume;
        environnementAudioSource.clip = birdLandSFX;
        environnementAudioSource.pitch = 1;
        environnementAudioSource.Play();
    }

    public void PlayBirdTakeoff()
    {
        environnementAudioSource.volume = sfxVolume;
        environnementAudioSource.clip = birdTakeoffSFX;
        environnementAudioSource.pitch = 1;
        environnementAudioSource.Play();
    }

    public void PlayBirdPeck()
    {
        environnementAudioSource.volume = sfxVolume;
        environnementAudioSource.clip = birdPeckSFX;
        environnementAudioSource.pitch = Random.Range(0.9f, 1.1f);
        environnementAudioSource.Play();
    }

    public void PlayCatFlick()
    {
        environnementAudioSource.volume = sfxVolume;
        environnementAudioSource.clip = catFlickSFX;
        environnementAudioSource.pitch = Random.Range(0.9f, 1.1f);
        environnementAudioSource.Play();
    }

    public void PlayCatWalk()
    {
        environnementAudioSource.volume = sfxVolume * 0.25f;
        environnementAudioSource.clip = catStepSFX;
        environnementAudioSource.pitch = 1;
        environnementAudioSource.Play();
    }
}
