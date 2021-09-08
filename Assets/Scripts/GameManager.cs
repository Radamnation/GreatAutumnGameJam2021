using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarvingTool { NONE, PEN, ERASER, RAZOR, KNIFE, SAW}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Background backgroundPrefab;
    [SerializeField] private GameObject dayMenu;
    [SerializeField] private GameObject nightMenu;
    [SerializeField] private Vector3 menuOffset;

    private CarvingTool currentCarvingTool = CarvingTool.NONE;
    [SerializeField] private bool nightMode = false;

    public CarvingTool CurrentCarvingTool { get => currentCarvingTool; set => currentCarvingTool = value; }
    public bool NightMode { get => nightMode; set => nightMode = value; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeDayNight();
        //ResetCarving();
    }

    public void ChangeDayNight()
    {
        //if (!Input.GetKeyDown(KeyCode.L)) return;

        if (!nightMode)
        {
            SwitchToNight();
        }
        else if (nightMode)
        {
            SwitchToDay();
        }
    }

    private void SwitchToNight()
    {
        dayMenu.transform.position += menuOffset;
        nightMenu.transform.position -= menuOffset;
        FindObjectOfType<Background>().SwitchToNight();
        FindObjectOfType<Pumpkin>().SwitchToNight();
        FindObjectOfType<Player>().SwitchToNight();
        var menuButtons = FindObjectsOfType<MenuButton>();
        foreach (MenuButton menuButton in menuButtons)
        {
            menuButton.SwitchToNight();
        }
        var nightButtons = FindObjectsOfType<NightButton>();
        foreach (NightButton nightButton in nightButtons)
        {
            nightButton.Reinitialize();
        }
        nightMode = true;
    }

    private void SwitchToDay()
    {
        dayMenu.transform.position -= menuOffset;
        nightMenu.transform.position += menuOffset;
        FindObjectOfType<Background>().SwitchToDay();
        FindObjectOfType<Pumpkin>().SwitchToDay();
        FindObjectOfType<Player>().SwitchToDay();
        var menuButtons = FindObjectsOfType<MenuButton>();
        foreach (MenuButton menuButton in menuButtons)
        {
            menuButton.SwitchToDay();
        }
        var brushButtons = FindObjectsOfType<BrushButton>();
        foreach (BrushButton brushButton in brushButtons)
        {
            brushButton.Reinitialize();
        }
        nightMode = false;
    }

    public void ResetCarving()
    {
        SwitchToDay();

        var brushButtons = FindObjectsOfType<BrushButton>();
        foreach (BrushButton brushButton in brushButtons)
        {
            brushButton.Reinitialize();
        }
        currentCarvingTool = CarvingTool.NONE;
        var currentBackground = FindObjectOfType<Background>();
        Destroy(currentBackground.gameObject);
        Instantiate(backgroundPrefab);
    }
}
