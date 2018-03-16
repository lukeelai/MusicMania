using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlayerController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool slam = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float regSpeed = 2f;
    public float jumpForce = 1f;
    public Transform groundCheck;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private int counter;
    private float timer;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("BottomJump") && counter == 0)
        {
            jump = true;
            timer = Time.time;
            counter++;
        }
        else if (Input.GetButtonDown("BottomJump") && counter == 1 && Time.time - timer >= 0.27)
        {
            jump = true;
            counter++;
        }

        if (Input.GetKey("down"))
        {
            slam = true;
        }

    }
    void OnCollisionEnter2D()
    {
        counter = 0;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("BottomHorizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (Mathf.Round(Input.GetAxisRaw("BottomSpeedUp")) < 0)
        {
            maxSpeed = 8;
        }
        else
        {
            maxSpeed = regSpeed;
        }

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

         if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("BottonJump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, 20.0f);
            jump = false;
        }

        if (slam)
        {
            rb2d.AddForce(new Vector2(0f, -jumpForce));
            slam = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
