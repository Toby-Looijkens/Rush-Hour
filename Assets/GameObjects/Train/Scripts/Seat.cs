using UnityEngine;

public class Seat : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private GameObject child;
    private bool isOccupied = true;
    [SerializeField] private Sprite[] people;
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;
    [SerializeField] bool isTarget = false;

    private void Start()
    {
        child = GetComponentsInChildren<SpriteRenderer>()[1].gameObject;
        child.GetComponent<SpriteRenderer>().sprite = people[UnityEngine.Random.Range(0, people.Length)];
        if (isTarget)
        {
            SetAsGoal();
        }
    }

    private void Update()
    {
        if (!isOccupied)
        {
            child.transform.RotateAround(child.transform.position, Vector3.forward, 100 * Time.deltaTime);
        }       
    }

    public void SetAsGoal()
    {
        isOccupied = false;
        isTarget = true;
        _gameStateManager = (GameStateManager)FindAnyObjectByType(typeof(GameStateManager));
        GetComponent<BoxCollider2D>().enabled = false;
        child.GetComponent<SpriteRenderer>().sprite = sprite;
        child.GetComponent<SpriteRenderer>().color = color;        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameStateManager.TriggerWin();
            collision.gameObject.transform.SetParent(gameObject.transform);
            child.GetComponent<SpriteRenderer>().enabled = false;
        }

        //if (collision.gameObject.CompareTag("NPC-Dynamic")) 
        //{
        //    _gameStateManager.TriggerLoss();
        //}
    }


}
