using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.1f;
    [SerializeField] private AudioClip myAudioClip;

    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SFXManager>().PlayFlash();
        lifeTimer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
