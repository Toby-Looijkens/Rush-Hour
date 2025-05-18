using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] people;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = people[UnityEngine.Random.Range(0, people.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.magnitude > 0)
        {
            SlowDownNPC(1f);
        }
    }

    private void SlowDownNPC(float deceleration)
    {
        Vector3 speedVector = rb.linearVelocity;
        Vector3 invertedSpeedVector = speedVector * -1 * deceleration * Time.deltaTime;

        if (Mathf.Abs(speedVector.x) >= 0 && Mathf.Abs(speedVector.x) <= Mathf.Abs(invertedSpeedVector.x))
        {
            speedVector.x = 0;
        }
        else
        {
            speedVector.x += invertedSpeedVector.x;
        }

        if (Mathf.Abs(speedVector.y) >= 0 && Mathf.Abs(speedVector.y) <= Mathf.Abs(invertedSpeedVector.y))
        {
            speedVector.y = 0;
        }
        else
        {
            speedVector.y += invertedSpeedVector.y;
        }

        rb.linearVelocity = speedVector;
    }


}
