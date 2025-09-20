using UnityEngine;

[CreateAssetMenu(fileName = "Car",menuName ="Cars",order = 1)]
public class Car : ScriptableObject
{
    //La fuerza de la suspensión
    public float springForce = 5;
    //el punto en el que descansa la suspensión del carro
    public float restDistance = 1;
    //que tanta resistencia tiene la suspension al regresar a su punto de descanso
    public float springDamper = .65f;
    public float turnSpeed = 10f;

}
