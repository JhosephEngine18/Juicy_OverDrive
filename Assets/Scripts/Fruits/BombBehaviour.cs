using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private SphereCollider ExplosionRadius;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("car")) 
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(Explosion());
        
        }
    }

    IEnumerator Explosion() 
    {
        yield return new WaitForSeconds(5f);
        ExplosionRadius.enabled = true;
        yield return null;
    }
}
