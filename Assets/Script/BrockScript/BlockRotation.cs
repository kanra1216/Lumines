using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの左右回転を行うクラス
/// </summary>
public class BlockRotation: MonoBehaviour
{
    private BlockFall blockFall;
    private BlockController blockController;
    private GameController gameController;
    private SoundController soundController;
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
    private TouchController touchController;
#endif

    //オフセット用
    readonly Vector3 ROW = new Vector3(1f, 0f);
    readonly Vector3 COL = new Vector3(0f, 1f);

    const int A_ANGLE = 0;
    const int B_ANGLE = 270;
    const int C_ANGLE = 180;
    const int D_ANGLE = 90;

    /// <summary>
    /// 左回転
    /// </summary>
    const float LEFT_ROTATION = 90f;
    /// <summary>
    /// 右回転
    /// </summary>
    const float RIGHT_ROTATION = -90f;

    private Vector3 prePos;
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

    void Update()
    {
        if(blockFall.fallState != BlockFall.FallState.LANDING)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //左回転
                LeftRotation();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                //右回転
                RightRotation();
            }

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
            if (touchController.rightRotation&& touchController.fastFall == false)
            {
                touchController.rightRotation = false;

                //右回転
                LeftRotation();
            }
            else if (touchController.leftRotation&& touchController.fastFall == false)
            {
                touchController.leftRotation = false;

                //右回転
                RightRotation();
            }
#endif
        }
    }

    ///<summary>
    ///左回転
    ///</summary>
    void LeftRotation()
    {
        //左回転
        gameObject.transform.eulerAngles += new Vector3(0f, 0f, LEFT_ROTATION);
        soundController.RotationSound();
    }

    ///<summary>
    ///右回転
    ///</summary>
    void RightRotation()
    {
        //右回転
        gameObject.transform.eulerAngles += new Vector3(0f, 0f, RIGHT_ROTATION);
        soundController.RotationSound();
    }
}
