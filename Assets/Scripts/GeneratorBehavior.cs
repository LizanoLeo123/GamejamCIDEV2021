using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehavior : MonoBehaviour
{
    public GameObject activatedGenerator;

    public AudioClip turnOff;

    private bool plugged;

    // Start is called before the first frame update
    void Start()
    {
        plugged = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug purpose only
        if (Input.GetKeyDown(KeyCode.C))
        {
            plugged = true;
        }

        if (plugged && Input.GetKeyDown(KeyCode.E))
        {
            activatedGenerator.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player
        if (collision.CompareTag("Player"))
        {
            plugged = true;
        }

        //Capacitor
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player
        if (collision.CompareTag("Player"))
        {
            plugged = false;
            activatedGenerator.SetActive(false);
            AudioSource.PlayClipAtPoint(turnOff, transform.position);
        }
    }
}
