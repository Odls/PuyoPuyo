using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PuyoData : ScriptableObject
{
    [SerializeField] E_TYPE mType;
    public E_TYPE type { get => mType; }

    [SerializeField] Sprite mMainSprite;
    public Sprite mainSprite { get => mMainSprite;}
}
