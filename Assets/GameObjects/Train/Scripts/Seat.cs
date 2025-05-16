using UnityEngine;

public class Seat : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private GameObject child;
    private bool isOccupied = true;
    private Sprite[] people;
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;

    private void Start()
    {
        child = GetComponentsInChildren<SpriteRenderer>()[1].gameObject;
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
        _gameStateManager = (GameStateManager)FindAnyObjectByType(typeof(GameStateManager));
        GetComponent<BoxCollider2D>().isTrigger = true;
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
        }

        if (collision.gameObject.CompareTag("NPC-Dynamic")) 
        {
            _gameStateManager.TriggerLoss();
        }
    }


}
