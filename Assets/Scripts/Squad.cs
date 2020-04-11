using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private Animator panelSquad;    

    private void OnMouseUp() 
    {
        panelSquad.SetBool("isPanelSquad", true);
    }       
}
