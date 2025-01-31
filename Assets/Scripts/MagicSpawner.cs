using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] GameObject magicSphere;

    [Header("Attributes")]
    [SerializeField] float rate;
    [SerializeField] int amount;

    private void Start()
    {
        InvokeRepeating("SpawnMagic", 2f, rate);
    }

    void SpawnMagic()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(magicSphere, transform.position, Quaternion.identity);
        }
    }

}
