using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoors()
    {
        GetComponent<Animation>().Play("DoorOpenClose");
    }

    public void CloseDoors()
    {

    }
}
