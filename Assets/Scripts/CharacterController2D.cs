using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 60f;
    private Rigidbody2D rigidbody2D;
    private Vector3 direction;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        float moveX = 0f;
        float moveY = 0f;


        if(Input.GetKey(KeyCode.W)){
            moveY = +1f;
            //transform.position += new Vector3(0, +1);
        }
        if(Input.GetKey(KeyCode.S)){
            moveY = -1f;
            //transform.position += new Vector3(0,-1);
        }
        if(Input.GetKey(KeyCode.A)){
            moveX = -1f;
            //transform.position += new Vector3(-1,0);
        }
        if(Input.GetKey(KeyCode.D)){
            moveX = +1f;
            //transform.position += new Vector3(+1,0);
        }
        direction = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = direction * speed ;
    }
}
