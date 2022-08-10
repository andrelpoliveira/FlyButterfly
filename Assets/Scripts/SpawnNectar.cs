using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNectar : MonoBehaviour
{
    public GameObject nectar;
    public float height;
    public float maxTime = 1f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newPipe = Instantiate(nectar);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newNectar = Instantiate(nectar);
            newNectar.transform.position = transform.position + new Vector3(0, Random.Range(-height, height));
            Destroy(newNectar,20f);
            timer = 0f;
        }
        
        timer += Time.deltaTime;
    }

   
}
