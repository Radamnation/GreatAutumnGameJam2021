using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool isCameraAim;

    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePicture()
    {
        if (!FindObjectOfType<GameManager>().NightMode)
        {
            mySpriteRenderer.sprite = sprites[0];
        }
        else if (FindObjectOfType<GameManager>().NightMode)
        {
            mySpriteRenderer.sprite = sprites[1];
        }
    }

    private void OnMouseDown()
    {
        if (isCameraAim)
        {
            FindObjectOfType<GameManager>().TakePicture();
            FindObjectOfType<GameManager>().HideCameraAim();
        }
        else
        {
            FindObjectOfType<SFXManager>().PlayClick();
            this.gameObject.SetActive(false);
        }
        
    }
}
