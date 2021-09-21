using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float shadowTimerMin = 0.25f;
    [SerializeField] private float shadowTimerMax = 0.5f;
    [SerializeField] private AnimationClip myAnimationClip;

    private float shadowTimer;

    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        shadowTimer = Random.Range(shadowTimerMin, shadowTimerMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowTimer > 0)
        {
            shadowTimer -= Time.deltaTime;
        }
        else
        {
            shadowTimer = Random.Range(shadowTimerMin, shadowTimerMax);
            myAnimator.speed = 1 / shadowTimer;
        }
    }
}
