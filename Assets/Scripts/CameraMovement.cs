using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{ 
    [SerializeField] private float _speed = 1;

    private Camera _camera;
    private Vector3 _startPosition;
    private Vector3 _targetPos;

    private void Awake()
    {
        _camera = GetComponent<Camera>();        
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x - _startPosition.x,
                _camera.ScreenToWorldPoint(Input.mousePosition).y - _startPosition.y,
                transform.position.z);            
            _targetPos = new Vector3 (Mathf.Clamp(transform.position.x - currentPosition.x, -3f, 3f),
                Mathf.Clamp(transform.position.y - currentPosition.y, -3.5f, 3.5f), transform.position.z);            
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _targetPos.x, _speed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, _targetPos.y, _speed * Time.deltaTime), transform.position.z);        
    }    
}