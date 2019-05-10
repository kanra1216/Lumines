using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイムラインの移動を制御するクラス
/// </summary>
public class TimeLineMove : MonoBehaviour
{
    [SerializeField]
    private ScoreController scoreController=null;
    private TimeLineController timeLineController;

    [SerializeField]
    private TextMesh squareNum = null;
    private int square = 0;

    ///<summary>
    ///判定列を同期させるための現在列
    ///</summary>
    private int decisionLine=0;


    public enum LineMoveState
    {
        /// <summary>
        /// ポーズ中
        /// </summary>
        POSE,
        ///<summary>
        ///無移動
        ///</summary>
        NONE,
        ///<summary>
        ///移動中
        ///</summary>
        NOW_MOVE,
        ///<summary>
        ///削除
        ///</summary>
        DELETE,
    }
    private LineMoveState lineMoveState;

    //オフセット用
    readonly Vector3 ROW = new Vector3(0.1f, 0f);
    readonly Vector3 BACK = new Vector3(19f, 0f);

    /// <summary>
    /// タイムラインのインターバル時間
    /// </summary>
    private float INTERVAL_TIME = 3;
    private float loopCount = 0;

    private int deleteBlock=0;

    void Awake()
    {
        timeLineController = GetComponent<TimeLineController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (lineMoveState)
        {
            #region ポーズ
            case LineMoveState.POSE:
                break;
            #endregion

            #region 無移動
            case LineMoveState.NONE:

                //時間が来たら初期化して再スタート
                loopCount += UnityEngine.Time.deltaTime;

                if (loopCount >= INTERVAL_TIME)
                {
                    square = 0;
                    deleteBlock = 0;
                    loopCount = 0;
                    lineMoveState = LineMoveState.NOW_MOVE;
                    //timeLineController.DeleteReset();
                }
                break;
            #endregion

            #region 移動中
            case LineMoveState.NOW_MOVE:
                //探索列を過ぎたら戻って停止
                if (this.transform.position.x >= 15)
                {
                    gameObject.transform.position-=BACK;
                    scoreController.AddScore(deleteBlock,square);
                    LineStop();
                }
                squareNum.text = square.ToString();
                LineMove();
                break;
            #endregion

            #region 削除
            case LineMoveState.DELETE:

                //画面外で停止
                gameObject.transform.position -= BACK;
                break;
            #endregion
        }
    }

    ///<summary>
    ///ライン移動の開始
    ///</summary>
    public void MoveStart()
    {
        lineMoveState = LineMoveState.NOW_MOVE;
    }

    ///<summary>
    ///ラインを削除
    ///</summary>
    public void DeleteLine()
    {
        lineMoveState = LineMoveState.DELETE;
    }

    ///<summary>
    ///ポーズ中
    ///</summary>
    public void Pose()
    {
        lineMoveState = LineMoveState.POSE;
    }

    ///<summary>
    ///ラインの移動
    ///</summary>
    void LineMove()
    {
        //右移動
        gameObject.transform.position += ROW;
    }

    ///<summary>
    ///ラインを停止させる
    ///</summary>
    void LineStop()
    {
        lineMoveState = LineMoveState.NONE;
    }

    ///<summary>
    ///座標と判定列の動機
    ///</summary>
    public int LineDecision()
    {
        decisionLine = (int)this.transform.position.x;

        return decisionLine;
    }

    ///<summary>
    ///消去ブロックの数を受け取る
    ///</summary>
    public void DeleteBlocks(int delete)
    {
        deleteBlock += delete;
    }

    ///<summary>
    ///消去された役の数を受け取る
    ///</summary>
    public void DeleteSquares(int deleteSquare)
    {
        square += deleteSquare;
    }
}