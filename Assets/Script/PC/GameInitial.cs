using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタンドアローンで実行ファイル作成時に画面サイズを合わせるクラス
/// </summary>
public class GameInitial : MonoBehaviour
{
#if UNITY_STANDALONE
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(480, 800, true);
    }
#endif
}
