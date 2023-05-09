﻿/*
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
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviourElevated : MonoBehaviour
{
    public GameObject Child;
    public SelectElevatedPlane SelectElevatedPlane;
    public SelectGroundPlane SelectGroundPlane;

    public ARPlane CurrentPlane, CurrentPlane2;

    // Start is called before the first frame update
    private void Start()
    {
        //Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // TODO: Conduct a ray cast to position this object.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        SelectElevatedPlane.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);
        CurrentPlane = null;
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If you don't have a locked plane already...
            var lockedPlane = SelectElevatedPlane.LockedElevatedPlane;
            hit = lockedPlane == null
                // ... use the first hit in `hits`.
                ? hits[0]
                // Otherwise use the locked plane, if it's there.
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }
        if (hit.HasValue)
        {
            CurrentPlane = SelectElevatedPlane.PlaneManager.GetPlane(hit.Value.trackableId);
            // Move this reticle to the location of the hit.
            transform.position = hit.Value.pose.position;
        }



        if (WasTapped() && CurrentPlane != null)
        {
            SelectElevatedPlane.LockElevatedPlane(CurrentPlane);

            var screenCenter2 = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits2 = new List<ARRaycastHit>();
            SelectGroundPlane.RaycastManager.Raycast(screenCenter2, hits2, TrackableType.PlaneWithinBounds);
            CurrentPlane2 = null;
            ARRaycastHit? hit2 = null;
            if (hits2.Count > 0)
            {
                // If you don't have a locked plane already...
                var lockedPlane = SelectGroundPlane.LockedGroundPlane;
                hit2 = lockedPlane == null
                    // ... use the first hit in `hits`.
                    ? hits2[0]
                    // Otherwise use the locked plane, if it's there.
                    : hits2.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
            }
            if (hit2.HasValue)
            {
                CurrentPlane2 = SelectGroundPlane.PlaneManager.GetPlane(hit2.Value.trackableId);
                // Move this reticle to the location of the hit.
                transform.position = hit2.Value.pose.position;
            }

            if (WasTapped() && CurrentPlane2 != null)
            {
                SelectGroundPlane.LockGroundPlane(CurrentPlane2);
            }
        }

        //Child.SetActive(CurrentPlane != null);
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}