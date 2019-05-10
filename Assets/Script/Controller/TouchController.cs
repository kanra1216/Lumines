using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スマートフォンの制御をするクラス
/// </summary>
public class TouchController : MonoBehaviour
{
    ///<summary>
    ///タッチした反応
    ///</summary>
    private Touch touch;
    /// <summary>
    /// タッチした位置
    /// </summary>
    private  Vector2 startPos;
    ///<summary>
    ///タッチしている位置
    ///</summary>>
    private Vector2 betweenPos;
    ///<summary>
    ///タッチを離した位置
    ///</summary>>
    private Vector2 endPos;

#if UNITY_ANDROID
    ///<summary>
    ///移動量調整変数
    ///</summary>
    public float MoveValue = 5f;

    ///<summary>
    ///横移動するための移動量
    ///</summary>
    private float lateralMoveValue = 13f;

    ///<summary>
    ///高速落下するための移動量
    ///</summary>
    private float fastFallValue = 24f;

        [SerializeField]
    private Pose pose=null;
    
    ///<summary>
    ///タップしたかどうか
    ///</summary>
    private bool tap = true;
#endif

    ///<summary>
    ///フリックするための移動量
    ///</summary>
    public float flickValue;

    ///<summary>
    ///左移動判定
    ///</summary>
    public bool leftMove = false;
    ///<summary>
    ///右移動判定
    ///</summary>
    public bool rightMove = false;
    ///<summary>
    ///高速落下判定
    ///</summary>
    public bool fastFall = false;
    ///<summary>
    ///回転設定
    ///</summary>
    public bool rotationSetting = true;
    ///<summary>
    ///左回転
    ///</summary>
    public bool leftRotation = false;
    ///<summary>
    ///右回転
    ///</summary>
    public bool rightRotation = false;

    /// <summary>
    /// フリックが有効かどうか
    /// </summary>
    public bool nextFlick = true;

    /// <summary>
    /// メニューが有効かどうか
    /// </summary>
    public bool menuActive = false;

    /// <summary>
    /// 画面座標設定用
    /// </summary>
    private float leftEdge;
    private float rightEdge;
#if UNITY_ANDROID
    void Start()
    {
        rotationSetting = true;
    }

    void Update()
    {
            touch = Input.GetTouch(0);

        /*タッチ開始*/
        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
            tap = true;
            menuActive = true;
            if (nextFlick == false)
            {
                nextFlick = true;
            }
        }
        /*タッチ移動*/
        else if (touch.phase == TouchPhase.Moved 
                 && pose.pose == false
                 && nextFlick == true)
        {
            if (touch.position.x <= startPos.x - (MoveValue * lateralMoveValue))
            {
                leftMove = true;
                startPos = touch.position;
                tap = false;
            }
            else if (touch.position.x >= startPos.x + (MoveValue * lateralMoveValue))
            {
                rightMove = true;
                startPos = touch.position;
                tap = false;
            }
            else if (touch.position.y <= startPos.y - (MoveValue * fastFallValue))
            {
                fastFall = true;
                startPos = touch.position;
                tap = false;
            }
        }
        /*タッチ終了*/
        else if (touch.phase == TouchPhase.Ended && pose.pose == false)
        {
            //設定で回転方向を決定
            if (rotationSetting == true && tap == true)
            {
                //左回転
                leftRotation = true;
            }
            else if(tap == true)
            {
                //右回転
                rightRotation = true;
            }

            menuActive = false;
            if(nextFlick == false)
            {
                nextFlick = true;
            }
        }
    }
#endif
}
