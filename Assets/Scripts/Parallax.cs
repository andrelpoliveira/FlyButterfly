using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Renderer mesh_render;
    private Material current_material;

    private float offset;

    public float increment_offset;
    public float speed;
    public string sorting_layer;
    public int order_layer;


    // Start is called before the first frame update
    void Start()
    {
        mesh_render = GetComponent<MeshRenderer>();
        mesh_render.sortingLayerName = sorting_layer;
        mesh_render.sortingOrder = order_layer;
        current_material = mesh_render.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += increment_offset;
        current_material.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
    }
}
