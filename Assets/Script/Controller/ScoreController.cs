using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアとハイスコアを制御するクラス
/// </summary>
public class ScoreController : MonoBehaviour
{
    private GameController gameController;
    /// <summary>
    /// スコア
    /// </summary>
    public int score;
    /// <summary>
    /// ハイスコア
    /// </summary>
    public int highScore;
    public const string HIGH_SCORE_KEY = "HIGH_SCORE_KEY";
    /// <summary>
    /// 最大スコア
    /// </summary>
    const int MAX_SCORE = 999999;

    #region 消去時のスコア
    const int ONE_BROCK_POINT = 100;
    const int ONE_SQUARE_POINT = 400;
    #endregion

    #region ボーナス倍率
    const int THREE_SQUARE_BONUS = 3;
    const int FOUR_SQUARE_BONUS = 4;
    const int FIVE_SQUARE_BONUS = 5;
    //5以上は5の時と同様のボーナスとして処理する
    #endregion

    #region コンボ倍率
    private bool comboContinuation = false;
    private int combo;

    const int ONE_COMBO = 2;
    const int TWO_COMBO = 3;
    const int THREE_COMBO = 4;
    #endregion
    private int addScore=0;
    //private int bonus = 0;

    /// <summary>
    /// 全消し
    /// </summary>
    const int ALL_DELETE = 10000;

    /// <summary>
    /// エフェクト関係を取得
    /// </summary>
    [SerializeField]
    private GameObject bonusLine = null;
    [SerializeField]
    private GameObject bonus = null;
    [SerializeField]
    private GameObject superBonus = null;
    [SerializeField]
    private GameObject ultraBonus = null;
    [SerializeField]
    private GameObject comboLine = null;
    [SerializeField]
    private GameObject combo2 = null;
    [SerializeField]
    private GameObject combo3 = null;
    [SerializeField]
    private GameObject combo4 = null;

    void Awake()
    {
        // 初期化
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score > MAX_SCORE)
        {
            //スコアカンスト
            score = MAX_SCORE;
        }
    }

    ///<summary>
    ///スコア計算
    ///</summary>
    public void AddScore(int deleteBlockNum,int deleteSquareNum)
    {
        //追加スコアの初期化
        addScore = 0;
        comboContinuation = false;
        bonusLine.SetActive(false);
        comboLine.SetActive(false);

        //ブロックと役の計算
        addScore += deleteBlockNum * ONE_BROCK_POINT;
        addScore += deleteSquareNum * ONE_SQUARE_POINT;

        //役の個数でボーナスを計算
        if (deleteSquareNum == 3)
        {
            addScore = addScore * THREE_SQUARE_BONUS;
            comboContinuation = true;
            bonus.SetActive(false);
            bonus.SetActive(true);
            bonusLine.SetActive(true);
        }
        else if(deleteSquareNum == 4)
        {
            addScore = addScore * FOUR_SQUARE_BONUS;
            comboContinuation = true;
            superBonus.SetActive(false);
            superBonus.SetActive(true);
            bonusLine.SetActive(true);
        }
        else if(deleteSquareNum >= 5)
        {
            addScore = addScore * FIVE_SQUARE_BONUS;
            comboContinuation = true;
            ultraBonus.SetActive(false);
            ultraBonus.SetActive(true);
            bonusLine.SetActive(true);
        }

        //コンボしているかどうか判定
        if(comboContinuation == true)
        {
            combo++;
        }
        else
        {
            combo = 0;
        }

        //コンボが続いていればボーナスを加える
        if (combo == 2)
        {
            addScore = addScore * ONE_COMBO;
            combo2.SetActive(false);
            combo2.SetActive(true);
            comboLine.SetActive(true);
        }
        else if (combo == 3)
        {
            addScore = addScore * TWO_COMBO;
            combo3.SetActive(false);
            combo3.SetActive(true);
            comboLine.SetActive(true);
        }
        else if (combo>=4)
        {
            addScore = addScore * THREE_COMBO;
            combo4.SetActive(false);
            combo4.SetActive(true);
            comboLine.SetActive(true);
        }

        //計算したスコアを追加
        SetScore(addScore);
    }

    ///<summary>
    ///スコア加算
    ///</summary>
    void SetScore(int score)
    {
        this.score += score;
    }
}
