using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorBehavior : MonoBehaviour
{
    public Sprite[] sprites;

    public int power;

    public bool plugged;
    private bool canCharge;
    private bool canDischarge;

    SpriteRenderer render;

    void Start()
    {
        power = 0;
        canCharge = true;
        canDischarge = true;

        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            plugged = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            plugged = false;
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
}
