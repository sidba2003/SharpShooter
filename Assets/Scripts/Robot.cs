using UnityEngine;
using UnityEngine.AI;

namespace RobotController{
    public class Robot : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Animator animator;

        NavMeshAgent agent;
        EnemyHealth enemyHealth;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }

        void Update()
        {
            if (target == null) return;

            agent.SetDestination(target.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {   
                enemyHealth.SelfDestruct();
            }
        }
    }
}
