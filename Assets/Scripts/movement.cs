using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float force = 0.0f;//fuerza en newtons para el objeto
    [SerializeField] private float speed = 0.0f;
    Rigidbody2D rb;
    public Sprite sdoge;
    bool pup = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();

        float movspeed = Input.GetAxis("Horizontal") * speed;
        transform.Translate(movspeed * Time.deltaTime, 0, 0) ;

        if (movspeed < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (movspeed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Jump()
    {
        if (rb && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            rb.AddForce(new Vector2(0, force),ForceMode2D.Impulse);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(pup == false) { 
            Destroy(gameObject);
            }else if(pup == true)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("powerup"))
        {
            Destroy(collider.gameObject);
            pup = true;
            GetComponent<SpriteRenderer>().sprite = sdoge;
        }
    }
}
