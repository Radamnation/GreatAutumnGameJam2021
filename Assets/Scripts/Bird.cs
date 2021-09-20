using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfPeck()
    {
        //FindObjectOfType<BirdRig>().ResetLandedTimer();
    }

    public void EndOfJump()
    {
        FindObjectOfType<BirdRig>().FlipPosition();
        //FindObjectOfType<BirdRig>().ResetLandedTimer();
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SFXManager>().PlayChirp();
    }
}
