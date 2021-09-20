using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRig : MonoBehaviour
{
    [SerializeField] private Transform cat;
    [SerializeField] private Transform arrivalPosition;
    [SerializeField] private Transform stopPosition;
    [SerializeField] private Transform leavingPosition;
    [SerializeField] private float walkTime = 2.0f;
    [SerializeField] private float flickTimerMin = 0.25f;
    [SerializeField] private float flickTimerMax = 2.0f;
    [SerializeField] private float minimumTime = 5.0f;

    private bool walking = true;
    private bool stopped = false;
    private float walkTimer;
    private float flickTimer;
    private float minimumTimer;
    private Collider2D myCollider2D;

    private Animator catAnimator;

    // Start is called before the first frame update
    void Start()
    {
        walkTimer = walkTime;
        minimumTimer = minimumTime;
        catAnimator = GetComponentInChildren<Animator>();
        catAnimator.SetBool("CatWalk", true);
        flickTimer = Random.Range(flickTimerMin, flickTimerMax);
        RandomCatDirection();
        RandomCatPosition();
        myCollider2D = GetComponentInChildren<Collider2D>();
        myCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Walking();
        Catting();
        Leaving();
    }

    private void RandomCatDirection()
    {
        var randomDirection = -1 + 2 * Random.Range(0, 2);
        transform.localScale = new Vector3(randomDirection, 1, 1);
    }

    private void RandomCatPosition()
    {
        var randomDirection = Random.Range(-16, 16);
        transform.position = new Vector3(randomDirection, transform.position.y, transform.position.z);
    }

    private void Walking()
    {
        if (!walking) return;

        if (walkTimer > 0)
        {
            walkTimer -= Time.deltaTime;
            var timeRatio = (walkTime - walkTimer) / walkTime;
            cat.position = Vector3.Lerp(arrivalPosition.position, stopPosition.position, timeRatio);
        }
        else
        {
            walking = false;
            stopped = true;
            catAnimator.SetBool("CatWalk", false);
            myCollider2D.enabled = true;
        }
    }

    private void Catting()
    {
        if (!stopped) return;

        if (flickTimer > 0)
        {
            minimumTimer -= Time.deltaTime;
            flickTimer -= Time.deltaTime;
        }
        else
        {
            ResetFlickTimer();
        }
    }

    public void ResetFlickTimer()
    {
        flickTimer = Random.Range(flickTimerMin, flickTimerMax);
        var catAction = 0;
        if (minimumTimer > 0)
        {
            catAction = Random.Range(0, 8);
        }
        else
        {
            catAction = Random.Range(0, 10);
        }

        switch (catAction)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                catAnimator.SetTrigger("CatBlink");
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                catAnimator.SetTrigger("CatFlick");
                FindObjectOfType<SFXManager>().PlayCatFlick();
                break;
            case 8:
            case 9:
                stopped = false;
                walkTimer = walkTime;
                catAnimator.SetBool("CatWalk", true);
                RandomCatDirection();
                myCollider2D.enabled = false;
                break;
        }
    }

    private void Leaving()
    {
        if (walking || stopped) return;

        if (walkTimer > 0)
        {
            walkTimer -= Time.deltaTime;
            var timeRatio = (walkTime - walkTimer) / walkTime;
            cat.position = Vector3.Lerp(stopPosition.position, leavingPosition.position, timeRatio);
        }
        else
        {
            catAnimator.SetBool("CatWalk", false);
            Destroy(transform.gameObject);
        }
    }

    public void FlipPosition()
    {
        if (transform.localScale.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
