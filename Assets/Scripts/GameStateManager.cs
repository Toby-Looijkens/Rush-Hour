using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float timeUntilDeparture = 30f;

    TrainBehavior train;
    [SerializeField] NPCBehavior[] npcs;
    [SerializeField] UIController controller;
    [SerializeField] TextMeshProUGUI timer;
    PlayerController playerController;
    List<Vector2> trainStopLocations;
    

    private bool hasTrainStopped = false;
    private bool isCountingDown = false;

    void Start()
    {
        train = FindAnyObjectByType<TrainBehavior>();
        playerController = FindAnyObjectByType<PlayerController>();

        foreach (NPCBehavior npc in npcs)
        {
            npc.GetComponent<CircleCollider2D>().enabled = false;
        }
        //Seat[] seats = FindObjectsByType<Seat>(FindObjectsSortMode.None);
        //seats[UnityEngine.Random.Range(0, seats.Length)].SetAsGoal();
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
            TriggerLoss();
        } else
        {
            timeUntilDeparture -= Time.deltaTime;
            timer.text = Mathf.Round(timeUntilDeparture).ToString();
        }
    }

    private void FinishGame()
    {
        timer.text = "";
        isCountingDown = false;
        DestroyRigidBodies();
        playerController.transform.SetParent(train.transform, true);
        FindAnyObjectByType<PlayerController>().gameObject.GetComponent<PlayerInput>().enabled = false;
    }

    private void DestroyRigidBodies()
    {
        foreach (NPCBehavior npc in npcs)
        {
            Destroy(npc.GetComponent<CircleCollider2D>());
        }

        Destroy(playerController.GetComponent<Collider2D>());
    }

    public void TrainArrived()
    {
        isCountingDown = true;
        foreach (NPCBehavior npc in npcs)
        {
            npc.GetComponent<CircleCollider2D>().enabled = true;
        }

        train.Invoke("OpenDoors", 0.2f);
    }

    public void TriggerWin()
    {
        FinishGame();
        train.Invoke("CloseDoors", 0.2f);
        controller.Invoke("OpenWinScreen", 5);
    }

    public void TriggerLoss()
    {
        FinishGame();
        train.Invoke("CloseDoors", 0.2f);
        controller.Invoke("OpenLossScreen", 5);
    }
}
