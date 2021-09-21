using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelMenu : MonoBehaviour
{
    [SerializeField] private int id;

    private Collider2D myCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        switch (id)
        {
            case 0:
                FindObjectOfType<SFXManager>().PlayClick();
                FindObjectOfType<GameManager>().ResetCarving();
                break;
            case 1:
                FindObjectOfType<SFXManager>().PlayClick();
                FindObjectOfType<GameManager>().CancelReset();
                break;
        }
    }
}
