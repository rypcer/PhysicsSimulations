using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 a = new Vector3(1, 3, 2);
        Vector3 b = new Vector3(4, 7, 1);

        Vector3 point = new Vector3(2.66f,0f,2.66f);
        Vector3 start = new Vector3(2f,0f,2f);
        Vector3 end = new Vector3(12f,0f,12f);

        float scalarNum = 2;

        float radius = 5f;
        float theta = 2.214298f;
        Vector3 n = new Vector3(-3, 0f,4);


        Vector3 b1 = new Vector3(1,10,1);
        Vector3 b2 = new Vector3(1,20,1);


        Debug.Log("Vector 1: " + a + "  Vector 2: " + b +  "  Scalar Number: " + scalarNum);
        Debug.Log("Angle between 2 3D Vectors - Output: " + VectorLibrary.angleOfVectors(a, b) + "\nExpected Output: 0.47 radians");
        Debug.Log("3D Vector addition - Output: " + VectorLibrary.addVectors(a, b) + "\nExpected Output: (5, 10, 3)");
        Debug.Log("3D Vector subtraction - Output: " + VectorLibrary.subVectors(a, b) + "\nExpected Output: (-3, -4, 1)");
        Debug.Log("3D Dot Product Vector - Output: " + VectorLibrary.dotProduct(a, b) + "\nExpected Output: 27");
        Debug.Log("Unit vector of a 3D Vector - Output: " + VectorLibrary.getUnitVector(a) + "\nExpected Output: (0.3, 0.8 , 0.5)");

        Debug.Log("Vector reflection (axis aligned X) Output: " + VectorLibrary.getVectorReflection(point,true) + "\nExpected Output: (-2.66f,0f,2.66f) "); 
        Debug.Log("Vector reflection (axis aligned Z) Output: " + VectorLibrary.getVectorReflection(point,false) + "\nExpected Output: (2.66f,0f,-2.66f) ");

        Debug.Log("Polar to Cartesian - Output:  " + VectorLibrary.convertToCartesian(radius, theta) + "\nExpected Output: -3, 4");
        Debug.Log("Cartesian to Polar - Output:  " + VectorLibrary.convertToPolar(n).x+" "+VectorLibrary.convertToPolar(n).z + "\nExpected Output: 5 2.214 ");

        Debug.Log("Unit Direction Vector - Output: " + VectorLibrary.getUnitDirection(a,b) + "\nExpected Output: (0.6, 0.8, -0.2)");
        Debug.Log("Magnitude of a 3D Vector - Output: " + VectorLibrary.getMagnitude(a) + "\nExpected Output: 3.7416");
        Debug.Log("Scalar Multiple of a 3D Vector - Output: " + VectorLibrary.getScalarMultiple(a,scalarNum) + "\nExpected Output: 2,6,4");
        Debug.Log("Vectors nearly equal with radius - Output: " + VectorLibrary.circleCollision(b1,b2,radius) + "\nExpected Output: False");
        Debug.Log("3D zero Vector - Output: " + VectorLibrary.getScalarMultiple(a,scalarNum) + "\nExpected Output: (2,6,4)");
        Debug.Log("A point is on a Line - Output: " + VectorLibrary.isPointOnLine(point,start, end) + "\nExpected Output: True");


    }


}
