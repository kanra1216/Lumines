using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転方向を決定するクラス
/// </summary>
public class RotateSelect : MonoBehaviour
{
    [SerializeField]
    private TouchController touchController = null;

    [SerializeField]
    private GameObject flameRight = null;
    [SerializeField]
    private GameObject flameLeft = null;

    /// <summary>
    /// 右回転を設定
    /// </summary>
    public void SelectRight()
    {
        flameRight.SetActive(true);
        flameLeft.SetActive(false);
        touchController.rotationSetting = true;
    }

    /// <summary>
    /// 左回転を設定
    /// </summary>
    public void SelectLeft()
    {
        flameRight.SetActive(false);
        flameLeft.SetActive(true);
        touchController.rotationSetting = false;
    }
}
