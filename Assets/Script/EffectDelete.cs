using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクトを削除するクラス
/// </summary>
public class EffectDelete : MonoBehaviour
{
    private float delete;
    // Start is called before the first frame update
    void Start()
    {
        delete = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        delete -= 1f;
        if (delete < 1)
        {
            Destroy(gameObject);
        }
    }
}
