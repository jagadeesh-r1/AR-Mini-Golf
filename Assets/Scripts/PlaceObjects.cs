using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class PlaceObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ufo_prefab, windmill_prefab, golfball_prefab;
    public PlaneSelection planeSelection;
    private bool flag = true;
    private bool boundingbox = false;

    private ARPlane GroundPlane, ElevatedPlane;
    private NativeArray<Vector2> groundPlaneBoundary;
    private int counter = 0, fps = 0;
    private float pingPongValue = 0f;
    public float moveSpeed = 1f;
    [SerializeField] private ARRaycastManager raycastManager;
    //float minX, maxX, minZ, maxZ;
    Vector3 targetPosition;
    float speed = 1f;

    public GameObject Cube;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fps++;
        if (planeSelection.selectedPlanes.Count == 2 && flag)
        {
            if (planeSelection.selectedPlanes[0].transform.position.y > planeSelection.selectedPlanes[1].transform.position.y)
            {
                GroundPlane = planeSelection.selectedPlanes[1];
                ElevatedPlane = planeSelection.selectedPlanes[0];
            }
            else
            {
                GroundPlane = planeSelection.selectedPlanes[0];
                ElevatedPlane = planeSelection.selectedPlanes[1];
            }

            Color color = new Color(0, 1, 0, 0f);

            GroundPlane.GetComponent<MeshRenderer>().material.color = color;
            ElevatedPlane.GetComponent<MeshRenderer>().material.color = color;
            //groundPlaneBoundary = GroundPlane.boundary;
            //for(int i = 0; i < groundPlaneBoundary.Length; i++)
            //{
            //    groundPlaneBoundary[i] = GroundPlane.transform.TransformPoint(new Vector3(groundPlaneBoundary[i].x,, groundPlaneBoundary[i].y));
            //}
            GameObject b1canvas = GameObject.Find("uiCanvas_b1");
            b1canvas.GetComponent<Canvas>().enabled = true;
            flag = false;
            boundingbox = true;
        }


        //GameObject[] flyingObjects = GameObject.FindGameObjectsWithTag("FlyingObject");
        //if (flyingObjects.Length > 0)
        //{
        //    foreach (GameObject obj in flyingObjects)
        //   {
        //        if (Vector3.Distance(obj.transform.position, targetPosition) < 0.1f)
        //        {
        //            targetPosition = new Vector3(Random.Range(minX, maxX), obj.transform.position.y, Random.Range(minZ, maxZ));
        //        }

        //        obj.transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        //    }
            /*pingPongValue = Mathf.PingPong(Time.time * moveSpeed, groundPlaneBoundary.Length);
            float boundaryPercentage = pingPongValue / (groundPlaneBoundary.Length - 1);

            Vector3 currentPoint = groundPlaneBoundary[counter]; //GroundPlane.transform.TransformPoint(new Vector3(groundPlaneBoundary[counter].x,flyingObjects[0].transform.position.y, groundPlaneBoundary[counter].y));
            Vector3 nextPoint = groundPlaneBoundary[(counter + 1) % groundPlaneBoundary.Length]; //GroundPlane.transform.TransformPoint(new Vector3(groundPlaneBoundary[(counter + 1) % groundPlaneBoundary.Length].x, flyingObjects[0].transform.position.y, groundPlaneBoundary[(counter + 1) % groundPlaneBoundary.Length].y));
            Vector3 interpolatedPoint = Vector3.Lerp(currentPoint, nextPoint, boundaryPercentage);

            foreach (GameObject obj in flyingObjects)
            {
                obj.transform.position = interpolatedPoint;
            }

            if (boundaryPercentage >= 1f)
            {
                counter = (counter + 1) % groundPlaneBoundary.Length;
            }*/

            //flyingObjects[0].transform.position = GroundPlane.transform.TransformPoint(groundPlaneBoundary[counter]);
            //flyingObjects[0].transform.position = new Vector3(groundPlaneBoundary[counter% groundPlaneBoundary.Length].x,flyingObjects[0].transform.position.y, groundPlaneBoundary[counter % groundPlaneBoundary.Length].y);
            //if (fps % 60 == 0)
            //{
            //    counter++;
            //}
        //}
        
    }


    private void FixedUpdate()
    {
        if (boundingbox)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    ARPlane selectedPlane = hit.transform.GetComponent<ARPlane>();

                    if (selectedPlane.GetInstanceID() == GroundPlane.GetInstanceID() || selectedPlane.GetInstanceID() == ElevatedPlane.GetInstanceID())
                    {
                        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
                        if (!box)
                        {
                            Instantiate(Cube, hit.point, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    public void ufo_pressed()
    {
        GameObject new_ufo = Instantiate(ufo_prefab);
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)));

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var ScreenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            ARPlane targetPlane = hit.transform.GetComponent<ARPlane>();
            if (targetPlane.GetInstanceID() == GroundPlane.GetInstanceID())
            {
                new_ufo.transform.position = new Vector3(hit.point.x,GroundPlane.transform.position.y+0.399f,hit.point.z);
            }
            if (targetPlane.GetInstanceID() == ElevatedPlane.GetInstanceID())
            {
                new_ufo.transform.position = new Vector3(hit.point.x, ElevatedPlane.transform.position.y + 0.399f, hit.point.z);
            }
        }

            //new_ufo.transform.position = ;
    }

    public void windmill_pressed()
    {

        if (!GameObject.FindGameObjectWithTag("Windmill-Preset"))
        {
            GameObject new_windmill = Instantiate(windmill_prefab);
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                ARPlane targetPlane = hit.transform.GetComponent<ARPlane>();
                if (targetPlane.GetInstanceID() == GroundPlane.GetInstanceID())
                {
                    new_windmill.transform.position = new Vector3(hit.point.x, GroundPlane.transform.position.y, hit.point.z + 2);
                }
                if (targetPlane.GetInstanceID() == ElevatedPlane.GetInstanceID())
                {
                    new_windmill.transform.position = new Vector3(hit.point.x, ElevatedPlane.transform.position.y, hit.point.z + 2);
                }
            }

            GameObject canvas = GameObject.Find("uiCanvas_b2-controls");

            canvas.GetComponent<Canvas>().enabled = true;

            /*var hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)), hits))
            {
                var hitPose = hits[0].pose;
                Instantiate(windmill_prefab, hitPose.position, hitPose.rotation);
            }*/
        }
    }

    public void up_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.position = new Vector3(windmill.transform.position.x, windmill.transform.position.y, windmill.transform.position.z+0.2f);
    }

    public void down_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.position = new Vector3(windmill.transform.position.x, windmill.transform.position.y, windmill.transform.position.z- 0.2f);
    }

    public void right_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.position = new Vector3(windmill.transform.position.x+ 0.2f, windmill.transform.position.y, windmill.transform.position.z);
    }

    public void left_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.position = new Vector3(windmill.transform.position.x - 0.2f, windmill.transform.position.y, windmill.transform.position.z);
    }

    public void rotate_left_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.Rotate(0f,10f,0f);
    }

    public void rotate_right_pressed()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.transform.Rotate(0f, -10f, 0f);
    }

    public void press_edit_done()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("Windmill-Preset");
        windmill.tag = "Windmill-Set";
        GameObject canvas = GameObject.Find("uiCanvas_b2-controls");

        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void press_play()
    {
        GameObject canvas_controls = GameObject.Find("uiCanvas_b2");
        GameObject canvas = GameObject.Find("uiCanvas_b2-controls");

        canvas.SetActive(false);
        canvas_controls.SetActive(false);

        GameObject startflag = GameObject.Find("StartPole");

        GameObject instantiatedObject = Instantiate(golfball_prefab, startflag.transform.position + new Vector3(0,0.5f,0), startflag.transform.rotation);
        //instantiatedObject.transform.parent = ElevatedPlane.transform;

        startflag.SetActive(false);
    }


    public void b1_rotate_left()
    {
        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
        box.transform.Rotate(0f, 10f, 0f);
    }

    public void b1_rotate_right()
    {
        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
        box.transform.Rotate(0f, -10f, 0f);
    }

    public void scale_up()
    {
        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
        box.transform.localScale = box.transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void scale_down()
    {
        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
        box.transform.localScale = box.transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void b1_edit_done()
    {
        GameObject box = GameObject.FindGameObjectWithTag("boundingbox");
        box.tag = "Untagged";
        box.name = "lol";
        Color color = new Color(0.1f, 0.1f, 0.1f, 0.001f);
        box.GetComponent<Renderer>().material.color = color;

    }

    public void b1_nextstep()
    {
        boundingbox = false;
        GameObject b1canvas = GameObject.Find("uiCanvas_b1");
        b1canvas.SetActive(false);
        GameObject canvas = GameObject.Find("uiCanvas_b2");
        canvas.GetComponent<Canvas>().enabled = true;
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("boundingbox");
        foreach(GameObject box in boxes)
        {
            box.SetActive(false);
        }
    }

}
