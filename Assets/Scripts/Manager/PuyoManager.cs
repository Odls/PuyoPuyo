using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoManager : ManagerBase<PuyoManager>
{
    [SerializeField] List<PuyoData> puyoDatas = new List<PuyoData>();
    
    Dictionary<E_PUYO_TYPE ,PuyoData> puyoDataDict = new Dictionary<E_PUYO_TYPE, PuyoData>();

    protected override void Awake()
    {
        base.Awake();

        foreach(var _data in puyoDatas)
        {
            puyoDataDict.Add(_data.type, _data);
        }
    }

    public PuyoData GetData(E_PUYO_TYPE p_type)
    {
        PuyoData _data;
        if(puyoDataDict.TryGetValue(p_type, out _data))
        {
            return _data;
        }
        else
        {
            Debug.LogError("No " + p_type + " In puyoDataDict");
            return null;
        }
    }

	public E_PUYO_TYPE GetRandomType() {
		return (E_PUYO_TYPE)Random.Range(0, (int)E_PUYO_TYPE.Len);
	}
}
