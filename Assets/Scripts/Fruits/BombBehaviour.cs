using System;
using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private SphereCollider ExplosionRadius;
    public bool exploded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("car")) 
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(Explosion());
        
        }
    }

    private void Update()
    {
        if (exploded)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Explosion() 
    {
        yield return new WaitForSeconds(5f);
        ExplosionRadius.enabled = true;
        yield return null;
    }
}
