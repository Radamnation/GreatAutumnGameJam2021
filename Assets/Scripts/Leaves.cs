using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField] private float minLeavesTime = 2.0f;
    [SerializeField] private float maxLeavesTime = 5.0f;
    [SerializeField] private float timerOffset = 5.0f;

    private ParticleSystem myParticleSystem;
    private float leavesTimer;
    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        myAudioSource = GetComponent<AudioSource>();

        myParticleSystem.Stop();
        var main = myParticleSystem.main;
        leavesTimer = Random.Range(minLeavesTime, maxLeavesTime);
        main.duration = leavesTimer;
        leavesTimer += timerOffset;
        myAudioSource.volume = 2 * FindObjectOfType<SFXManager>().SFXVolume;
        myParticleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (leavesTimer > 0)
        {
            leavesTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(transform.gameObject);
        }
        myAudioSource.volume = 2 * FindObjectOfType<SFXManager>().SFXVolume;
    }
}
