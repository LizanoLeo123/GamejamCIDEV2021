using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorBehavior : MonoBehaviour
{
    public Sprite[] sprites;

    public int power;

    public Transform AnchorPoint;

    public AudioSource turnOn;
    public AudioClip turnOff;

    public bool plugged;
    private bool canCharge;
    private bool canDischarge;

    SpriteRenderer render;
    Rigidbody2D rb;

    private bool connCable = false;
    private bool nearCollider = false;
    private bool newCable = false;

    void Start()
    {
        power = 0;
        canCharge = true;
        canDischarge = true;

        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    plugged = true;
        //}
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    plugged = false;
        //}

        if (Input.GetKeyDown(KeyCode.E) && nearCollider)
        {
            connCable = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && nearCollider)
        {
            newCable = true;
        }

        if (plugged)
        {
            if (canCharge)
            {
                StartCoroutine(ChangeSprite());
            }
        }
        else
        {
            if (canDischarge)
            {
                StartCoroutine(Discharge());
            }
        }
    }

    private IEnumerator ChangeSprite()
    {
        canCharge = false;
        yield return new WaitForSeconds(1f);
        power += 1;

        if (power >= 4)
        {
            power = 3;
        }

        render.sprite = sprites[power];

        canCharge = true;
    }

    private IEnumerator Discharge()
    {
        canDischarge = false;
        yield return new WaitForSeconds(3f);
        power -= 1;

        if (power <= 0)
        {
            power = 0;
        }

        render.sprite = sprites[power];
        canDischarge = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player
        if (collision.CompareTag("Player"))
        {
            nearCollider = true;
        }

        //Capacitor
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player
        if (collision.CompareTag("Player"))
        {
            nearCollider = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Player
        if (collision.CompareTag("Player"))
        {
            Movement player = collision.gameObject.GetComponent<Movement>();

            if (connCable)
            {
                CableObject cable = player.currentCable;
                //CableObject cable = GameObject.Find("Cable").GetComponent<CableObject>();
                if (!plugged && player.hasCable)
                {
                    player.hasCable = false;

                    plugged = true;
                    cable.EndPoint = AnchorPoint;
                    turnOn.Play();
                }
                else if (plugged && !player.hasCable)
                {
                    plugged = false;
                    cable.EndPoint = GameObject.Find("Player").transform;
                    AudioSource.PlayClipAtPoint(turnOff, transform.position);
                    player.hasCable = true;
                }
                connCable = false;
            }

            if (newCable)
            {
                player.lastAnchor = AnchorPoint;
                player.InstantiateCable();
                newCable = false;
            }
        }
    }
}
