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

    // Start is called before the first frame update
    void Start()
    {
        plugged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearCollider)
        {
            connCable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
         //Player
        if (collision.CompareTag("Player"))
        {
            if(connCable)
            {
                CableObject cable = GameObject.Find("Cable").GetComponent<CableObject>();
                if(!plugged)
                {
                    plugged = true;
                    activatedGenerator.SetActive(true);
                    cable.EndPoint = AnchorPoint;
                } else{
                    plugged = false;
                    activatedGenerator.SetActive(false);
                    AudioSource.PlayClipAtPoint(turnOff, transform.position);
                    cable.EndPoint = GameObject.Find("Player").transform;
                }
                connCable = false;
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
