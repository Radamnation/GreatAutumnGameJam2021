using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] mySprites;
    [SerializeField] private Text toolTip;
    [SerializeField] private AudioClip[] scratchsSounds;
    [SerializeField] private bool playingScratch = false;
    [SerializeField] private float scratchTime = 0.5f;
    [SerializeField] private float movementTreshold = 0.5f;
    [SerializeField] private bool intro = false;

    private float scratchTimer;
    private SpriteRenderer mySpriteRenderer;
    private bool inCarvingZone = false;
    private bool moving = false;

    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;

    public Text ToolTip { get => toolTip; set => toolTip = value; }
    public bool PlayingScratch { get => playingScratch; set => playingScratch = value; }
    public bool InCarvingZone { get => inCarvingZone; set => inCarvingZone = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentMousePosition = new Vector3(0, 0, 0);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        scratchTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        Scratch();
        // previousMousePosition = currentMousePosition;
        // currentMousePosition = Input.mousePosition;
        if (currentMousePosition != previousMousePosition)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    private void Scratch()
    {
        if (!playingScratch || !inCarvingZone)
        {
            scratchTimer = 0;
            return;
        }

        //if (!moving)
        //{
        //    return;
        //}

        if (scratchTimer > 0)
        {
            scratchTimer -= Time.deltaTime;
        }
        else
        {
            previousMousePosition = currentMousePosition;
            currentMousePosition = Input.mousePosition;
            if (intro)
            {
                FindObjectOfType<SFXManager>().PlayScratch();
                scratchTimer = scratchTime;
            }
            else if (currentMousePosition != previousMousePosition)
            {
                var tool = FindObjectOfType<GameManager>().CurrentCarvingTool;
                if (tool == CarvingTool.PEN)
                {
                    FindObjectOfType<SFXManager>().PlayMarker();
                }
                else if (tool == CarvingTool.ERASER)
                {
                    FindObjectOfType<SFXManager>().PlayEraser();
                }
                else if (tool == CarvingTool.RAZOR || tool == CarvingTool.KNIFE || tool == CarvingTool.SAW)
                {
                    FindObjectOfType<SFXManager>().PlayScratch();
                }
                scratchTimer = scratchTime;
            }
        }
    }

    private void UpdatePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var intMousePosition = new Vector3Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y), -5);
        transform.position = intMousePosition;
    }

    public void SwitchToDay()
    {
        mySpriteRenderer.sprite = mySprites[0];
        GetComponentInChildren<Text>().color = new Color32(44, 27, 46, 255);
    }

    public void SwitchToNight()
    {
        mySpriteRenderer.sprite = mySprites[1];
        GetComponentInChildren<Text>().color = new Color32(255, 244, 224, 255);
    }

    public void Transparency(float level)
    {
        var color = Color.white;
        color.a = level;
        mySpriteRenderer.color = color;
    }
}
