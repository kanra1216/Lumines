using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイムラインを制御するクラス
/// </summary>
public class TimeLineController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController=null;
    private TimeLineDecision timeLineDecision;
    private TimeLineMove timeLineMove;

    /// <summary>
    /// タイムライン
    /// </summary>
    [SerializeField]
    private GameObject timeLine;

    //オフセット用
    readonly Vector3 ROW = new Vector3(1f, 0f);

    void Awake()
    {
        timeLineDecision = GetComponent<TimeLineDecision>();
        timeLineMove = GetComponent<TimeLineMove>();
        LineMoveStart();
    }

    ///<summary>
    ///タイムラインの各機能を動作させる
    ///</summary>
    public void LineMoveStart()
    {
        timeLineMove.MoveStart();
        timeLineDecision.DecisionStart();
    }

    ///<summary>
    ///消去命令をゲームコントローラーに通知
    ///</summary>
    public void DeleteResult()
    {
        gameController.BlockDelete();
    }

    ///<summary>
    ///盤面リセット命令をゲームコントローラーに通知
    ///</summary>
    //public void DeleteReset()
    //{
    //    gameController.ResetDelete();
    //}

    /// <summary>
    /// ゲームオーバー時にタイムラインを画面外で停止
    /// </summary>
    public void TimeLineDelete()
    {
        timeLineMove.DeleteLine();
    }

}
