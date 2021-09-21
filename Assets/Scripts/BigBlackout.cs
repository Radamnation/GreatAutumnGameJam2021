using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBlackout : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1.0f;
    private float transitionTimer;
    private Color colorWhite;
    private Color colorTransparent;

    private SpriteRenderer mySpriteRenderer;

    private Color currentColor;
    private Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        colorWhite = mySpriteRenderer.color;
        colorTransparent = colorWhite;
        colorTransparent.a = 0;
        transitionTimer = 0;
        targetColor = colorTransparent;
        mySpriteRenderer.color = colorWhite;
        MakeDisappear();
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTimer > 0)
        {
            var ratio = (transitionTime - transitionTimer) / transitionTime;
            mySpriteRenderer.color = Color.Lerp(currentColor, targetColor, ratio);
            transitionTimer -= Time.deltaTime;
        }
    }

    public void MakeAppear()
    {
        currentColor = colorTransparent;
        targetColor = colorWhite;
        transitionTimer = transitionTime;
    }

    public void MakeDisappear()
    {
        currentColor = colorWhite;
        targetColor = colorTransparent;
        transitionTimer = transitionTime;
    }
}
