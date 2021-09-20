using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SFXManager>().PlayMeow();
    }

    public void CatStep()
    {
        FindObjectOfType<SFXManager>().PlayCatWalk();
    }
}
