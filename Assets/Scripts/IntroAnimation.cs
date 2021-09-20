using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimation : MonoBehaviour
{
    [SerializeField] private GameObject introMiddle;
    [SerializeField] private GameObject introBack;
    [SerializeField] private float timeInterval;
    [SerializeField] private float initialWait = 1f;

    private bool startCarving = false;
    private float timeIntervalTimer;

    // Start is called before the first frame update
    void Start()
    {
        timeIntervalTimer = timeInterval;
        StartCoroutine(StartIntro());
    }

    private IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(initialWait);

        FindObjectOfType<Player>().InCarvingZone = true;
        startCarving = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (startCarving)
        {
            if (timeIntervalTimer > 0)
            {
                timeIntervalTimer -= Time.deltaTime;
            }
            else
            {
                if (introMiddle.transform.localPosition.x < 0)
                {
                    var tempPosition = introMiddle.transform.localPosition;
                    tempPosition.x++;
                    introMiddle.transform.localPosition = tempPosition;
                    timeIntervalTimer = timeInterval;
                }
                else if (introBack.transform.localPosition.x < 0)
                {
                    var tempPosition = introBack.transform.localPosition;
                    tempPosition.x++;
                    introBack.transform.localPosition = tempPosition;
                    timeIntervalTimer = timeInterval;
                }
                else
                {
                    StartCoroutine(SwitchScene());
                }
            }
        }
    }

    private IEnumerator SwitchScene()
    {
        FindObjectOfType<Player>().PlayingScratch = false;
        FindObjectOfType<Player>().InCarvingZone = false;

        yield return new WaitForSeconds(3.0f);

        NextScene();
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnMouseDown()
    {
        NextScene();
    }
}
