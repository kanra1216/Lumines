using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フリックの感度を設定するクラス
/// </summary>
public class SetFlick : MonoBehaviour
{
#if UNITY_ANDROID
    [SerializeField]
    private TouchController touchController = null;
#endif

    [SerializeField]
    private Slider slider = null;

    [SerializeField]
    private Text text = null;

    private void Update()
    {
        text.text = ((int)slider.value) + " ";
    }

    /// <summary>
    /// フリックの感度をを設定
    /// </summary>
    public void FlickPower(float power)
    {
#if UNITY_ANDROID
        touchController.MoveValue = power;
#endif
    }
}
