using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    public float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        print(timer);
    }
}
