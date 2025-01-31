using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public float despawnTime = 5f;

    void Start()
    {
        transform.Rotate(90,0,0);
        Invoke("DestroyObject", despawnTime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
