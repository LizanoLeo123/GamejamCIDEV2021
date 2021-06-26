using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;

    public bool hasCable;
    public Transform lastAnchor;

    public Transform cablesContainer;
    public GameObject cable;
    public CableObject currentCable;

    Animator animator;
    Rigidbody2D rigidbody2D;
    Vector2 movement;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hasCable = true;
        lastAnchor = transform;
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

    public void InstantiateCable()
    {
        //CableObject cableScript = cable
        GameObject spawnedCable = Instantiate(cable, cablesContainer);
        CableObject cableScript = spawnedCable.GetComponent<CableObject>();
        cableScript.StartPoint = lastAnchor;
        cableScript.EndPoint = transform;

        currentCable = cableScript;

        hasCable = true;
    }
}
