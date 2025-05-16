using UnityEngine;
using UnityEngine.AI;

namespace RobotController{
    public class Robot : MonoBehaviour
    {
        [SerializeField] Transform target;

        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
        }

        void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}
