using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float musicVolume = 0.25f;
    [SerializeField] private AudioSource dayMusic;
    [SerializeField] private AudioSource nightMusic;

    public static MusicManager instance;

    public AudioSource DayMusic { get => dayMusic; set => dayMusic = value; }
    public AudioSource NightMusic { get => nightMusic; set => nightMusic = value; }

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

    public void ResetMusic()
    {
        Destroy(instance.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
