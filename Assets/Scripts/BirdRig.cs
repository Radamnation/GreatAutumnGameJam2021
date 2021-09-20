using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRig : MonoBehaviour
{
    [SerializeField] private Transform bird;
    [SerializeField] private Transform arrivalPosition;
    [SerializeField] private Transform landingPosition;
    [SerializeField] private Transform leavingPosition;
    [SerializeField] private float landingTime = 2.0f;
    [SerializeField] private float landedTimerMin = 0.25f;
    [SerializeField] private float landedTimerMax = 2.0f;
    [SerializeField] private float minimumTime = 5.0f;

    private bool landing = true;
    private bool landed = false;
    private float landingTimer;
    private float landedTimer;
    private float minimumTimer;
    private Collider2D myCollider2D;

    private Animator birdAnimator;

    // Start is called before the first frame update
    void Start()
    {
        landingTimer = landingTime;
        minimumTimer = minimumTime;
        birdAnimator = GetComponentInChildren<Animator>();
        birdAnimator.SetBool("BirdFly", true);
        landedTimer = Random.Range(landedTimerMin, landedTimerMax);
        RandomBirdDirection();
        FindObjectOfType<SFXManager>().PlayBirdLand();
        myCollider2D = GetComponentInChildren<Collider2D>();
        myCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Landing();
        Birding();
        Leaving();
    }

    private void RandomBirdDirection()
    {
        var randomDirection = -1 + 2 * Random.Range(0, 2);
        transform.localScale = new Vector3(randomDirection, 1, 1);
    }

    private void Landing()
    {
        if (!landing) return;

        if (landingTimer > 0)
        {
            landingTimer -= Time.deltaTime;
            var timeRatio = (landingTime - landingTimer) / landingTime;
            bird.position = Vector3.Lerp(arrivalPosition.position, landingPosition.position, timeRatio);
        }
        else
        {
            landing = false;
            landed = true;
            birdAnimator.SetBool("BirdFly", false);
            myCollider2D.enabled = true;
        }
    }

    private void Birding()
    {
        if (!landed) return;

        if (landedTimer > 0)
        {
            minimumTimer -= Time.deltaTime;
            landedTimer -= Time.deltaTime;
        }
        else
        {
            ResetLandedTimer();
        }
    }

    public void ResetLandedTimer()
    {
        landedTimer = Random.Range(landedTimerMin, landedTimerMax);
        var birdAction = 0;
        if (minimumTimer > 0)
        {
            birdAction = Random.Range(0, 9);
        }
        else
        {
            birdAction = Random.Range(0, 10);
        }

        switch (birdAction)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                birdAnimator.SetTrigger("BirdPeck");
                FindObjectOfType<SFXManager>().PlayBirdPeck();
                break;
            case 6:
            case 7:
            case 8:
                birdAnimator.SetTrigger("BirdJump");
                break;
            case 9:
                landed = false;
                landingTimer = landingTime;
                birdAnimator.SetBool("BirdFly", true);
                FindObjectOfType<SFXManager>().PlayBirdTakeoff();
                myCollider2D.enabled = false;
                break;
        }
    }

    private void Leaving()
    {
        if (landing || landed) return;

        if (landingTimer > 0)
        {
            landingTimer -= Time.deltaTime;
            var timeRatio = (landingTime - landingTimer) / landingTime;
            bird.position = Vector3.Lerp(landingPosition.position, leavingPosition.position, timeRatio);
        }
        else
        {
            birdAnimator.SetBool("BirdFly", false);
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
