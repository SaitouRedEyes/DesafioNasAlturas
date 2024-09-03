using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 initialPosition;
    private float imageSize;
    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        // size = tamanho em pixel da imagem original | localScale = escala do objeto na cena
        // size * localScale = tamanho da imagem na cena na unidade de tela
        imageSize = GetComponent<SpriteRenderer>().size.x * transform.localScale.x;    
        gc = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gc.IsGameOver)
        {
            // repetindo o range de deslocamento da imagem entre 0 e o tamanho da imagem
            float deslocamento = Mathf.Repeat(speed * Time.time, imageSize);

            transform.position = initialPosition + Vector3.left * deslocamento;
        }
    }
}
