using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    private Collider2D myCollider2D;
    private SpriteRenderer[] helpScreens;

    // Start is called before the first frame update
    void Start()
    {
        myCollider2D = GetComponent<Collider2D>();
        helpScreens = GetComponentsInChildren<SpriteRenderer>();

        myCollider2D.enabled = false;
        foreach (SpriteRenderer helpScreen in helpScreens)
        {
            helpScreen.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHelpMenu()
    {
        helpScreens[0].gameObject.SetActive(true);
        myCollider2D.enabled = true;
    }

    private void OnMouseDown()
    {
        if (helpScreens[0].gameObject.activeSelf == true)
        {
            helpScreens[0].gameObject.SetActive(false);
            helpScreens[1].gameObject.SetActive(true);
        }
        else if (helpScreens[1].gameObject.activeSelf == true)
        {
            helpScreens[1].gameObject.SetActive(false);
            helpScreens[2].gameObject.SetActive(true);
        }
        else if (helpScreens[2].gameObject.activeSelf == true)
        {
            helpScreens[2].gameObject.SetActive(false);
            myCollider2D.enabled = false;
            FindObjectOfType<GameManager>().EndHelpMenu();
        }
    }
}
