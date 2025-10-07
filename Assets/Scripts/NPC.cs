using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
        private NavMeshAgent m_Agent;
        float time = 5f;
        public GameObject[] _checkCheckpoints;
        private int index;
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }
    
        // Update is called once per frame
        void Update()
        {
            
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                m_Agent.destination = _checkCheckpoints[index].transform.position;
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoints"))
            {
                index++;
                Debug.Log("Found it");
                if (index >= _checkCheckpoints.Length)
                {
                    index = 0;
                }
            }
        }
}
