using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float speed = 3.0f;
    private float yInitial = 1.3f;
    private GameController gc;
    void Start()
    {
        transform.Translate(Vector3.up * Random.Range(-yInitial, yInitial));
        gc = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gc.IsGameOver) transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.tag.Equals("Player")) Destroy(gameObject);
    }
}
