using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class ArrowHead : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ballArrowPrefab;
    public GameObject holeArrowPrefab;
    public Camera arCamera;
    GameObject golfBall, parHole;
    GameObject holeArrow, ballArrow;
    GameObject text1, text2, text3;
    int distance = 1;
    void Start()
    {
        holeArrow = Instantiate(holeArrowPrefab);
        ballArrow = Instantiate(ballArrowPrefab);
        ballArrow.SetActive(false);
        holeArrow.SetActive(false);

        text1 = GameObject.Find("Text1");
        //text2 = GameObject.Find("Text2");
        //text3 = GameObject.Find("Text3");

        //Debug.Log(text1);
        //text1.GetComponent<Text>().text = "Ball Not In View";
        //text2.GetComponent<Text>().text = "Ball Not In View";
    }

    // Update is called once per frame
    void Update()
    {
        //counter++;
        golfBall = GameObject.FindGameObjectWithTag("GolfBall");
        parHole = GameObject.FindGameObjectWithTag("EndPole");
        //text1.GetComponent<Text>().text = "Doing nothing" + counter.ToString();
        //text2.GetComponent<Text>().text = "Doing nothing" + counter.ToString();
        //text3.GetComponent<Text>().text = ballArrow.GetInstanceID().ToString() + "--" + ballArrow.transform.position.ToString() + "####" + holeArrow.GetInstanceID().ToString() + "--" + holeArrow.transform.position.ToString();
        //Debug.Log("Doing nothing" + counter.ToString());

        if (!IsObjectInView(golfBall) && golfBall)
        {
            //text1.GetComponent<Text>().text = "Ball Not In View";
            ballArrow.SetActive(true);
            ballArrow.transform.position = arCamera.transform.position + arCamera.transform.forward * distance;
            ballArrow.transform.rotation = Quaternion.LookRotation(Vector3.forward,GetPositionOnScreen(golfBall, ballArrow));
            //text1.GetComponent<Text>().text = golfBall.GetInstanceID().ToString() + "--" + golfBall.transform.position.ToString();
        }

        if (!IsObjectInView(parHole) && parHole)
        {
            //text2.GetComponent<Text>().text = "Parhole Not In View";
            holeArrow.SetActive(true);
            holeArrow.transform.position = arCamera.transform.position + arCamera.transform.forward * distance + arCamera.transform.right * 0.5f;
            holeArrow.transform.rotation = Quaternion.LookRotation(Vector3.forward,GetPositionOnScreen(parHole, holeArrow));
        }

        if (IsObjectInView(golfBall))
        {
            //text1.GetComponent<Text>().text = "Ball In View";
            ballArrow.SetActive(false);
        }

        if (IsObjectInView(parHole))
        {
            //text2.GetComponent<Text>().text = "Parhole In View";
            holeArrow.SetActive(false);
        }

    }

    bool IsObjectInView(GameObject obj)
    {
        if (obj)
        {
            Vector3 screenPoint = arCamera.WorldToViewportPoint(obj.transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen)
            {
                //text1.GetComponent<Text>().text = "Ball In View";
                Debug.Log("Object is visible on the screen.");
                return true;
            }
            if (!onScreen)
            {
                //text1.GetComponent<Text>().text = "Ball Not In View";
                return false;
            }
        }
        return false;
    }

    Vector3 GetPositionOnScreen(GameObject obj, GameObject arrow)
    {
        Vector3 targetScreenPosition = arCamera.WorldToScreenPoint(obj.transform.position);
        Vector3 arrowScreenPosition = arCamera.WorldToScreenPoint(arrow.transform.position);

        Vector3 direction = targetScreenPosition - arrowScreenPosition;
        direction.z = 0;

        return direction;
        /*Pose objPose = new Pose(obj.transform.position, obj.transform.rotation);

        Vector3 screenPoint = Camera.main.ViewportToScreenPoint(objPose.position);
        Vector3 viewPoint = Camera.main.ScreenToViewportPoint(screenPoint);

        float distance = Vector3.Distance(Camera.main.transform.position, objPose.position);
        Vector3 arrowPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;

        arrowPosition.x = (viewPoint.x - 0.5f) * Camera.main.pixelWidth;
        arrowPosition.y = (viewPoint.y - 0.5f) * Camera.main.pixelHeight;
        arrowPosition.z = 0;

        return arrowPosition;*/
    }
}
