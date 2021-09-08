using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] private Tilemap penTilemap;
    [SerializeField] private Tile[] penColor;
    [SerializeField] private Tilemap topShellTilemap;
    [SerializeField] private Tile[] topShellColor;
    [SerializeField] private Tilemap hardShellTilemap;
    [SerializeField] private Tile[] hardShellColor;
    [SerializeField] private Tilemap softShellTilemap;
    [SerializeField] private Tile[] softShellColor;
    [SerializeField] private Tilemap emptyShellTilemap;
    [SerializeField] private Tile[] emptyShellColor;

    private bool mouseClicked = false;

    public bool MouseClicked { get => mouseClicked; set => mouseClicked = value; }

    //private bool nightMode = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ChangeColor();
    }

    //private void ChangeColor()
    //{
    //    if (!Input.GetKeyDown(KeyCode.L)) return;

    //    if (!nightMode)
    //    {
    //        penTilemap.SwapTile(penColor[0], penColor[1]);
    //        topShellTilemap.SwapTile(topShellColor[0], topShellColor[1]);
    //        hardShellTilemap.SwapTile(hardShellColor[0], hardShellColor[1]);
    //        softShellTilemap.SwapTile(softShellColor[0], softShellColor[1]);
    //        emptyShellTilemap.SwapTile(emptyShellColor[0], emptyShellColor[1]);
    //        nightMode = true;
    //    }
    //    else if (nightMode)
    //    {
    //        penTilemap.SwapTile(penColor[1], penColor[0]);
    //        topShellTilemap.SwapTile(topShellColor[1], topShellColor[0]);
    //        hardShellTilemap.SwapTile(hardShellColor[1], hardShellColor[0]);
    //        softShellTilemap.SwapTile(softShellColor[1], softShellColor[0]);
    //        emptyShellTilemap.SwapTile(emptyShellColor[1], emptyShellColor[0]);
    //        nightMode = false;
    //    }
    //}

    public void SwitchToDay()
    {
        SwitchCandleOff();
        SwitchBlacklightOff();
        penTilemap.SwapTile(penColor[2], penColor[0]);
        topShellTilemap.SwapTile(topShellColor[1], topShellColor[0]);
        hardShellTilemap.SwapTile(hardShellColor[2], hardShellColor[0]);
        softShellTilemap.SwapTile(softShellColor[2], softShellColor[0]);
        emptyShellTilemap.SwapTile(emptyShellColor[2], emptyShellColor[0]);
    }

    public void SwitchToNight()
    {
        penTilemap.SwapTile(penColor[0], penColor[2]);
        topShellTilemap.SwapTile(topShellColor[0], topShellColor[1]);
        hardShellTilemap.SwapTile(hardShellColor[0], hardShellColor[2]);
        softShellTilemap.SwapTile(softShellColor[0], softShellColor[2]);
        emptyShellTilemap.SwapTile(emptyShellColor[0], emptyShellColor[2]);
    }

    public void SwitchCandleOn()
    {
        hardShellTilemap.SwapTile(hardShellColor[2], hardShellColor[1]);
        softShellTilemap.SwapTile(softShellColor[2], softShellColor[1]);
        emptyShellTilemap.SwapTile(emptyShellColor[2], emptyShellColor[1]);
    }

    public void SwitchCandleOff()
    {
        hardShellTilemap.SwapTile(hardShellColor[1], hardShellColor[2]);
        softShellTilemap.SwapTile(softShellColor[1], softShellColor[2]);
        emptyShellTilemap.SwapTile(emptyShellColor[1], emptyShellColor[2]);
    }

    public void SwitchBlacklightOn()
    {
        penTilemap.SwapTile(penColor[2], penColor[1]);
    }

    public void SwitchBlacklightOff()
    {
        penTilemap.SwapTile(penColor[1], penColor[2]);
    }

    private void OnMouseDown()
    {
        mouseClicked = true;
    }

    private void OnMouseUp()
    {
        mouseClicked = false;
    }

    private void OnMouseOver()
    {
        Carve();
    }

    public void Carve()
    {
        if (!mouseClicked || FindObjectOfType<GameManager>().NightMode) return;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var intMousePosition = new Vector3Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y), 0);

        if (FindObjectOfType<GameManager>().CurrentCarvingTool == CarvingTool.PEN)
        {
            if (topShellTilemap.GetTile(intMousePosition) != null)
            {
                penTilemap.SetTile(intMousePosition, penColor[0]);
            }
        }
        if (FindObjectOfType<GameManager>().CurrentCarvingTool == CarvingTool.ERASER)
        {
            penTilemap.SetTile(intMousePosition, null);
        }
        if (FindObjectOfType<GameManager>().CurrentCarvingTool == CarvingTool.RAZOR)
        {
            penTilemap.SetTile(intMousePosition, null);
            topShellTilemap.SetTile(intMousePosition, null);
        }
        else if (FindObjectOfType<GameManager>().CurrentCarvingTool == CarvingTool.KNIFE)
        {
            penTilemap.SetTile(intMousePosition, null);
            topShellTilemap.SetTile(intMousePosition, null);
            hardShellTilemap.SetTile(intMousePosition, null);
        }
        else if (FindObjectOfType<GameManager>().CurrentCarvingTool == CarvingTool.SAW)
        {
            penTilemap.SetTile(intMousePosition, null);
            topShellTilemap.SetTile(intMousePosition, null);
            hardShellTilemap.SetTile(intMousePosition, null);
            softShellTilemap.SetTile(intMousePosition, null);
        }
    }
}
