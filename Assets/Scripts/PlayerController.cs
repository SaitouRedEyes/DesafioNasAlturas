using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameController gc;
    private Vector3 startPosition;
    [SerializeField]
    private AudioSource scoreSFX;
    private bool doImpulse = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = FindAnyObjectByType<GameController>();
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(doImpulse)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            doImpulse = false;
        }
        anim.SetFloat("speedY", rb.velocity.y);
    }

    public void Impulse(InputAction.CallbackContext context)
    {
        if(context.performed) doImpulse = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        rb.simulated = false;
        anim.enabled = false;
        gc.GameOver();
    }

    public void Restart()
    {        
        rb.simulated = true; 
        anim.enabled = true;
        transform.position = startPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gc.Score = 1;
        scoreSFX.Play();
    }
}
