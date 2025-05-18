using UnityEngine;
using UnityEngine.AI;

namespace RobotController{
    public class Robot : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Animator animator;

        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}
