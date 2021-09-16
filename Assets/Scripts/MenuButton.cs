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

    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reinitialize()
    {
        mySpriteRenderer.sprite = mySprites[0];
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
        transform.position += offset;

        var toolTip = FindObjectOfType<Player>().GetComponentInChildren<Text>();
        toolTip.enabled = false;
    }

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
        //var menuButtons = FindObjectsOfType<MenuButton>();
        //foreach (MenuButton menuButton in menuButtons)
        //{
        //    menuButton.Reinitialize();
        //}
        //mySpriteRenderer.sprite = mySprites[1];
        //clicked = true;
        switch (id)
        {
            case 0:
                // TO DO
                Debug.Log("Let's go back to main menu");
                break;
            case 1:
                // TO DO, ADD A CONFIRMATION BEFORE RESETING
                FindObjectOfType<GameManager>().ResetCarving();
                break;
            case 2:
                FindObjectOfType<GameManager>().ChangeDayNight();
                break;
            case 3:
                // TO DO
                Debug.Log("Let's take a picture");
                FindObjectOfType<GameManager>().TakePicture();
                // ScreenCapture.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "-Carving Simulator.png");
                // UnityEditor.AssetDatabase.Refresh();
                // Screen.SetResolution(16, 9, false);
                break;
            case 4:
                FindObjectOfType<GameManager>().StartHelpMenu();
                break;
        }
    }
}
