using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string _template;

    public void SetTextDamage(int damage)
    {
        _text.CrossFadeAlpha(1, 0, false);        
        _text.text = string.Format(_template, damage);
        _text.CrossFadeAlpha(0, 1.5f, false);
    }
}
