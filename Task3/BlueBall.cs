using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBall : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1;
    Vector3 centerPoint;
    public float centerRadius = 1.5f;
    public float centerAngle = 0;
    public float angularSpeed = 90f;
    int waypointID = 0;
    bool isRotating = false;
    Vector3 pos;
    public float angle = 0;

   
    void Update()
    {
        
        if (isTargetPositionReached(this.transform.position, waypoints[waypointID].position) && !isRotating)
        {
            isRotating = true;
        }

        // Get new CenterPoint for each update
        centerPoint = waypoints[waypointID].position;


        if (isRotating)
        {
            centerPoint = waypoints[waypointID].position;
            // Transpose the ball to world cooridnate center
            pos = VectorLibrary.subVectors(this.transform.position, centerPoint);

            //float yof = this.transform.position.y;

            // Convert to Polar Coordinates
            Vector3 polarcoord = VectorLibrary.convertToPolar(pos);
            float radius = polarcoord.x;
            float theta = polarcoord.z;

            // Creating a angle for 1 turn
            angle += speed * angularSpeed * Time.deltaTime;

            // Adding the same speed to obejcts angle
            theta += speed * angularSpeed * Time.deltaTime;

            // Check if 1 Turn has been reached
            if (angle >= 360f * Mathf.Deg2Rad)
            {
                isRotating = false;
                waypointID++;
                // If last waypoint reached reset to first
                if (waypointID >= waypoints.Length)
                {
                    waypointID = 0;
                }
                angle = 0;
            }
            //Converting back to cartesian Coordinates and also resetting y position
            Vector3 cartesiancoord = VectorLibrary.convertToCartesian(centerRadius, theta); // Using a Fixed radius, so distance from ball to waypoint is always the same
            
            // Tranposing it back to its original position
            pos = VectorLibrary.addVectors(cartesiancoord, centerPoint);

            //pos.y = yof;
            // Assigning p_old = p_new
            this.transform.position = pos;
        }

        else
        {
            // v = d * s (d is direction and s speed)
            Vector3 direction = VectorLibrary.getUnitDirection(this.transform.position, waypoints[waypointID].position);
            Vector3 velocity = VectorLibrary.getScalarMultiple(direction, speed);

            // p = p_old + vel * deltaT
            Vector3 velTimesdeltaT = VectorLibrary.getScalarMultiple(velocity, Time.deltaTime);
            this.transform.position = VectorLibrary.addVectors(this.transform.position, velTimesdeltaT);

        }
    }

    // Check if ball is near waypoint
    bool isTargetPositionReached(Vector3 vec, Vector3 vec2) {
        if (VectorLibrary.getMagnitude(VectorLibrary.subVectors(vec, vec2)) <= centerRadius)
            return true;
        else
            return false;
    }

    //if (isRotating)
    //{
    //    // Update Position with angle
    //    this.transform.position = getAngle(this.transform.position);
    //    // Update angle 
    //    centerAngle += Time.deltaTime * angularSpeed;
    //    if (centerAngle >= 360f * Mathf.Deg2Rad)
    //    {
    //        isRotating = false;
    //        centerAngle = 0;
    //    }

    //}

    //Vector3 getAngle(Vector3 vec)
    //{

    //    vec.x = centerRadius * Mathf.Cos(centerAngle) + centerPoint.x;
    //    vec.z = centerRadius * Mathf.Sin(centerAngle) + centerPoint.z;
    //    return vec;
    //} 
}
