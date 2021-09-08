using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarvingArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<Player>().Transparency(0.25f);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<Player>().Transparency(1.0f);
    }

    private void OnMouseDown()
    {
        FindObjectOfType<Pumpkin>().MouseClicked = true;
    }

    private void OnMouseUp()
    {
        FindObjectOfType<Pumpkin>().MouseClicked = false;
    }

    private void OnMouseOver()
    {
        FindObjectOfType<Pumpkin>().Carve();
    }
}
