using UnityEngine;
using UnityEngine.UIElements;

public class Car_Suspension : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Car carStats;

    Ray ray;

    Vector3 origin;
    Vector3 direction;

    private Transform wheelTransform;
    private Rigidbody carRigidBody;


    private void Start()
    {
        carRigidBody = GetComponentInParent<Rigidbody>();
        wheelTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        ray = new Ray(origin, direction);
        origin = wheelTransform.position;
        direction = Vector3.down;
        carSuspension(ray, origin, direction, carStats.springForce, carStats.restDistance, carStats.springDamper, wheelTransform, carRigidBody);
    }
    void carSuspension(Ray ray, Vector3 rayOrigin, Vector3 rayDirection, float springForce, float restDistance, float springDamper, Transform tireTransform, Rigidbody rb)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin,direction, out hit, carStats.restDistance))
        {

            Debug.DrawRay(origin, direction);
            Vector3 springDir = wheelTransform.up;
            Vector3 tireVelocity = carRigidBody.GetPointVelocity(wheelTransform.position);
            float offset = restDistance - hit.distance;
            float velocity = Vector3.Dot(springDir, tireVelocity);
            float force = (offset * springForce) - (velocity * springDamper);

            carRigidBody.AddForceAtPosition(springDir * force, wheelTransform.position);


        }
    }
}
