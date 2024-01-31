using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Component references
    // TODO: create variable to store rigidbody of player (2D)
    Rigidbody2D rb;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;

    //TODO: keep track of current horizontal movement direction
    float direction = 0f;

    //keep track of if the player is on the ground
    bool isGrounded = false;

    //TODO: keep track of which direction player is facing
    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get references to the rigidbody attached to the current GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

        // TODO: check conditions needed to flip player, and if so, flip player
        if (isFacingRight && direction == -1 || !isFacingRight && direction == 1)
        {
            Flip();
        }
    }

    void OnJump()
    {
        //if player is on the ground, jump
        if (isGrounded)
        {
            Jump();
        }

    }

    private void Jump()
    {
        // TODO: change y velocity of player
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        List<GameObject> currentCollisions = new List<GameObject>();
        currentCollisions.Add(collision.gameObject);
        Debug.Log(currentCollisions);
        isGrounded = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Vector2.Angle(collision.GetContact(i).normal, Vector2.up) < 45f)
            {
                Debug.Log(Vector2.Angle(collision.GetContact(i).normal, Vector2.up));
                isGrounded = true;
                return;
            }
        }
    }

        private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }


    void OnMove(InputValue moveVal)
    {
        // TODO: store direction input and store it to global variable
        float movDirection = moveVal.Get<float>();
        direction = movDirection;
    }

    private void Move(float x)
    {
        // TODO: change x velocity of player
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    //commonly used function but not used in this case
    // void OnCollisionEnter(Collision collision)
    // {

    // }

    // void OnCollisionStay(Collision collision)
    // {
    //     //check if angle between normal vector of object of contact and up vector is less than 45 degrees
    //     //AKA if-statement is true if player is touching another object that is 0 to 45 degrees slope
    //     if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
    //         isGrounded = true;
    //     else
    //         isGrounded = false;
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     isGrounded = false;
    // }

    private void Flip()
    {
        // TODO: flip local scale of player and change global variable that stores which direction player is facing
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectibles"))
        {
            Destroy(other.gameObject);
        }
    }
}
