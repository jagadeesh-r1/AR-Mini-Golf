using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneSelection : MonoBehaviour
{
    private List<ARPlane> planes = new List<ARPlane>();
    public List<ARPlane> selectedPlanes = new List<ARPlane>();
    public bool planeDetectionEnabled = true;
    private float groundheight;

    void Start()
    {
        ARPlaneManager planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (ARPlane plane in args.added)
        {
            planes.Add(plane);
        }
    }

    void Update()
    {
        if (planeDetectionEnabled && Input.GetMouseButtonDown(0) && selectedPlanes.Count != 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ARPlane selectedPlane = hit.transform.GetComponent<ARPlane>();
                

                if (selectedPlane != null)
                {
                    groundheight = selectedPlane.transform.position.y;
                    planes.Remove(selectedPlane);
                    if (selectedPlanes.Contains(selectedPlane))
                    {
                        // Deselect plane if it has already been selected
                        //selectedPlanes.Remove(selectedPlane);
                        selectedPlane.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        // Select plane
                        selectedPlanes.Add(selectedPlane);
                        selectedPlane.GetComponent<MeshRenderer>().material.color = Color.white;

                        // Disable plane detection and disable all other planes except the selected ones
                        if (selectedPlanes.Count == 2)
                        {
                            ARPlaneManager planeManager = GetComponent<ARPlaneManager>();
                            planeManager.enabled = false;

                            foreach (ARPlane plane in planes)
                            {
                                if (!selectedPlanes.Contains(plane))
                                {
                                    plane.gameObject.SetActive(false);
                                }
                            }

                            planeDetectionEnabled = false;
                        }
                    }
                }
            }
        }
        if (planeDetectionEnabled && selectedPlanes.Count == 1)
        {
            foreach (ARPlane plane in planes)
            {
                if (plane.transform.position.y < groundheight)
                { 
                    plane.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if (plane.transform.position.y > groundheight)
                {
                    plane.GetComponent<MeshRenderer>().material.color = Color.cyan;
                }
            }
        }
        if (planeDetectionEnabled == false)
        {
            foreach (ARPlane plane in planes)
            {
                if (!selectedPlanes.Contains(plane))
                {
                    plane.gameObject.SetActive(false);
                }
            }
            ARPlaneManager planeManager = GetComponent<ARPlaneManager>();
            planeManager.enabled = false;
        }
    }
}
