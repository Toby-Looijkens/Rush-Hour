using UnityEngine;

public class TrainCarBehavior : MonoBehaviour
{
    [SerializeField] GameObject trainRoof;

    private Doors[] trainDoors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trainDoors = GetComponentsInChildren<Doors>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoors()
    {
        foreach (Doors door in trainDoors)
        {
            door.OpenDoors();
        }
    }

    public void CloseDoors()
    {
        foreach (Doors door in trainDoors)
        {
            door.CloseDoors();
        }
        Invoke("Depart", 0.5f);
        //trainRoof.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Depart()
    {
        GetComponentInParent<TrainBehavior>().Depart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trainRoof.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player") trainRoof.GetComponent<SpriteRenderer>().enabled = true;
    //}
}
