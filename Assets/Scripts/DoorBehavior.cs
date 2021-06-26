using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject door;

    public AudioClip open;

    public AudioSource doorReject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Capacitor"))
        {
            CapacitorBehavior cb = collision.GetComponent<CapacitorBehavior>();
            if(cb.power > 0)
            {
                AudioSource.PlayClipAtPoint(open, transform.position);
                door.SetActive(false);
                //Open door sound
            }
            else
            {
                //Reject sound
                doorReject.Play();
            }

        }
    }
}
