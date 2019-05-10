using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイムラインの列判定を制御するクラス
/// </summary>
public class TimeLineDecision : MonoBehaviour
{
    [SerializeField]
    private GameController gameController=null;
    private TimeLineController timeLineController;
    private TimeLineMove timeLineMove;
    [SerializeField]
    private SoundController soundController = null;

    public enum LineDecisionState
    {
        ///<summary>
        ///探索
        ///</summary>
        SEARCH,
        ///<summary>
        ///消去実行の可否
        ///</summary>
        PROPRIETY,
        ///<summary>
        ///通知
        ///</summary>
        NOTIFICATION,
    }
    private LineDecisionState lineDecisionState=LineDecisionState.SEARCH;

    /// <summary>
    /// 消去が確定したブロックにかぶせるプレハブ
    /// </summary>
    [SerializeField]
    private GameObject DeleteAfterPrefab = null;

    /// <summary>
    /// 消去が確定したブロックの数
    /// </summary>
    private int deleteBlock;

    ///<summary>
    ///ラインごとに発見した消去ブロックの数
    ///</summary>
    private int delete;

    ///<summary>
    ///消去が確定した役の数
    ///</summary>
    private int deleteSquare;

    /// <summary>
    /// 消去ブロック数に応じたボーナス
    /// </summary>
    private int bonus;

    ///<summary>
    ///消去判定された明ブロックデータ
    ///</summary>
    public const int DELETE_LIGHT = 3;
    ///<summary>
    ///消去判定された暗ブロックデータ
    ///</summary>
    public const int DELETE_BRACK = 4;
    /// <summary>
    /// ペア成立の明ブロックデータ
    /// </summary>
    public const int SQUARE_LIGHTBLOCK = 10;
    /// <summary>
    /// ペア成立の暗ブロックデータ
    /// </summary>
    public const int SQUARE_BRACKBLOCK = 20;

    /// <summary>
    /// 探索する列
    /// </summary>
    private int seachLine;
    private int completionLine = 0;

    void Awake()
    {
        timeLineController = GetComponent<TimeLineController>();
        timeLineMove = GetComponent<TimeLineMove>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (lineDecisionState)
        {
            #region 探索
            case LineDecisionState.SEARCH:

                //探索範囲(0～14)以外でははしらない
                seachLine = timeLineMove.LineDecision();
                if (seachLine < 0 || seachLine >=14) break;

                if (seachLine != completionLine) break;
                completionLine += 1;
                delete = 0;

                //消去予定のブロックの優先度を変更
                for (int y = 0; y <GameController.FIELD_Y; y++)
                {
                    if (gameController.fields[seachLine, y] == DELETE_BRACK)
                    {
                        gameController.fields[seachLine,y] = 6;

                        //ブロックの表示を優先度に合わせて変更
                        Destroy(gameController.delBlockPrefabs[seachLine, y].gameObject);
                        gameController.delBlockPrefabs[seachLine, y] = Instantiate(DeleteAfterPrefab, new Vector3(seachLine, y, -7.2f), transform.rotation);

                        deleteBlock++;
                        delete++;
                    }
                    else if(gameController.fields[seachLine, y] == DELETE_LIGHT)
                    {
                        gameController.fields[seachLine, y] = 5;

                        //ブロックの表示を優先度に合わせて変更
                        Destroy(gameController.delBlockPrefabs[seachLine, y].gameObject);
                        gameController.delBlockPrefabs[seachLine, y] = Instantiate(DeleteAfterPrefab, new Vector3(seachLine, y, -7.2f), transform.rotation);

                        deleteBlock++;
                        delete++;
                    }

                    if(gameController.delBlocks[seachLine,y]==SQUARE_BRACKBLOCK
                        || gameController.delBlocks[seachLine, y] == SQUARE_LIGHTBLOCK)
                    {
                        deleteSquare++;
                    }
                }

                //探索ラインを戻す
                if (completionLine == 14)
                {
                    completionLine = 0;
                    delete = 0;
                }
                lineDecisionState = LineDecisionState.PROPRIETY;
                break;
            #endregion

            #region 消去実行の可否　
            case LineDecisionState.PROPRIETY:
                lineDecisionState = LineDecisionState.SEARCH;

                //ラインのブロックの数で命令を変更
                if (delete == 0)
                {
                    //ブロックを消去
                    lineDecisionState = LineDecisionState.NOTIFICATION;
                } 
                else if (delete > 0)
                {
                    //消去ブロックがあることを通知
                    soundController.TimeLineCheckSound();
                }
                break;
            #endregion

            #region 通知
            case LineDecisionState.NOTIFICATION:

                //初期化とブロックの消去を実行
                timeLineMove.DeleteBlocks(deleteBlock);
                timeLineMove.DeleteSquares(deleteSquare);
                deleteBlock = 0;
                deleteSquare = 0;
                timeLineController.DeleteResult();
                lineDecisionState = LineDecisionState.SEARCH;
                break;
            #endregion
        }
    }

    ///<summary>
    ///判定の開始
    ///</summary>
    public void DecisionStart()
    {
        lineDecisionState = LineDecisionState.SEARCH;
    }

}
