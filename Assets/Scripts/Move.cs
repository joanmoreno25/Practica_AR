using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float from;
    [SerializeField]
    private float to;

    [SerializeField]
    private float speed;

    private bool forward = true;
    void Update()
    {
        if(forward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.z > to)
            {
                forward = false;
            }
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (transform.position.z <= from)
            {
                forward = true;
            }
        }

        
    }
}
