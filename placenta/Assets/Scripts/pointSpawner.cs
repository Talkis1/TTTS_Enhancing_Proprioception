using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using System;
using HapticGUI;

public class SpawnSphereOnClick : MonoBehaviour
{
    public float sphereRadius = 0.5f; // Radius of the sphere to be created
    public int sphereCount = 1; // Number of spheres created

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0)) //needs to be changed to be the when the button on the haptic device is pressed
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition; //needs to be changed to be the position of the haptic collider

            // Convert the mouse position to a ray
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Create a RaycastHit variable to store information about the hit
            RaycastHit hit;

            // Check if the ray hits something in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Create a sphere at the hit point
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphereCount++;
                sphere.name = sphereCount.ToString();
                sphere.GetComponent<Renderer>().material.color = Color.red;
                sphere.transform.position = hit.point;
                sphere.transform.localScale = Vector3.one * sphereRadius * 2; // Multiply by 2 because the scale is in diameter
                sphere.tag = "Waypoint";

            }
        }
    }
}