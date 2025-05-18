using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float timeUntilDeparture = 30f;

    TrainBehavior[] trainCars;

    List<Vector2> trainStopLocations;
    

    private bool hasTrainStopped = false;
    private bool isCountingDown = false;

    void Start()
    {  
        trainCars = FindObjectsByType<TrainBehavior>(FindObjectsSortMode.None);
        Seat[] seats = FindObjectsByType<Seat>(FindObjectsSortMode.None);
        seats[UnityEngine.Random.Range(0, seats.Length)].SetAsGoal();
    }

    void Update()
    {
        if (isCountingDown) 
        {
            CountDownDeparture();
        }
    }

    private void CountDownDeparture()
    {
        if (timeUntilDeparture <= 0)
        {
            isCountingDown = false;
            foreach (TrainBehavior car in trainCars)
            {
                car.Invoke("CloseDoors", 0.2f);
            }
        } else
        {
            timeUntilDeparture -= Time.deltaTime;
        }
    }

    public void TrainArrived()
    {
        isCountingDown = true;

        foreach (TrainBehavior car in trainCars)
        {
            car.Invoke("OpenDoors", 0.2f);
        }
    }

    public void TriggerWin()
    {
        isCountingDown = false;
        FindAnyObjectByType<PlayerController>().gameObject.GetComponent<PlayerInput>().enabled = false;
        foreach (TrainBehavior car in trainCars)
        {
            car.Invoke("CloseDoors", 0.2f);
        }

    }

    public void TriggerLoss()
    {
        FindAnyObjectByType<PlayerController>().gameObject.GetComponent<PlayerInput>().enabled = false;
    }
}
