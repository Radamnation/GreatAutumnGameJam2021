using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] mySprites;
    [SerializeField] private Text toolTip;

    private SpriteRenderer mySpriteRenderer;

    public Text ToolTip { get => toolTip; set => toolTip = value; }

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
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
