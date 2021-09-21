using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlRig : MonoBehaviour
{
    private Owl myOwl;

    // Start is called before the first frame update
    void Start()
    {
        myOwl = FindObjectOfType<Owl>();
        myOwl.InitializeOwl();
    }

    // Update is called once per frame
    void Update()
    {
        if (myOwl.Sleeping)
        {
            Destroy(transform.gameObject);
        }
    }
}
