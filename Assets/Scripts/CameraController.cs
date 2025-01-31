using UnityEngine;

public class CameraController : MonoBehaviour 
{

    GameObject player;
    Vector3 offset;
	float mouseX;

    //  called on first frame
	void Start ()
    {
        player = GameObject.FindWithTag("Player");

        //  locks cursor and sets offset
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.transform.position;
	}

    //  called every frame
    private void Update()
    {
        //  gets mouse movement along x axis and rotates view
        mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX, 0);
    }

    private void FixedUpdate()
    {
        //  rotates player to follow looking direction
        transform.localScale = player.transform.localScale;
        player.transform.rotation = transform.rotation;
    }

    void LateUpdate ()
    {
        //  sets camera position based off offset from player
        transform.position = player.transform.position + offset;
	}
}