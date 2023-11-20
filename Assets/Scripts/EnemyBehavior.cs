using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehavior : MonoBehaviour
{
    public Transform player, enemy;
    private int locationIndex = 0;
    private NavMeshAgent agent;
    public Transform patrolRoute1, patrolRoute2;
    public float percent = 0.5f;
    public List<Transform> locations;
    private int _lives = 3;
    private GameBehavior _gameManager;
    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }
    void Start()
    {
        player = GameObject.Find("Player").transform;
        enemy = GameObject.Find("Enemy").transform;
        agent = enemy.GetComponent<NavMeshAgent>();
        InitializePatrolRoute(patrolRoute1);
        MoveToNextPatrolLocation();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        if (locationIndex == 1)
            if (Random.value > percent)
            {
                percent += 0.1f;
                InitializePatrolRoute(patrolRoute1);
                //Debug.Log("1");
            }
            else
            {
                percent -= 0.1f;
                InitializePatrolRoute(patrolRoute2);
                //Debug.Log("2");
            }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    void InitializePatrolRoute(Transform patrolRoute)
    {
        locations.Clear();
        foreach (Transform child in patrolRoute)
            locations.Add(child);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            GetComponent<BoxCollider>().size = new Vector3(6f, 3.5f, 25f);
            GetComponent<SphereCollider>().radius = 12f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            GetComponent<BoxCollider>().size = new Vector3(3f, 3.5f, 25f);
            GetComponent<SphereCollider>().radius = 6f;
        }
    }
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
            MoveToNextPatrolLocation();       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet1(Clone)")
        {
            _gameManager.EHP -= 10;
            Debug.Log(_gameManager.EHP);
            Destroy(collision.gameObject, 0);
        }
        if (collision.gameObject.name == "Bullet2(Clone)")
        {
            _gameManager.EHP -= 20;
            Debug.Log(_gameManager.EHP);
            Destroy(collision.gameObject, 0);
        }
        if (_gameManager.EHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy dead!");
        }
    }
}
