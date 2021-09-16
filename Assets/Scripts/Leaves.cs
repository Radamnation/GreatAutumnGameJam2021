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

    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        
        myParticleSystem.Stop();
        var main = myParticleSystem.main;
        leavesTimer = Random.Range(minLeavesTime, maxLeavesTime);
        main.duration = leavesTimer;
        leavesTimer += timerOffset;
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
    }
}
