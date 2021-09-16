using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushButton : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite[] mySprites;
    [SerializeField] private string myLabel;
    [SerializeField] private AudioClip clickSFX;

    private SpriteRenderer mySpriteRenderer;

    private bool clicked = false;
    private Vector3 initialPosition;

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
        mySpriteRenderer.sprite = mySprites[0];
        transform.localPosition = initialPosition;
        clicked = false;
    }

    private void OnMouseEnter()
    {
        if (clicked) return;
        
        var toolTip = FindObjectOfType<Player>().GetComponentInChildren<Text>();
        toolTip.enabled = true;
        toolTip.rectTransform.anchoredPosition = new Vector2(-300, 0);
        toolTip.alignment = TextAnchor.UpperRight;
        toolTip.text = myLabel;
        MovedToClicked();
    }

    private void OnMouseExit()
    {
        var toolTip = FindObjectOfType<Player>().GetComponentInChildren<Text>();
        toolTip.enabled = false;

        if (clicked) return;

        MovedToUnclicked();
    }

    private void MovedToClicked()
    {
        transform.position -= offset;
    }

    private void MovedToUnclicked()
    {
        transform.position += offset;
    }

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
        var brushButtons = FindObjectsOfType<BrushButton>();
        foreach (BrushButton brushButton in brushButtons)
        {
            brushButton.Reinitialize();
        }
        mySpriteRenderer.sprite = mySprites[1];
        clicked = true;
        MovedToClicked();
        switch (id)
        {
            case 0:
                FindObjectOfType<GameManager>().CurrentCarvingTool = CarvingTool.PEN;
                break;
            case 1:
                FindObjectOfType<GameManager>().CurrentCarvingTool = CarvingTool.ERASER;
                break;
            case 2:
                FindObjectOfType<GameManager>().CurrentCarvingTool = CarvingTool.RAZOR;
                break;
            case 3:
                FindObjectOfType<GameManager>().CurrentCarvingTool = CarvingTool.KNIFE;
                break;
            case 4:
                FindObjectOfType<GameManager>().CurrentCarvingTool = CarvingTool.SAW;
                break;
        }
    }
}
