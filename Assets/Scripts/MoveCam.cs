using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveCam : MonoBehaviour
{
    private Transform cam;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
