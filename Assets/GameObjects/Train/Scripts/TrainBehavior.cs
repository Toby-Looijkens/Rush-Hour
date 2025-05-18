using System.Xml.Serialization;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    [SerializeField] GameObject trainRoof;
    [SerializeField] private float Acceleration = 2f;

    private float speed = 15f;

    private Vector2 stopPosition = new Vector2(0, -0.41f);
    private Vector2 startPosition;
    private bool isAccelerating = false;
    private bool isStopping = true;
    private bool hasStopped = false;

    void Start()
    {
        stopPosition = transform.position;
        transform.position = new Vector3(50, -0.41f);
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
            Invoke("OpenDoors", 0.2f);
        }
    }

    public void OpenDoors()
    {
        Doors[] doors = FindObjectsByType<Doors>(FindObjectsSortMode.None);
        foreach (Doors door in doors)
        {
            door.OpenDoors();
        }
    }

    public void CloseDoors()
    {
        Doors[] doors = FindObjectsByType<Doors>(FindObjectsSortMode.None);
        foreach (Doors door in doors)
        {
            door.CloseDoors();
        }
        Invoke("Depart", 0.5f);
        //trainRoof.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Depart()
    {
        stopPosition = new Vector2(-50, -0.41f);
        transform.position = new Vector2(0.0001f, -0.41f);
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
        if (collision.gameObject.tag == "Player")
        {
            trainRoof.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trainRoof.GetComponent<SpriteRenderer>().enabled = true;
    }
}
