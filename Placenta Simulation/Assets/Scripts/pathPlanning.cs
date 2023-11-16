using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pathPlanning : MonoBehaviour
{
    // Apply these values in the editor
    private LineRenderer lineRenderer;
    private GameObject[] gameObjects;
    //[SerializeField] private Transform[] waypointTransforms; // Create array of transforms
    private Transform[] waypointTransforms;
    int numWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        // Create lineRenderer object
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get GameObjects with Waypoint tag
        gameObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        numWaypoints = gameObjects.Length;

        // Sort waypoints in ascending order
        var sortedGameObjects = gameObjects.OrderBy(go => go.name, new AlphanumericComparer()).ToArray();
        gameObjects = sortedGameObjects;

        //// Print waypoint name
        //for (int i = 0; i < numWaypoints; i++)
        //{
        //    Debug.Log(gameObjects[i].name);
        //}

        // Get transforms of gameobjects and put into a Transform list
        waypointTransforms = gameObjects.Select(go => go.transform).ToArray();

        //for (int i = 0; i < numWaypoints; i++)
        //{
        //    lineRenderer.SetPosition(i, waypointTransforms[i].position);
        //}

        // Set how many positions you want and create path between the points
        lineRenderer.positionCount = waypointTransforms.Length;
        for (int i = 0; i < waypointTransforms.Length; i++)
        {
            lineRenderer.SetPosition(i, waypointTransforms[i].position);
        }
    }

    // Custom comparer to sort strings in a numerical order
    public class AlphanumericComparer: IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // Parse strings into integers if they represent numbers
            if(int.TryParse(x, out int a) && int.TryParse(y, out int b))
            {
                return a.CompareTo(b);
            }

            // If not numbers, use default string comparison
            return string.Compare(x, y, System.StringComparison.Ordinal);
        }
    }
}
