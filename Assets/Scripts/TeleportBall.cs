using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GOLF;
    void Start()
    {
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("GolfBall"))
        {
            GameObject telA = GameObject.Find("TeleportA");
            GameObject telB = GameObject.Find("TeleportB");

            if (gameObject.name == "ColliderA")
            {
                //other.gameObject.transform.position = telB.transform.position + new Vector3(0.2f,0.2f,0);
                other.gameObject.SetActive(false);
                Instantiate(GOLF, telB.transform.position + new Vector3(0.2f, 0.2f, 0), Quaternion.identity);
            }
            if (gameObject.name == "ColliderB")
            {
                //other.gameObject.transform.position = telA.transform.position + new Vector3(0.2f, 0.2f, 0);
                other.gameObject.SetActive(false);
                Instantiate(GOLF, telA.transform.position + new Vector3(0.2f, 0.2f, 0), Quaternion.identity);
            }
        }
    }
}
