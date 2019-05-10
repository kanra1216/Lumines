using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景の大きさをを端末ごとに合わせるクラス
/// </summary>
public class BackGround : MonoBehaviour
{
    void Start()
    {
        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // スケールを求める。
        Vector2 scale = max/4;

        // スケールを変更。
        transform.localScale = scale;
    }
}
