using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("GolfBall"))
        {
            GameObject startflag = GameObject.Find("StartPole");

            other.gameObject.transform.position = startflag.transform.position + new Vector3(0, 0.2f, 0);
        }
    }
}
