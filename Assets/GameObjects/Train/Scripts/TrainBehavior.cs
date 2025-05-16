using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    [SerializeField] GameObject trainRoof;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trainRoof.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trainRoof.GetComponent<SpriteRenderer>().enabled = true;
    }
}
