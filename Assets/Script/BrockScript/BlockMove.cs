using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの左右移動を行うクラス
/// </summary>
public class BlockMove : MonoBehaviour
{
    private BlockFall blockFall;
    private BlockController blockController;
    private GameController gameController;
    private SoundController soundController;
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
    private TouchController touchController;
#endif

    public enum MoveState
    {
        ///<summary>
        ///無移動
        ///</summary>
        NONE,
        ///<summary>
        ///左移動
        ///</summary>
        LEFT,
        ///<summary>
        ///右移動
        ///</summary>
        RIGHT,
        ///<summary>
        ///連続左移動
        ///</summary>
        CONT_LEFT,
        ///<summary>
        ///連続右移動
        ///</summary>
        CONT_RIGHT,
    }
    private MoveState moveState;

    ///<summary>
    ///連続移動に移行までの時間の計測用
    ///</summary>
    private float contTime = 0f;
    ///<summary>
    ///移動するまでの時間の計測用
    ///</summary>
    private float moveTime = 0f;

    ///<summary>
    ///連続移動になるまでの時間
    ///</summary>
    const float CONT_TIME = 1f;
    ///<summary>
    ///移動するまでの時間
    ///</summary>
    const float MOVE_TIME = 0.1f;

    //オフセット用
    readonly Vector3 ROW = new Vector3(1f, 0f);

    void Awake()
    {
        blockFall = GetComponent<BlockFall>();
        blockController = GetComponent<BlockController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        touchController = GameObject.Find("TouchController").GetComponent<TouchController>();
#endif
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }

    void Start()
    {
        //生成したときに左移動が入力されていたら
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            contTime = CONT_TIME;

            //状態を連続左移動に
            moveState = MoveState.CONT_LEFT;
        }

        //生成したときに右移動が入力されていたら
        if (Input.GetKey(KeyCode.RightArrow))
        {
            contTime = CONT_TIME;

            //状態を連続右移動に
            moveState = MoveState.CONT_RIGHT;
        }
    }

    void Update()
    {
        if (blockFall.fallState == BlockFall.FallState.FREEFALL)
        {
            switch (moveState)
            {
                #region 無移動
                case MoveState.NONE:
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        //状態を左移動に
                        moveState = MoveState.LEFT;
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        //状態を右移動に
                        moveState = MoveState.RIGHT;
                    }
                    break;
                #endregion

                #region 左移動
                case MoveState.LEFT:
                    //左移動
                    LeftMove();

                    //状態を連続左移動に
                    moveState = MoveState.CONT_LEFT;
                    break;
                #endregion

                #region  右移動
                case MoveState.RIGHT:
                    RightMove();

                    //状態を連続右移動に
                    moveState = MoveState.CONT_RIGHT;
                    break;
                #endregion

                #region 連続左移動
                case MoveState.CONT_LEFT:
                    //連続移動になるまでの時間を計測
                    contTime += UnityEngine.Time.deltaTime;

                    //連続行動になるまでの時間になったら
                    if (contTime >= CONT_TIME)
                    {
                        //移動するまでの時間を計測
                        moveTime += UnityEngine.Time.deltaTime;

                        //移動するまでの時間になったら
                        if (moveTime >= MOVE_TIME)
                        {
                            //移動するまでの時間をリセット
                            moveTime = 0f;

                            //左移動
                            gameObject.transform.position -= ROW;

                            //移動先の中身が空では無かったら
                            if (!blockController.BlockFieldCheck())
                            {
                                //右移動で戻す
                                gameObject.transform.position += ROW;
                            }
                        }
                    }

                    if (!Input.GetKey(KeyCode.LeftArrow))
                    {
                        //連続移動になるまでの時間をリセット
                        contTime = 0f;
                        //移動するまでの時間をリセット
                        moveTime = 0f;

                        //状態を無移動に
                        moveState = MoveState.NONE;
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        //連続移動になるまでの時間をリセット
                        contTime = 0f;
                        //移動するまでの時間をリセット
                        moveTime = 0f;

                        //状態を右移動に
                        moveState = MoveState.RIGHT;
                    }
                    break;
                #endregion

                #region 連続右移動
                case MoveState.CONT_RIGHT:
                    //連続移動になるまでの時間を計測
                    contTime += UnityEngine.Time.deltaTime;

                    //連続移動になるまでの時間になったら
                    if (contTime >= CONT_TIME)
                    {
                        //移動するまでの時間を計測
                        moveTime = 0f;

                        //移動するまでの時間になったら
                        if (moveTime >= MOVE_TIME)
                        {
                            //移動するまでの時間をリセット
                            moveTime = 0f;

                            //右移動
                            gameObject.transform.position += ROW;

                            //移動先の中身が空では無かったら
                            if (!blockController.BlockFieldCheck())
                            {
                                //左移動で戻す
                                gameObject.transform.position -= ROW;
                            }
                        }
                    }

                    if (!Input.GetKey(KeyCode.RightArrow))
                    {
                        //連続移動になるまでの時間をリセット
                        contTime = 0f;
                        //移動するまでの時間をリセット
                        moveTime = 0f;

                        //状態を無移動に
                        moveState = MoveState.NONE;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        //連続移動になるまでの時間をリセット
                        contTime = 0f;
                        //移動するまでの時間をリセット
                        moveTime = 0f;

                        //状態を右移動に
                        moveState = MoveState.RIGHT;
                    }
                    break;
                #endregion
            }

#if UNITY_EDITOR || UNIITY_IOS || UNITY_ANDROID
            if (touchController.leftMove&&touchController.fastFall == false)
            {
                touchController.leftMove = false;

                //左移動
                LeftMove();
            }

            if (touchController.rightMove&& touchController.fastFall == false)
            {
                touchController.rightMove = false;

                //右移動
                RightMove();
            }
#endif
        }
    }

    ///<summary>
    ///左移動
    ///</summary>
    void LeftMove()
    {
        //左移動
        gameObject.transform.position -= ROW;
        blockController.guidL.transform.position -= ROW;
        blockController.guidR.transform.position -= ROW;

        //移動先の中身が空では無かったら
        if (!blockController.BlockFieldCheck())
        {
            //右移動で戻す
            gameObject.transform.position += ROW;
            blockController.guidL.transform.position += ROW;
            blockController.guidR.transform.position += ROW;
        }
        soundController.MoveSound();
    }

    ///<summary>
    ///右移動
    ///</summary>
    void RightMove()
    {
        //右移動
        gameObject.transform.position += ROW;
        blockController.guidL.transform.position += ROW;
        blockController.guidR.transform.position += ROW;

        //移動先の中身が空では無かったら
        if (!blockController.BlockFieldCheck())
        {
            //左移動で戻す
            gameObject.transform.position -= ROW;
            blockController.guidL.transform.position -= ROW;
            blockController.guidR.transform.position -= ROW;
        }
        soundController.MoveSound();
    }
}
