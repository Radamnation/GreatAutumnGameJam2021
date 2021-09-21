using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    [SerializeField] private Transform owlTransform;
    [SerializeField] private float flickTimerMin = 0.25f;
    [SerializeField] private float flickTimerMax = 2.0f;
    [SerializeField] private float minimumTime = 5.0f;

    private bool sleeping = true;
    private float flickTimer;
    private float minimumTimer;
    private Collider2D myCollider2D;

    private Animator owlAnimator;

    public bool Sleeping { get => sleeping; set => sleeping = value; }

    // Start is called before the first frame update
    void Start()
    {
        owlAnimator = GetComponentInChildren<Animator>();
        myCollider2D = GetComponentInChildren<Collider2D>();
        myCollider2D.enabled = false;
    }

    public void InitializeOwl()
    {
        owlAnimator.SetBool("OwlActive", true);
        minimumTimer = minimumTime;
        sleeping = false;
        flickTimer = Random.Range(flickTimerMin, flickTimerMax);
        myCollider2D.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        OwlAction();
    }

    private void OwlAction()
    {
        if (sleeping) return;

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
            case 4:
            case 5:
                owlAnimator.SetTrigger("OwlBlink");
                break;
            case 6:
            case 7:
                owlAnimator.SetTrigger("OwlFlap");
                FindObjectOfType<SFXManager>().PlayOwlFlap();
                break;
            case 8:
            case 9:
                sleeping = true;
                owlAnimator.SetBool("OwlActive", false);
                myCollider2D.enabled = false;
                break;
        }
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SFXManager>().PlayOwl();
    }
}
