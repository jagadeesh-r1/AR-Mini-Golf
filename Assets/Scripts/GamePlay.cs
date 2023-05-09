using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GamePlay : MonoBehaviour
{
    //public ARRaycastManager arRaycastManager;
    public float distance = 0.1f;
    public float force = 0.1f;

    private bool isDragging = false;
    private Vector3 dragStartPosition, mouse_down, mouse_up;
    private Rigidbody rb;
    GameObject text1, text2, text3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        text1 = GameObject.Find("Text1");

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
        if (transform.position.y < -10)
        {
            GameObject startflag = GameObject.Find("StartPole");
            transform.position = startflag.transform.position + new Vector3(0, 0.2f, 0);
            //GameObject instantiatedObject = Instantiate(golfball_prefab, startflag.transform.position + new Vector3(0, 1, 0), startflag.transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnMouseDown()
    {
        mouse_down = Input.mousePosition;
        text1.GetComponent<Text>().text = "Pressed Down" + mouse_down.ToString();
    }

    private void OnMouseUp()
    {
        mouse_up = Input.mousePosition;
        Shoot(mouse_down-mouse_up);
        text1.GetComponent<Text>().text = "Released" + mouse_up.ToString();
    }

    private void Shoot(Vector3 Force)
    {
        //rb.isKinematic = false;
        rb.AddForce(new Vector3(Force.x, 0, Force.y)*0.05f,ForceMode.Impulse);
        //rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //rb.isKinematic = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        //rb.isKinematic = false;
    }
}
