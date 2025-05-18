using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] float distanceToTravel = 0.025f;
    [SerializeField] float doorSpeed = 2f;
    [SerializeField] bool isEnabled = true;

    private Vector2 leftDoorClosed;
    private Vector2 rightDoorClosed;

    private bool isOpening = false;
    private bool isClosing = false;
    [SerializeField] private float progress = 0f;
    private float distanceFromCenter = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftDoorClosed = leftDoor.transform.position;
        rightDoorClosed = rightDoor.transform.position;
        distanceFromCenter = (transform.position - leftDoor.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        leftDoorClosed = transform.position + new Vector3(-distanceFromCenter, 0, 0);
        rightDoorClosed = transform.position + new Vector3(distanceFromCenter, 0, 0);

        if (isOpening)
        {
            progress += Time.deltaTime * doorSpeed;
            if (progress > 1) 
            {
                progress = 1;
                isOpening = false;
            }
            leftDoor.transform.position = Vector2.Lerp(leftDoorClosed, leftDoorClosed + new Vector2(-distanceToTravel, 0), progress);
            rightDoor.transform.position = Vector2.Lerp(rightDoorClosed, rightDoorClosed + new Vector2(distanceToTravel, 0), progress);
        }

        if (isClosing) 
        {
            progress -= Time.deltaTime * doorSpeed;
            if (progress < 0) 
            {
                progress = 0;
                isClosing = false;

            }
            leftDoor.transform.position = Vector2.Lerp(leftDoorClosed, leftDoorClosed + new Vector2(-distanceToTravel, 0), progress);
            rightDoor.transform.position = Vector2.Lerp(rightDoorClosed, rightDoorClosed + new Vector2(distanceToTravel, 0), progress);
        }
    }

    public void OpenDoors()
    {
        if(isEnabled)
        {
            isOpening = true;
        }
    }

    public void CloseDoors()
    {
        if(isEnabled) 
        {
            isClosing = true;
        }
    }
}
