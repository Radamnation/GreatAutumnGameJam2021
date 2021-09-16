using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] eventPrefabs;
    [SerializeField] private bool generateEvents = false;

    private GameObject currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (generateEvents)
        {
            GenerateEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (generateEvents)
        {
            EventExist();
        }
    }

    private void EventExist()
    {
        if (currentEvent == null)
        {
            GenerateEvent();
        }
    }

    private void GenerateEvent()
    {
        currentEvent = Instantiate(eventPrefabs[Random.Range(0, eventPrefabs.Length)]);
    }

    public void StopEvents()
    {
        generateEvents = false;
        Destroy(currentEvent.gameObject);
    }

    public void StartEvents()
    {
        generateEvents = true;
    }
}
