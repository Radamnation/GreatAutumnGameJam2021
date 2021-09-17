using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CarvingTool { NONE, PEN, ERASER, RAZOR, KNIFE, SAW}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Background backgroundPrefab;
    [SerializeField] private GameObject dayMenu;
    [SerializeField] private GameObject nightMenu;
    [SerializeField] private Vector3 menuOffset;
    [SerializeField] private float fadeTransitionTime = 1.0f;

    [SerializeField] private ButtonPanel menuButtons;
    [SerializeField] private ButtonPanel dayButtons;
    [SerializeField] private ButtonPanel nightButtons;

    [SerializeField] private RandomEvent dayEvents;
    [SerializeField] private RandomEvent nightEvents;

    [SerializeField] private float musicVolume = 0.25f;
    private AudioSource dayMusic;
    private AudioSource nightMusic;

    [SerializeField] private GameObject dayCancelMenu;
    [SerializeField] private GameObject nightCancelMenu;

    private CarvingTool currentCarvingTool = CarvingTool.NONE;
    [SerializeField] private bool nightMode = false;

    private float dayMusicTimer = 0;
    private float nightMusicTimer = 0;

    private float currentDayVolume = 0.25f;
    private float targetDayVolume = 0f;
    private float currentNightVolume = 0f;
    private float targetNightVolume = 0.25f;

    private Player myPlayer;

    public CarvingTool CurrentCarvingTool { get => currentCarvingTool; set => currentCarvingTool = value; }
    public bool NightMode { get => nightMode; set => nightMode = value; }

    [SerializeField] private GameObject cameraAim;
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private SpriteRenderer pictureTexture;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private GameObject pictureFrame;
    private Texture2D screencap;
    private Texture2D border;
    bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // screencap = ScreenCapture.CaptureScreenshotAsTexture();
        pictureFrame.SetActive(false);
        // new Texture2D((int) pictureTexture.sprite.textureRect.width, (int) pictureTexture.sprite.textureRect.height, TextureFormat.RGB24, false);
        // border = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        dayCancelMenu.SetActive(false);
        nightCancelMenu.SetActive(false);
        cameraAim.SetActive(false);
        myPlayer = FindObjectOfType<Player>();
        dayMusic = FindObjectOfType<MusicManager>().DayMusic;
        nightMusic = FindObjectOfType<MusicManager>().NightMusic;
    }

    public void BackToHomeScreen()
    {
        FindObjectOfType<MusicManager>().ResetMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ShowCameraAim()
    {
        cameraAim.SetActive(true);
    }

    public void HideCameraAim()
    {
        cameraAim.SetActive(false);
    }

    public void TakePicture()
    {
        StartCoroutine(Capture());
    }

    private IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        
        Camera.main.orthographicSize = 65;
        pictureFrame.SetActive(true);
        myPlayer.gameObject.SetActive(false);
        //var corners = new Vector3[4];
        //targetRect.GetWorldCorners(corners);
        //var bl = RectTransformUtility.WorldToScreenPoint(_camera, corners[0]);
        //var tl = RectTransformUtility.WorldToScreenPoint(_camera, corners[1]);
        //var tr = RectTransformUtility.WorldToScreenPoint(_camera, corners[2]);

        //var height = tl.y - bl.y;
        //var width = tr.x - bl.x;

        //screencap.ReadPixels(new Rect(bl.x, bl.y, width, height), 0, 0);
        //// screencap.ReadPixels(new Rect(198, 98, 298, 198), 0, 0);
        //screencap.Apply();
        shot = true;
    }

    private void OnPostRender()
    {
        if (shot)
        {
            Camera _camera = Camera.main;
            var corners = new Vector3[4];
            targetRect.GetWorldCorners(corners);
            var bl = RectTransformUtility.WorldToScreenPoint(_camera, corners[0]);
            var tl = RectTransformUtility.WorldToScreenPoint(_camera, corners[1]);
            var tr = RectTransformUtility.WorldToScreenPoint(_camera, corners[2]);

            var height = tl.y - bl.y;
            var width = tr.x - bl.x;

            screencap = new Texture2D((int) width, (int) height, TextureFormat.RGB24, false);
            screencap.ReadPixels(new Rect(bl.x, bl.y, width, height), 0, 0);
            screencap.Apply();

            int targetWidth = 520;
            int targetHeight = 500;

            Texture2D result = new Texture2D(targetWidth, targetHeight, screencap.format, true);
            Color[] rpixels = result.GetPixels(0);
            float incX = ((float)1 / screencap.width) * ((float)screencap.width / targetWidth);
            float incY = ((float)1 / screencap.height) * ((float)screencap.height / targetHeight);
            for (int px = 0; px < rpixels.Length; px++)
            {
                rpixels[px] = screencap.GetPixelBilinear(incX * ((float)px % targetWidth),
                                  incY * ((float)Mathf.Floor(px / targetWidth)));
            }
            result.SetPixels(rpixels, 0);
            result.Apply();

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "-Pumpkin Planner 2021.png";
            byte[] bytes = result.EncodeToPNG();
            File.WriteAllBytes(filename, bytes);
            Camera.main.orthographicSize = 40;
            pictureFrame.SetActive(false);
            myPlayer.gameObject.SetActive(true);
            shot = false;
            Instantiate(cameraFlash);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeDayNight();
        //ResetCarving();
        if (dayMusicTimer > 0)
        {
            var ratio = (fadeTransitionTime - dayMusicTimer) / fadeTransitionTime;
            dayMusic.volume = Mathf.Lerp(currentDayVolume, targetDayVolume, ratio);
            dayMusicTimer -= Time.deltaTime;
        }
        else if (nightMusicTimer > 0)
        {
            var ratio = (fadeTransitionTime - nightMusicTimer) / fadeTransitionTime;
            nightMusic.volume = Mathf.Lerp(currentNightVolume, targetNightVolume, ratio);
            nightMusicTimer -= Time.deltaTime;
        }
    }

    public void ChangeDayNight()
    {
        if (dayMusic == null || nightMusic == null)
        {
            dayMusic = FindObjectOfType<MusicManager>().DayMusic;
            nightMusic = FindObjectOfType<MusicManager>().NightMusic;
        }
        //if (!Input.GetKeyDown(KeyCode.L)) return;

        if (!nightMode)
        {
            StartCoroutine(FadeOutNight());
        }
        else if (nightMode)
        {
            StartCoroutine(FadeOutDay());
        }
    }

    public void StartHelpMenu()
    {
        if (!nightMode)
        {
            dayEvents.StopEvents();
        }
        else if (nightMode)
        {
            nightEvents.StopEvents();
        }
        FindObjectOfType<HelpMenu>().StartHelpMenu();
    }

    public void EndHelpMenu()
    {
        if (!nightMode)
        {
            dayEvents.StartEvents();
        }
        else if (nightMode)
        {
            nightEvents.StartEvents();
        }
    }

    private IEnumerator FadeOutNight()
    {
        currentDayVolume = 0.25f;
        targetDayVolume = 0f;
        dayMusicTimer = fadeTransitionTime;
        FindObjectOfType<Blackout>().MakeAppear(fadeTransitionTime);
        menuButtons.MoveOut(fadeTransitionTime);
        dayButtons.MoveOut(fadeTransitionTime);
        dayMusic.volume = 0f;

        yield return new WaitForSeconds(fadeTransitionTime * 1.25f);

        SwitchToNight();
        StartCoroutine(FadeInNight());
    }

    private void SwitchToNight()
    {
        // dayMenu.transform.position += menuOffset;
        // nightMenu.transform.position -= menuOffset;

        dayEvents.StopEvents();
        nightEvents.StartEvents();
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

    private IEnumerator FadeInNight()
    {
        currentNightVolume = 0f;
        targetNightVolume = 0.25f;
        nightMusicTimer = fadeTransitionTime;
        FindObjectOfType<Blackout>().MakeDisappear(fadeTransitionTime);
        menuButtons.MoveIn(fadeTransitionTime);
        nightButtons.MoveIn(fadeTransitionTime);

        yield return new WaitForSeconds(fadeTransitionTime);
    }

    private IEnumerator FadeOutDay()
    {
        currentNightVolume = 0.25f;
        targetNightVolume = 0f;
        nightMusicTimer = fadeTransitionTime;
        FindObjectOfType<Blackout>().MakeAppear(fadeTransitionTime);
        menuButtons.MoveOut(fadeTransitionTime);
        nightButtons.MoveOut(fadeTransitionTime);

        yield return new WaitForSeconds(fadeTransitionTime * 1.25f);

        SwitchToDay();
        StartCoroutine(FadeInDay());
    }

    private void SwitchToDay()
    {
        // dayMenu.transform.position -= menuOffset;
        // nightMenu.transform.position += menuOffset;
        nightEvents.StopEvents();
        dayEvents.StartEvents();
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

    private IEnumerator FadeInDay()
    {
        currentDayVolume = 0f;
        targetDayVolume = 0.25f;
        dayMusicTimer = fadeTransitionTime;
        FindObjectOfType<Blackout>().MakeDisappear(fadeTransitionTime);
        menuButtons.MoveIn(fadeTransitionTime);
        dayButtons.MoveIn(fadeTransitionTime);

        yield return new WaitForSeconds(fadeTransitionTime);
    }

    public void AskResetCarving()
    {
        if (!nightMode)
        {
            dayCancelMenu.SetActive(true);
        }
        else if (nightMode)
        {
            nightCancelMenu.SetActive(true);
        }
    }

    public void CancelReset()
    {
        if (!nightMode)
        {
            dayCancelMenu.SetActive(false);
        }
        else if (nightMode)
        {
            nightCancelMenu.SetActive(false);
        }
    }

    public void ResetCarving()
    {
        if (nightMode)
        {
            SwitchToDay();
        }

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
