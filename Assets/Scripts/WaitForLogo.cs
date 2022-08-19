using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class WaitForLogo : MonoBehaviour
{
    private bool readyToStart = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SplashScreen.isFinished)
        {
            readyToStart = true;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        if (readyToStart)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
