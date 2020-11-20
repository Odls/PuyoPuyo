using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;

    public void SetType(E_TYPE p_type)
    {
        renderer.sprite = PuyoManager.instance.GetData(p_type).mainSprite;
    }
}
