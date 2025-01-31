using UnityEngine;

public class Floating : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] float timer;
    [SerializeField] float sendTime;
    [SerializeField] float cooldown;
    float returnTime;

    [Header("Height")]
    [SerializeField] float maxHeight = -33;
    [SerializeField] float minHeight = -38;

    [Header("Speed")]
    [SerializeField] float speed = 3f;
    [SerializeField] float rotationSpeed = 5f;

    [Header("Extra")]
    [SerializeField] bool isRotating = false;
    

    private void Start()
    {
        sendTime = timer + sendTime;
        returnTime = sendTime + cooldown;
    }

    private void FixedUpdate()
    {
        timer = Time.time;

        //  platform moves up
        if (Time.time > sendTime)
        {
            MoveUp();
        }

        //  platform moves down
        if (Time.time > returnTime)
        {
            MoveDown();
        }

        if (isRotating)
        {
            gameObject.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.time);
        }
    }

    void MoveUp()
    {
        gameObject.transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);

        if (gameObject.transform.position.y > maxHeight)
        {
            sendTime += cooldown;
        }

    }

    void MoveDown()
    {
        gameObject.transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
        
        if (gameObject.transform.position.y < minHeight)
        {
            returnTime += cooldown;
        }
    }
}
