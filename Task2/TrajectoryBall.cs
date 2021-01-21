using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryBall : MonoBehaviour
{
    [SerializeField] Vector3 velocity = new Vector3(5f,10f,0f);
    [SerializeField] Vector3 gravity = new Vector3 (0f,-9.8f,0f);
    [SerializeField] Vector3 gravity2 = new Vector3 (0f,-0.2f,0f);
    [SerializeField] float e = 0.8f;
    [SerializeField] float frictionFactor = 2f;
    Vector3 direction;
    Vector3 deccelaration;
    bool deccelarate = false;

    void Update()
    {

        // Apply Gravity
        if(this.transform.position.y > 0)
            velocity = VectorLibrary.addVectors(velocity, VectorLibrary.getScalarMultiple(gravity, Time.deltaTime));

        if (deccelarate)
        {
            // Get direction
            if (!(Mathf.Abs(velocity.x) < 0.1f && Mathf.Abs(velocity.z) < 0.1f))
            {
                direction = VectorLibrary.getUnitDirection(velocity, VectorLibrary.zeroVector());
            }
            // Apply deccelaration 
            deccelaration = VectorLibrary.getScalarMultiple(-direction, frictionFactor);

            // Calculate new Velocity: v = v_old + a * deltaT 
            velocity = VectorLibrary.addVectors(velocity, VectorLibrary.getScalarMultiple(-deccelaration, Time.deltaTime));
            
        }
        // p = p_old + vel * deltaT
        Vector3 velTimesdeltaT = VectorLibrary.getScalarMultiple(velocity, Time.deltaTime);
        this.transform.position = VectorLibrary.addVectors(this.transform.position, velTimesdeltaT);

        // When ground is hit
        if (this.transform.position.y < 0)
        {
            if(velocity.y < 0)
                // H = e*h -1 So it goes up again
                velocity.y = velocity.y * e * -1;

            if (velocity.y < 0.1f)
            {
                velocity = new Vector3(velocity.x, 0, 0);
                deccelarate = true;
            }
            if (Mathf.Abs(velocity.x) < 0.1f && Mathf.Abs(velocity.z) < 0.1f)
                velocity = new Vector3(0.0f, 0.0f, 0.00f);
        }
    }
}
