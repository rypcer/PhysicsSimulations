using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cueBall : MonoBehaviour
{
    [SerializeField] Vector3 deccelaration;
    [SerializeField] Vector3 velocity = new Vector3(0.0001f, 0.0f, 0.0001f);
    [SerializeField] Vector3 direction;
    [SerializeField] float frictionFactor = 0.3f;
    [SerializeField] cueBall[] balls;
    [SerializeField] Transform[] borders;
    Vector3 position;
    float ballRadius = 0.5f;
    float overlapDistance;
    float mass = 1;
    Vector3 LineStartPoint;
    Vector3 LineEndPoint;
    int ballIndex = 0;


    void Start()
    {
        // Set inital position and velocity
        position = this.transform.position;

    }


    void FixedUpdate()
    {
      
        direction = VectorLibrary.getUnitDirection(velocity, VectorLibrary.zeroVector());

        // Apply deccelaration 
        deccelaration = VectorLibrary.getScalarMultiple(-direction, frictionFactor);

        // Calculate new Velocity: v = v_old + a * deltaT 
        velocity = VectorLibrary.addVectors(velocity, VectorLibrary.getScalarMultiple(-deccelaration, Time.deltaTime));

        // Calculate new Position: p = p_old + v * deltaT
        position = VectorLibrary.addVectors(this.transform.position, VectorLibrary.getScalarMultiple(velocity, Time.deltaTime));

        // Bounce Back the Cue Ball from boundaries
        boundaryCollision();

        // Ball Collisions
        ballCollision();

        // Assign new position to the current object's position
        this.transform.position = position;

    }

    void ballCollision()
    {
        // Balls can stick to each other as I haven't implemented to reset position by the overlap amount
        foreach (cueBall ball in balls)
        {
            // Skip if ball is itself
            if (ball == this)
                continue;

            if (VectorLibrary.circleCollision(position, ball.position, ballRadius))
            {
                Vector3[] velocities = VectorLibrary.getCollidedVelocityBall(velocity, position, ball.velocity, ball.position, mass, ball.mass);
                // Assign new final velocities after collision
                this.velocity = velocities[0];
                ball.velocity = velocities[1];
            }
        }
    }


    void boundaryCollision()
    {

        // Border Line points
        ballIndex = 0;
        LineStartPoint = new Vector3(borders[ballIndex].position.x - ballRadius, 0, borders[ballIndex].position.z );
        LineEndPoint = new Vector3(borders[ballIndex].position.x - ballRadius, 0, borders[ballIndex].position.z - (borders[ballIndex].position.z * 2) );


        // Vertical X axis positive
        if (VectorLibrary.isPointOnLine(position, LineStartPoint, LineEndPoint))
        {
            // Reflect Velocity
            velocity = VectorLibrary.getVectorReflection(velocity, true);
            overlapDistance = Mathf.Abs(VectorLibrary.getScalarDistance(position.x + ballRadius, borders[ballIndex].position.x));

            // Transposes ball back if it goes outside the boundary
            position.x -= overlapDistance;
        }

        ballIndex = 1;
        LineStartPoint = new Vector3(borders[ballIndex].position.x + ballRadius, 0, borders[ballIndex].position.z);
        LineEndPoint = new Vector3(borders[ballIndex].position.x + ballRadius, 0, borders[ballIndex].position.z - (borders[ballIndex].position.z * 2));
        
        // Vertical X axis negative
        if (VectorLibrary.isPointOnLine(position, LineStartPoint, LineEndPoint))
        {
            velocity = VectorLibrary.getVectorReflection(velocity, true);
            overlapDistance = Mathf.Abs(VectorLibrary.getScalarDistance(position.x - ballRadius, borders[ballIndex].position.x));
            position.x += overlapDistance;
        }


        ballIndex = 2;
        LineStartPoint = new Vector3(borders[ballIndex].position.x , 0, borders[ballIndex].position.z - ballRadius);
        LineEndPoint = new Vector3(borders[ballIndex].position.x - (borders[ballIndex].position.x * 2), 0, borders[ballIndex].position.z - ballRadius);

        // Horizontal Z axis positive
        if (VectorLibrary.isPointOnLine(position, LineStartPoint, LineEndPoint))
        {
            velocity = VectorLibrary.getVectorReflection(velocity, false);
            overlapDistance = Mathf.Abs(VectorLibrary.getScalarDistance(position.z + ballRadius, borders[ballIndex].position.z));
            position.z -= overlapDistance;
        }

        ballIndex = 3;
        LineStartPoint = new Vector3(borders[ballIndex].position.x, 0, borders[ballIndex].position.z + ballRadius);
        LineEndPoint = new Vector3(borders[ballIndex].position.x - (borders[ballIndex].position.x * 2), 0, borders[ballIndex].position.z + ballRadius);
        
        // Horizontal Z axis negative
        if (VectorLibrary.isPointOnLine(position, LineStartPoint, LineEndPoint))
        {
            velocity = VectorLibrary.getVectorReflection(velocity, false);
            overlapDistance = Mathf.Abs(VectorLibrary.getScalarDistance(position.z - ballRadius, borders[3].position.z));
            position.z += overlapDistance;
        }

    }

}
