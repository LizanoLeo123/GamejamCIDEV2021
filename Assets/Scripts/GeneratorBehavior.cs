using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehavior : MonoBehaviour
{

    public GameObject activatedGenerator;

    public Transform AnchorPoint;

    public AudioClip turnOff;

    private bool plugged;

    private bool connCable = false;
    private bool nearCollider = false;
    private bool newCable = false;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        plugged = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearCollider)
        {
            connCable = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && nearCollider)
        {
            newCable = true;
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
                    player.lastAnchor = AnchorPoint;

                    plugged = true;
                    activatedGenerator.SetActive(true);
                    cable.EndPoint = AnchorPoint;

                    gameManager.TurnGenerator(true);
                } else if (plugged && !player.hasCable){
                    player.hasCable = true;

                    plugged = false;
                    activatedGenerator.SetActive(false);
                    AudioSource.PlayClipAtPoint(turnOff, transform.position);
                    cable.EndPoint = GameObject.Find("Player").transform;
                    gameManager.TurnGenerator(false);
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
        }
    }
    
}
