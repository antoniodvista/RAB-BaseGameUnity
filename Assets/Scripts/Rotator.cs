using UnityEngine;

public class Rotator : MonoBehaviour {

    //  called every frame
	void Update ()
    {
        //  rotates object at steady pace
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
