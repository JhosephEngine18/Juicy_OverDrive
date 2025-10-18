using UnityEngine;
using UnityEngine.UIElements;

public class Car_Suspension : MonoBehaviour
{
    //Se hace referencia al scriptable object que esta almacenando las variables de nuestro carro
    [SerializeField] private Car carStats;

    //Dos Vector 3 que nos van a ayudar para determinar el origen y direccion de los 
    //raycasts que vamos a usar para nuestra suspensión
    Vector3 origin;
    Vector3 direction;

    //se entiende
    private Transform wheelTransform;
    private Rigidbody carRigidBody;


    private void Start()
    {
        carRigidBody = GetComponentInParent<Rigidbody>();
        wheelTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        //hacemos nuestro punto de origen la posicion del transform de cada rueda
        origin = wheelTransform.position;
        //La direccion siempre va a ser hacia abajo por lo que le pasamos un Vector3.down
        direction = Vector3.down;
        carSuspension(origin, direction, carStats.springForce, carStats.restDistance, carStats.springDamper, wheelTransform, carRigidBody);
    }
    void carSuspension(Vector3 rayOrigin, Vector3 rayDirection, float springForce, float restDistance, float springDamper, Transform tireTransform, Rigidbody rb)
    {
        //Variable RaycastHit, nos da el punto donde pegó el Raycast
        RaycastHit hit;
        //usamos el out hit, para que nuestro if tambien retorne el punto donde pegó el raycast.
        if (Physics.Raycast(origin,direction, out hit, carStats.restDistance))
        {
            //Dibujamos el rayo solo como un apoyo visual
            Debug.DrawRay(origin, direction);
            //Variable SpringDir simula un el "resorte" de la suspension, siempre va a ser hacia arriba
            Vector3 springDir = wheelTransform.up;
            //Obtenemos la velocidad de la llanta obteniendo el pointVelocity de la posicion del transform.
            Vector3 tireVelocity = carRigidBody.GetPointVelocity(wheelTransform.position);
            //Calculamos la diferencia entre el punto de descanso del Raycast y la distancia en donde pega el Raycast
            float offset = restDistance - hit.distance;
            //Obtenemos la velocidad con el Dot Product de la direccion de nuestra suspencion y la velocidad de nuestra rueda
            float velocity = Vector3.Dot(springDir, tireVelocity);
            //La fuerza que aplica la suspensión se calcula con la siguiente formula
            //(diferencia entre descanso y posicion actual * fuerza de resorte) - (Velocidad * Resistencia de la suspensión)
            float force = (offset * springForce) - (velocity * springDamper);//La variable SpringDamper hace que la springForce se aplique gradualmente.
            //finalmente añadimos fuerza a la direccion de la suspension en el wheeltransform.
            carRigidBody.AddForceAtPosition(springDir * force, wheelTransform.position);


        }
    }
}
