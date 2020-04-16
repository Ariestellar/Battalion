using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraMovement))]
public class ClickHandler : MonoBehaviour
{    
    [SerializeField] private GameObject _selectedCurrentObject;
    [SerializeField] private GameObject _selectedBattalion; 

    private CameraMovement _cameraMovement;
    private Camera _camera;
    private Vector3 _touchPosition;

    public UnityAction NewSquadPositionSet;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraMovement = GetComponent<CameraMovement>();
    }

    private void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _touchPosition = new Vector3();                
            }
            else
            {
                _touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                OneTouch(_touchPosition);
            }          
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_touchPosition != new Vector3())
            {
                Vector3 directionSwipe = new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x - _touchPosition.x,
                   _camera.ScreenToWorldPoint(Input.mousePosition).y - _touchPosition.y,
                   transform.position.z);
                TouchAndSwipe(directionSwipe);
            }           
        }   
    }

    private void OneTouch(Vector3 touchPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector3.back);
        if (hit != false)
        {  
            _selectedCurrentObject = hit.collider.gameObject;
            Debug.Log(_selectedCurrentObject);
            if (_selectedCurrentObject.GetComponent<Squad>())
            {
                CheckSelectSquad();
            }
            else if (_selectedCurrentObject.name == "TacticalCardPaper")
            {
                if (_selectedBattalion != null)
                {
                    _selectedBattalion.GetComponent<Squad>().SquadData.TargetPosition = touchPosition;
                    NewSquadPositionSet?.Invoke();
                }                                                
            }
        }
    }

    private void TouchAndSwipe(Vector3 directionSwipe)
    {        
        if (_selectedCurrentObject != null)
        {
            if (_selectedCurrentObject.name == "TacticalCardPaper")
            {
                _cameraMovement.LaunchMove(directionSwipe);                
            }
        }
    }

    private void CheckSelectSquad()
    {
        if (_selectedBattalion == null)
        {
            _selectedCurrentObject.GetComponent<Squad>().Select(true);
            _selectedBattalion = _selectedCurrentObject;
        }
        else if (_selectedBattalion != null)
        {
            if (_selectedBattalion == _selectedCurrentObject)
            {
                _selectedCurrentObject.GetComponent<Squad>().Select(false);
                _selectedBattalion = null;
            } 
            else if (_selectedBattalion != _selectedCurrentObject) 
            {
                _selectedBattalion.GetComponent<Squad>().Select(false);
                _selectedCurrentObject.GetComponent<Squad>().Select(true);                
                _selectedBattalion = _selectedCurrentObject;
            }
        }        
    }
}
