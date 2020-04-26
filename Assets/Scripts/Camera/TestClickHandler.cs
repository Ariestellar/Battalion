using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class TestClickHandler : MonoBehaviour
{
    private GameObject _ray;
    private Camera _camera;    

    private void Start()
    {        
        _camera = GetComponent<Camera>();
        _ray = CreateRayLabel();              
    }

    private void Update()
    {
        var camera = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(camera, Vector2.zero, 100);
        _ray.transform.position = hit.centroid;
        if (Input.GetMouseButtonDown(0))
        {                       
            Debug.Log(hit.collider.gameObject);
        }        
    }

    private GameObject CreateRayLabel()
    {
        GameObject ray = new GameObject("RayLabel");        
        ray.AddComponent<SpriteRenderer>();
        ray.GetComponent<SpriteRenderer>().sprite = Resources.FindObjectsOfTypeAll<Sprite>()[0];
        ray.GetComponent<SpriteRenderer>().color = Color.red;
        ray.GetComponent<SpriteRenderer>().sortingOrder = 5;
        return ray;
    }
}
