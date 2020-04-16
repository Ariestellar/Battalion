using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject _ray;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(camera, Vector2.zero, 100);
            _ray.transform.position = hit.centroid;
            Debug.Log(hit.collider.gameObject);
        }        
    }
}
