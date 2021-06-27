using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorBehavior : MonoBehaviour
{
    public Sprite[] sprites;

    public int power;
    public bool electrified = false;

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
    List<CableObject> cables = new List<CableObject>();
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

        if (electrified)
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

        if(cablesElectrified())
        {
            electrified = true;
            for(int i = 0; i < cables.Count; i++){
                cables[i].electrified = true;
            }
        } else if(electrified)
        {
            for(int i = 0; i < cables.Count; i++){
                if(!cables[i].permanentElectrified){
                    cables[i].electrified = false;
                }
            }
            electrified = false;
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
                if(player.currentCable == null)
                {
                    return;
                }
                //CableObject cable = GameObject.Find("Cable").GetComponent<CableObject>();
                if (!plugged && player.hasCable && cable.inRange)
                {
                    player.hasCable = false;
                    player.currentCable = null;
                    plugged = true;
                    cable.EndPoint = AnchorPoint;
                    turnOn.Play();
                    cables.Add(cable);
                }
                else if (plugged && !player.hasCable)
                {
                    plugged = false;
                    cable.EndPoint = GameObject.Find("Player").transform;
                    AudioSource.PlayClipAtPoint(turnOff, transform.position);
                    player.hasCable = true;
                    cables.Remove(cable);
                }
                connCable = false;
            }

            if (newCable)
            {
                player.lastAnchor = AnchorPoint;
                player.InstantiateCable();
                cables.Add(player.currentCable);
                newCable = false;
            }
        }
    }

    private bool cablesElectrified(){
        bool electrified = false;
        
        for(int i = 0; i < cables.Count; i++){
            if (cables[i].inRange && cables[i].electrified)
            {
                return true;
            }
        }

        return electrified;
    }
}
