using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;

    public bool hasCable;

    Animator animator;
    Rigidbody2D rigidbody2D;
    Vector2 movement;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hasCable = true;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        animator.SetFloat("Movement", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        rigidbody2D.MovePosition(rigidbody2D.position + movement * speed * Time.deltaTime);

        if (movement.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movement.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
