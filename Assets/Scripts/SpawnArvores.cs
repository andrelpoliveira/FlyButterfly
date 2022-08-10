using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArvores : MonoBehaviour
{
    public GameObject pipe;
    public float height;
    public float maxTime = 1f;

    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject newPipe = Instantiate(pipe);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime)
        {
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height));
            Destroy(newPipe, 40f);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
