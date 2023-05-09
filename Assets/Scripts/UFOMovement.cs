using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public float amplitude = 0.25f; 
    public float frequency = 0.7f; 
    public bool moveAlongX = true;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Sin(Time.time * frequency) * amplitude;

        Vector3 position = startPos;
        if (moveAlongX)
        {
            position.x += newPosition;
        }
        else
        {
            position.z += newPosition;
        }
        transform.position = position;
    }
}
