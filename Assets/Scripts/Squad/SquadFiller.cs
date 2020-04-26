using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadFiller : MonoBehaviour
{
    private Transform _fillImage;

    public void Init(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
        _fillImage = GetComponent<Transform>();
    }
    
    public void SetFillBar(float fillAmount)
    {        
        fillAmount = Mathf.Clamp01(fillAmount);   
        this._fillImage.localScale = new Vector3(fillAmount,1,1);
    }
}
