/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SelectElevatedPlane : MonoBehaviour
{
    public ARPlaneManager PlaneManager, PlaneManager2;
    public ARRaycastManager RaycastManager;
    public ARPlane LockedElevatedPlane;

    public void LockElevatedPlane(ARPlane keepPlane)
    {
        // Disable all planes except the one we want to keep
        var arPlane = keepPlane.GetComponent<ARPlane>();
        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != arPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        LockedElevatedPlane = arPlane;
        PlaneManager.planesChanged += DisableNewPlanes;
    }

    private void Start()
    {
        PlaneManager = GetComponent<ARPlaneManager>();
        PlaneManager2 = GetComponent<ARPlaneManager>();

        RaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {

        if (LockedElevatedPlane?.subsumedBy != null)
        {
            LockedElevatedPlane = LockedElevatedPlane.subsumedBy;
        }
    }

    private void DisableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        PlaneManager.planesChanged -= DisableNewPlanes;
    }
}
