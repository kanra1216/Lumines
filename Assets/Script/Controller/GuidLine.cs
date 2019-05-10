using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ガイドラインを設定するクラス
/// </summary>
public class GuidLine : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = 5.8f;
        transform.position = pos;

    }
}
