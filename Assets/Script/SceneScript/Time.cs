using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム画面のタイムを制御するクラス
/// </summary>
public class Time : MonoBehaviour
{
    [SerializeField]
    private GameController gameController=null;

    [SerializeField]
    private Pose pose = null;

    /// <summary>
    /// 時間を表示するテキスト
    /// </summary>
    [SerializeField]
    private Text time=null;

    /// <summary>
    /// 時間を設定する変数
    /// </summary>
    private int minute=0;
    private int second=0;
    private int milliSecond=0;

    ///<summary>
    ///時間計算用
    ///</summary>
    private int plusTime = 1;
    private int resetTime = 0;
    private int stepupTime=60;

    /// <summary>
    /// 時間の限界値
    /// </summary>
    const int MAX_TIME = 99;

    void Start()
    {
        time.text=("Time " +minute.ToString("D2") + ":"+second.ToString("D2") + ":"+milliSecond.ToString("D2"));
    }

    void Update()
    {
        if (pose.pose != true
            && gameController.gameState != GameController.GameState.GAMEOVER)
        {
            milliSecond += plusTime;
        }

        if (milliSecond >= stepupTime)
        {
            milliSecond = resetTime;
            second += plusTime;
            if (second >= 60)
            {
                second = resetTime;
                minute += plusTime;
            }
        }

        //99分以降は表示タイムを更新しない
        if (second != MAX_TIME)
        {
            time.text = ("Time " + minute.ToString("D2") + ":" + second.ToString("D2") + ":" + milliSecond.ToString("D2"));
        }
    }
}
