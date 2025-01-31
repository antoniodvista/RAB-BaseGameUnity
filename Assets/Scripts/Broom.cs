using UnityEngine;

public class Broom : MonoBehaviour
{
    private GameObject player;
    public float speed = 3f;
    bool sweepLeft = false;
    bool sweepRight = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("SweepRight", 0f, 4f);
        InvokeRepeating("SweepLeft", 2f, 4f);
    }

    private void FixedUpdate()
    {


        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(player.transform.position.x, transform.position.y, 
            player.transform.position.z), speed * Time.deltaTime);
        
        if (sweepRight)
        {
            transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime * 2);
        }

        if (sweepLeft)
        {
            transform.Rotate(new Vector3(0, 0, -15) * Time.deltaTime * 2);
        }
    }

    private void SweepRight()
    {
        sweepRight = true;
        sweepLeft = false;
    }

    private void SweepLeft()
    {
        sweepLeft = true;
        sweepRight = false;
    }
}