using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアの追加をするクラス
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField]
    private ScoreController scoreController=null;
    [SerializeField]
    private Text text=null;

    void Update()
    {
        text.text = "SCORE:"+scoreController.score.ToString("D9");
    }
}
