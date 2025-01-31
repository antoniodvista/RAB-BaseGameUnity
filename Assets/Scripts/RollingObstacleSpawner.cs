using UnityEngine;

public class RollingObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;

    private void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, 7f);
    }

    private void SpawnObstacle()
    {
        Instantiate(obstacle, transform.position, Quaternion.identity);
    }
}
