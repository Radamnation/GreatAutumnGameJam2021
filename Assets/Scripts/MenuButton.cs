using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite[] mySprites;
    [SerializeField] private string myLabel;
    [SerializeField] private AudioClip clickSFX;

    private SpriteRenderer mySpriteRenderer;
    private Vector3 initialPosition;

    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reinitialize()
    {
        if (!FindObjectOfType<GameManager>().NightMode)
        {
            mySpriteRenderer.sprite = mySprites[1];
        }
        else if (FindObjectOfType<GameManager>().NightMode)
        {
            mySpriteRenderer.sprite = mySprites[0];
        }
        transform.localPosition = initialPosition;
        clicked = false;
    }

    public void SwitchToDay()
    {
        mySpriteRenderer.sprite = mySprites[0];
    }

    public void SwitchToNight()
    {
        mySpriteRenderer.sprite = mySprites[1];
    }

    private void OnMouseEnter()
    {
        transform.position -= offset;

        var toolTip = FindObjectOfType<Player>().GetComponentInChildren<Text>();
        toolTip.enabled = true;
        toolTip.rectTransform.anchoredPosition = new Vector2(300, 0);
        toolTip.alignment = TextAnchor.UpperLeft;
        toolTip.text = myLabel;
    }

    private void OnMouseExit()
    {
        if (clicked && id == 2)
        {

        }
        else
        {
            transform.position += offset;
        }

        var toolTip = FindObjectOfType<Player>().ToolTip;
        toolTip.enabled = false;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SFXManager>().PlayClick();
        // mySpriteRenderer.sprite = mySprites[1];
        clicked = true;
        switch (id)
        {
            case 0:
                FindObjectOfType<GameManager>().AskChangeMusicLevel();
                break;
            case 1:
                FindObjectOfType<GameManager>().AskResetCarving();
                break;
            case 2:
                FindObjectOfType<GameManager>().ChangeDayNight();
                break;
            case 3:
                FindObjectOfType<GameManager>().ShowCameraAim();
                break;
            case 4:
                FindObjectOfType<GameManager>().StartHelpMenu();
                break;
        }
    }
}
