using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehavior : MonoBehaviour
{
    public GameObject activatedGenerator;

    private bool condition;

    // Start is called before the first frame update
    void Start()
    {
        condition = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug purpose only
        if (Input.GetKeyDown(KeyCode.C))
        {
            condition = true;
        }

        if (condition && Input.GetKeyDown(KeyCode.E))
        {
            activatedGenerator.SetActive(true);
        }
    }
}
