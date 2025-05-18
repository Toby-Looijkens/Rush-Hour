using System.Xml.Serialization;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    [SerializeField] private float Acceleration = 2f;
    
    private TrainCarBehavior[] trainCars;

    private float speed = 15f;

    private Vector2 stopPosition = new Vector2(0, -0.41f);
    private Vector2 startPosition;
    private bool isAccelerating = false;
    private bool isStopping = true;
    private bool hasStopped = false;
    private bool soundTriggered = false;

    void Start()
    {
        stopPosition = transform.position;
        transform.position = new Vector3(200, -0.41f);
        trainCars = FindObjectsByType<TrainCarBehavior>(FindObjectsSortMode.None);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, stopPosition, speed * Time.deltaTime); 
        
        if (isAccelerating) 
        { 
            Accelerate();
        }

        if (isStopping) 
        { 
            Decelerate();
        }

        if (transform.position == (Vector3)stopPosition && !hasStopped)
        {
            hasStopped = true;
            foreach (TrainCarBehavior car in trainCars)
            {
                car.Invoke("OpenDoors", 0.2f);
            }
        }
    }

    public void OpenDoors()
    {
        foreach (TrainCarBehavior car in trainCars)
        {
            car.OpenDoors();
        }
    }

    public void CloseDoors()
    {
        foreach (TrainCarBehavior car in trainCars)
        {
            car.CloseDoors();
        }
    }

    public void Depart()
    {
        stopPosition = new Vector2(-50, -0.41f);
        isAccelerating = true;
    }

    public void Accelerate()
    {
        if (speed < 15)
        {
            speed += 2 * Time.deltaTime;
        }
        else
        {
            speed = 15f;
            isAccelerating = false;
        }
    }

    public void Decelerate()
    {
        if ((transform.position - (Vector3)stopPosition).magnitude > 0.005f)
        {
            if ((transform.position - (Vector3)stopPosition).magnitude < 15)
            {
                speed = (transform.position - (Vector3)stopPosition).magnitude;
                if (!soundTriggered)
                {
                    //soundTriggered = true;
                    //GetComponent<AudioSource>().Play();
                }
            }
        }
        else
        {
            transform.position = stopPosition;
            startPosition = transform.position;
            isStopping = false;
            FindAnyObjectByType<GameStateManager>().TrainArrived();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            FindFirstObjectByType<GameStateManager>().TriggerLoss();
            Destroy(collision);
        }
    }
}
