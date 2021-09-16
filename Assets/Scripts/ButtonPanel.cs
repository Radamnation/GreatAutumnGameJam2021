using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1.0f;
    private float transitionTimer;
    
    [SerializeField] private Vector3 inPosition;
    [SerializeField] private Vector3 outPosition;

    private Vector3 currentPosition;
    private Vector3 targetTarget;

    // Start is called before the first frame update
    void Start()
    {
        transitionTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTimer > 0)
        {
            var ratio = (transitionTime - transitionTimer) / transitionTime;
            transform.position = Vector3.Lerp(currentPosition, targetTarget, ratio);
            transitionTimer -= Time.deltaTime;
        }
    }

    public void MoveIn(float time)
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
        transitionTime = time;
        currentPosition = outPosition;
        targetTarget = inPosition;
        transitionTimer = transitionTime;
    }

    public void MoveOut(float time)
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        transitionTime = time;
        currentPosition = inPosition;
        targetTarget = outPosition;
        transitionTimer = transitionTime;
    }
}
