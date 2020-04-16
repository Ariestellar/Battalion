using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PanelMovement : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Display(bool value)
    {
        _animator.SetBool("isDisplay", value);
    }
}
