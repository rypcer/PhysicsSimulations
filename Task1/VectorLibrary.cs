using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class VectorLibrary
{

    public static Vector3 zeroVector()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }
    public static Vector3 addVectors(Vector3 vec1, Vector3 vec2)
    {
        Vector3 result;
        result.x = vec1.x + vec2.x;
        result.y = vec1.y + vec2.y;
        result.z = vec1.z + vec2.z;
        return result;
    }
    public static Vector3 subVectors(Vector3 vec1, Vector3 vec2)
    {
        Vector3 result;
        result.x = vec1.x - vec2.x;
        result.y = vec1.y - vec2.y;
        result.z = vec1.z - vec2.z;
        return result;
    }

    public static float getMagnitude(Vector3 vec)
    {
        // Magnitude of a vector
        float mag = Mathf.Sqrt(Mathf.Pow(vec.x, 2) + Mathf.Pow(vec.y, 2) + Mathf.Pow(vec.z, 2));
        return mag;
    }
    ///<summary>
    /// Multiplies vector1 * vector2 
    ///</summary>
    public static float dotProduct(Vector3 vec1, Vector3 vec2)
    {
        float x = vec1.x * vec2.x;
        float y = vec1.y * vec2.y;
        float z = vec1.z * vec2.z;
        return x + y + z;
    }

    public static Vector3 crossProduct(Vector3 vec1, Vector3 vec2)
    {
        Vector3 result;
        result.x = (vec1.y * vec2.z) - (vec1.z * vec2.y);
        result.y = (vec1.z * vec2.x) - (vec1.x * vec2.z);
        result.z = (vec1.x * vec2.y) - (vec1.y * vec2.x);
        return result;
    }

    ///<summary>
    /// Gets the angle of two vectors
    ///</summary>
    public static float angleOfVectors(Vector3 a, Vector3 b)
    {
        
        float dotP = dotProduct(a, b);
        float mag = getMagnitude(a) * getMagnitude(b);
       // Debug.Log(dotP + " " + mag);
        return Mathf.Acos(dotP / mag);
    }

    public static Vector3 getScalarMultiple(Vector3 vec, float scalar)
    {
        vec.x *= scalar;
        vec.y *= scalar;
        vec.z *= scalar;
        return vec;
    }

    ///<summary>
    /// Gets unit direction from vector1 to vector2 
    ///</summary>

    public static Vector3 getUnitVector(Vector3 vec)
    {
        float mag = getMagnitude(vec);
        vec.x /= mag;
        vec.y /= mag;
        vec.z /= mag;
        return vec;
    }
    public static Vector3 getUnitDirection(Vector3 vec1, Vector3 vec2)
    {
        Vector3 result;
        //Distance between vectors 
        result = getUnitVector(subVectors(vec2, vec1));
        return result;
    }
    public static Vector3 convertToCartesian(float radius, float theta)
    {
        Vector3 result;
        result.x = radius * Mathf.Cos(theta); 
        result.y = 0;
        result.z = radius * Mathf.Sin(theta);
        return result;
    }
    /// <summary>
    /// Returns a Vector 3, x = radius, z = theta 
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vector3 convertToPolar(Vector3 vec)
    {
        float radius = getMagnitude(vec);
        float theta = Mathf.Atan2(vec.z,vec.x); // Atan2 calculates the quadrants if x or y is 0 or negatives
       
        return new Vector3(radius,0,theta);
    }
    public static Vector3 trajectory(Vector3 vel, float theta, float gravity)
    {
        Vector3 result;
        result.x = vel.x * Mathf.Cos(theta) * Time.deltaTime;
        result.y = vel.y * Mathf.Sin(theta) * Time.deltaTime - gravity * Mathf.Pow(Time.deltaTime,2)/2;
        result.z = 0;
        return result;
    }

    public static Vector3 reverseVector(Vector3 vec)
    {
        vec.x *= -1;
        vec.y *= -1;
        vec.z *= -1;
        return vec;
    }

    ///<summary>
    /// Returns Axis Aligned Vector Reflection for XZ plane, 
    /// True = x axis
    /// False = z axis
    ///</summary>
    public static Vector3 getVectorReflection(Vector3 vec, bool axis)
    {

        if (axis)
            vec.x *= -1;
        else
            vec.z *= -1;

        return vec;
    }

    public static bool isPointOnLine(Vector3 p, Vector3 l1, Vector3 l2)
    {
        // get distance from the point to the two ends of the line
        float d1 = getMagnitude(subVectors(p, l1));
        float d2 = getMagnitude(subVectors(p, l2));

        // Length of Line
        float lineLen = getMagnitude(subVectors(l1, l2));

        // Because the ball can never be 1:1 with the line position, 
        // we need a small range to detect the collision
        float LineWidth = 0.0002f;

        // When both distances from both ends are equal to the lines length
        // Collision DETECTED!
        if (d1 + d2 >= lineLen - LineWidth && d1 + d2 <= lineLen + LineWidth)
            return true;
        else
            return false;
    }

    public static float getScalarDistance(float num1, float num2)
    {
        return Mathf.Abs(Mathf.Abs(num1) - Mathf.Abs(num2));
    }


    public static bool circleCollision(Vector3 vec1, Vector3 vec2, float radius)
    {
        float distance = getMagnitude(subVectors(vec2, vec1));

        // As the balls have same radius I only pass one radius
        if (distance < radius * 2)
            return true;
        else
            return false;
    }
    /// <summary>
    /// Function that applies convervation of momentum & kinetic energy.
    /// </summary>
    /// <param name="vel1"> Velocity of 1st Ball</param>
    /// <param name="pos1"> Position of 2nd Ball</param>
    /// <param name="vel2"> Velocity of 1st Ball</param>
    /// <param name="pos2"> Position of 2nd Ball</param>
    /// <param name="m1"> Mass of 1st Ball</param>
    /// <param name="m2"> Mass of 2nd Ball</param>
    /// <returns> Array of Velocities </returns>
    /// 
    public static Vector3[] getCollidedVelocityBall(Vector3 vel1, Vector3 pos1, Vector3 vel2, Vector3 pos2, float m1, float m2)
    {
        // Unit Normal is the direction from one ball to another
        Vector3 unitNormal = getUnitDirection(pos1, pos2);
        Vector3 unitTangent = new Vector3(-unitNormal.z, unitNormal.y, unitNormal.x);

        // Project Velocity of current ball to unit & tangent vector
        float v1n = dotProduct(vel1, unitNormal);
        float v1t = dotProduct(vel1, unitTangent);
        // Project Velocity of red ball to unit & tangent vector
        float v2n = dotProduct(vel2, unitNormal);
        float v2t = dotProduct(vel2, unitTangent);

        /* Calculate the conservation of Momentum & Kinetic energy for the unit normal 
         * direction as this is where the collision happens so a change is created for the velocity.
         * For tangetial direction there is no force between the balls so we dont have to
         * calculate how much force goes to the perpendicular velocity. */

        // Formula first Ball: v1(m1-m2)+2*m2*v2 / m1+m2 
        float v1nC = (v1n * (m1 - m2) + 2 * m2 * v2n) / (m1 + m2);

        // Formula second Ball: v2(m2-m1)+2*m1*v1 / m1+m2
        float v2nC = (v2n * (m2 - m1) + 2 * m1 * v1n) / (m1 + m2);

        // Now I know how much force goes into each direction use the, 
        // unit vector and multiply it by the amounts.
        Vector3 v1nv = getScalarMultiple(unitNormal, v1nC);
        Vector3 v1tv = getScalarMultiple(unitTangent, v1t);
        Vector3 v2nv = getScalarMultiple(unitNormal, v2nC);
        Vector3 v2tv = getScalarMultiple(unitTangent, v2t);

        // Assign the final velocties to both balls v = vNormal + vTangential 
        vel1 = addVectors(v1nv, v1tv);
        vel2 = addVectors(v2nv, v2tv);

        Vector3[] velocities = {vel1, vel2};
        return velocities;

    }


    //public static Vector3 getPerpendicularXZ(Vector3 vec)
    //{
    //    return rotationMatrix(vec, 90, true, false, true);
    //}

    //public static Vector3 rotationMatrix(Vector3 vec, float angle,bool x,bool y, bool z)
    //{

    //    Vector3 result = new Vector3();

    //    float[,] rotM = new float [2,2];
    //    rotM [0,0] = Mathf.Cos(angle * Mathf.Deg2Rad);
    //    rotM [0,1] = -Mathf.Sin(angle * Mathf.Deg2Rad);
    //    rotM [1,0] = Mathf.Sin(angle * Mathf.Deg2Rad);
    //    rotM [1,1] = Mathf.Cos(angle * Mathf.Deg2Rad);

    //    if (x && z)
    //    {
    //        result.x = vec.x * rotM[0, 0] + vec.z * rotM[1, 0];
    //        result.y = vec.y;
    //        result.z = vec.x * rotM[0, 1] + vec.z * rotM[1, 1];

    //    }
    //    else if (x && y)
    //    {
    //        result.x = vec.x * rotM[0, 0] + vec.y * rotM[1, 0];
    //        result.y = vec.x * rotM[0, 1] + vec.y * rotM[1, 1];
    //        result.z = vec.z;
    //    }

    //    return result;

    //}
}


