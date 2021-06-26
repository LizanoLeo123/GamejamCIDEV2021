using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    private Vector3 direction;
    private Rigidbody2D body;
    private float InputX, InputY;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        animator.SetFloat("Movement", Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");

        if (InputX != 0 || InputY != 0)
        {
            direction = new Vector3(InputX, InputY, 0).normalized;
            body.MovePosition(new Vector2((transform.position.x + direction.x * speed * Time.deltaTime), (transform.position.y + direction.y * speed * Time.deltaTime)));
        }

        if (InputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (InputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }
}
