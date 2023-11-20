using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using System;
using HapticGUI;
using System;
using System.IO;
// using Directory.GetCurrentDirectory.Assets.3DSystems.HapticsDirect.HapticScripts.HapticPlugin;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NewBehaviourScript : MonoBehaviour
{
    public float sphereRadius = 0.05f; // Radius of the sphere to be created
    public int sphereCount = 1; // Number of spheres created
    public LayerMask sphereLayer; // Layer mask for spheres
    public GameObject hoveredSphere; // Currently hovered sphere

    string currentDirectory = Directory.GetCurrentDirectory();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentDirectory);
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition; //needs to be changed to be the position of the haptic collider

        // Convert the mouse position to a ray
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Create a RaycastHit variable to store information about the hit
        RaycastHit hit;
        
        // Check if the left mouse button is pressed
        // if (HapticPlugin.LastButtons[0] == 0 && HapticPlugin.Buttons[0] == 1) //needs to be changed to be the when the button on the haptic device is pressed
        // if (HL_EVENT1_BUTTON_DOWN)
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the ray hits something in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Create a sphere at the hit point
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.name = sphereCount.ToString();
                sphere.GetComponent<Renderer>().material.color = Color.blue;
                sphere.transform.position = hit.point;
                sphere.transform.localScale = Vector3.one * sphereRadius * 2; // Multiply by 2 because the scale is in diameter
                sphere.tag = "Waypoint";
                sphereCount++;

            }
        }

        // Check if the middle mouse button is clicked
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            // Perform a raycast and check if it is a sphere
            if(Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Waypoint"))
            {
                Destroy(hit.collider.gameObject); // Destroy the sphere it hits
            }
        }
    }
}
