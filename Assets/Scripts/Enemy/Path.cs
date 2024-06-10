using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

public class Path : MonoBehaviour
{
    // List of waypoints, the order of the waypoints is the order the enemy will follow
    public List<Transform> waypoints = new List<Transform>();
    [SerializeField]
    private bool alwaysDrawPath; //if true, the path will always be drawn in the editor
    [SerializeField]
    private bool drawAsLoop; //if true, the path will loop back to the first waypoint
    [SerializeField]
    private bool drawNumbers; //if true, the path will draw numbers on each waypoint in order of the path
    public Color debugColor = Color.white; //color of the path in the editor

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //if alwaysDrawPath is true, draw the path
        if (alwaysDrawPath)
        {
            DrawPath();
        }
    }

    private void DrawPath()
    {
            for (int i = 0; i < waypoints.Count; i++)
            {
                GUIStyle labelStyle = new GUIStyle();
                labelStyle.fontSize = 15;
                labelStyle.normal.textColor = debugColor;
                if (drawNumbers)
                {
                    //draw a label on each waypoint with the index of the waypoint
                    Handles.Label(waypoints[i].position, i.ToString(), labelStyle);
                }
                //draw a line between each waypoint
                if (i >= 1)
                {
                    Gizmos.color = debugColor;
                    Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
                    
                    //if we are at the last waypoint and drawAsLoop is true, draw a line back to the first waypoint
                    if (drawAsLoop)
                    {
                        Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
                    }
                }
            }
    }

    public void OnDrawGizmosSelected() //this method is called when the object is selected in the editor
    {
            if (alwaysDrawPath) //if alwaysDrawPath is true, we don't need to draw the path again, because it's already drawn in OnDrawGizmos
                return;
            else 
                DrawPath(); 
    }
#endif
}
