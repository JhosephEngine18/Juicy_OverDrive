using UnityEngine;

[CreateAssetMenu(fileName = "Car",menuName ="Cars",order = 1)]
public class Car : ScriptableObject
{
    //La fuerza de la suspensi�n
    public float springForce = 5;
    //el punto en el que descansa la suspensi�n del carro
    public float restDistance = 1;
    //que tanta resistencia tiene la suspension al regresar a su punto de descanso
    public float springDamper = .65f;
    //La velocidad a la que las ruedas cambian de dirección
    public float turnSpeed = 15f;
    //
    public float minSpeed = 0f;
    public float maxSpeed = 20f;
    public float T = 0.0f;

    public float frontTireGrip = 0.50f;
    public float backTireGrip = 0.25f;
    public float tireMass = 1f;

}
