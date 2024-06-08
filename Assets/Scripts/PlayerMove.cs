using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private float move;
    [SerializeField] public float fallSpeed;

    [SerializeField] private float jump = 16f;
    private bool isJumping;
    private bool isfacingRight = true;

    private float cayoteTime = .1f;
    private float cayoteCounter;
    private float bufferTime = .1f;
    private float bufferCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
            cayoteCounter = cayoteTime;

        }
        else
        {
            cayoteCounter -= Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.Space))
        {
            bufferCounter = bufferTime;

        }
        else
        {
            bufferCounter -= Time.deltaTime;
        }
       // speed =speed * Time.deltaTime;

        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        Flip();

        if (bufferCounter > 0 && cayoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            cayoteCounter = 0f;
            bufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("s");
            rb.AddForce(new Vector2(rb.velocity.x, fallSpeed * -1 ));
        }
    }

    public void Flip()
    {
        if ((isfacingRight && move < 0) || (isfacingRight == false && move > 0))
        {
            isfacingRight = !isfacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            {
            isJumping = true;
        }
    }
}
