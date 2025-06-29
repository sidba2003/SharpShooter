using UnityEngine;
using UnityEngine.AI;

namespace RobotController{
    public class Robot : MonoBehaviour
    {
        [SerializeField] Animator animator;

        NavMeshAgent agent;
        EnemyHealth enemyHealth;
        Transform target;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
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
