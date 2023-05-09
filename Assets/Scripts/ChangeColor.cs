using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("GolfBall"))
        {
            GetComponent<Renderer>().material.color = new Color(0.1f, 0f, 0f, 0.001f);
        }
    }
}
