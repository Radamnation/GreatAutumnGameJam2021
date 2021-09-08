using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] mySprites;

    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToDay()
    {
        mySpriteRenderer.sprite = mySprites[0];
    }

    public void SwitchToNight()
    {
        mySpriteRenderer.sprite = mySprites[1];
    }
}
