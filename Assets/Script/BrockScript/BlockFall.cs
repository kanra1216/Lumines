using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの高速落下と着地を行うクラス
/// </summary>
public class BlockFall : MonoBehaviour
{
    private GameController gameController;

    private BlockController blockController;

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
    private TouchController touchController;
#endif
     public enum FallState
    {
        ///<summary>
        ///自然落下
        ///</summary>
        FREEFALL,
        ///<summary>
        ///高速落下
        ///</summary>
        FAST_FALL,
        ///<summary>
        ///着地
        ///</summary>
        LANDING,
        ///<summary>
        ///停止
        ///</summary>
        STOP,  
    }
    public FallState fallState = FallState.FREEFALL;

    /// <summary>
    /// 自然落下時間の計測用
    /// </summary>
    private float freefallTime = 0f;
    /// <summary>
    /// 高速落下時間の計測用
    /// </summary>
    private float fastFallTime = 0f;

    /// <summary>
    ///　ステージ毎の自然落下時間
    /// </summary>
    readonly float[] FREEFALL_TIME =
    {
        2f,2f
    };
    /// <summary>
    /// 高速落下時間
    /// </summary>
    const float FAST_FALL_TIME = 0.001f;
    ///<summary>
    ///着地時間
    ///</summary>
    //const float LANDING_TIME = 30f;

    /// <summary>
    /// オフセット用
    /// </summary>
    readonly Vector3 COL = new Vector3(0f, 1f);

    void Awake()
    {
        blockController = GetComponent<BlockController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        touchController = GameObject.Find("TouchController").GetComponent<TouchController>();
        touchController.fastFall = false;
#endif
    }
    // Update is called once per frame
    void Update()
    {
        switch (fallState)
        {
            #region 自然落下
            case FallState.FREEFALL:
                //自然落下時間を計測
                freefallTime += UnityEngine.Time.deltaTime;

                //自然落下時間になったら
                if(freefallTime >= FREEFALL_TIME[gameController.stage])
                {
                    //自然落下時間をリセット
                    freefallTime = 0f;

                    //下移動
                    gameObject.transform.position -= COL;
                    //移動先の中身が空ではなかったら
                    if (!blockController.BlockFieldCheck())
                    {
                        //上移動で戻す
                        gameObject.transform.position += COL;
                    }
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //状態を高速落下に
                    fallState = FallState.FAST_FALL;
                }
                break;
            #endregion

            #region 高速落下
            case FallState.FAST_FALL:
                //高速落下
                FastFall();

                break;
            #endregion

            #region 着地
            case FallState.LANDING:
                //配列に格納
                blockController.ArrayStore();

                //状態を停止に
                fallState = FallState.STOP;
                gameController.Create();
                break;
            #endregion

            case FallState.STOP:

                break;
        }

        //着地
        UpdateLanding();

        //状態がゲーム中では無かったら
        if(gameController.gameState != GameController.GameState.PLAY)
        {
            //状態を停止に
            fallState = FallState.STOP;
        }

#if UNITY_EDITOR||UNITY_IOS||UNITY_ANDROID
        //状態が落下中だったら
        if(fallState==FallState.FREEFALL || fallState == FallState.FAST_FALL)
        {
            if (touchController.fastFall)
            {
                //高速落下
                FastFall();

                touchController.nextFlick = false;
            }
        }
#endif
    }

    ///<summary>
    ///高速落下
    ///</summary>
    void FastFall()
    {
        //高速落下時間を計測
        fastFallTime += UnityEngine.Time.deltaTime;

        //高速落下時間になったら
        if (fastFallTime >= FREEFALL_TIME[gameController.stage] * FAST_FALL_TIME)
        {
            //高速落下時間をリセット
            fastFallTime = 0f;

            //下移動
            gameObject.transform.position -= COL;

            //移動先の中身が空では無かったら
            if (!blockController.BlockFieldCheck())
            {
                //上移動で戻す
                gameObject.transform.position -= COL;
            }
        }
    }

    ///<summary>
    ///着地
    ///</summary>
    void UpdateLanding()
    {
        //落下中
        if (fallState == FallState.FREEFALL || fallState == FallState.FAST_FALL)
        {
            //下の中身が空では無かったら
            if (!blockController.LandingFieldCheck())
            {
                fallState = FallState.LANDING;
            }
        }
    }
}
