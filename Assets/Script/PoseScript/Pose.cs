using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ポーズ画面を制御するクラス
/// </summary>
public class Pose : MonoBehaviour
{
    [SerializeField]
    private TouchController touchController = null;
    [SerializeField]
    private TimeLineMove timeLineMove = null;

    /// <summary>
    /// 左右回転を設定するための参照
    /// </summary>
    [SerializeField]
    private GameObject rotateRight=null;
    [SerializeField]
    private GameObject rotateLeft=null;

    [SerializeField]
    private GameObject poseMenu = null;

    public bool pose;

    void Start()
    {
        pose = false;
        poseMenu.SetActive(false);
    }

    /// <summary>
    /// ポーズを実行する
    /// </summary>
    public void PoseOn()
    {
        pose = true;
        UnityEngine.Time.timeScale = 0f;
        poseMenu.SetActive(true);
        timeLineMove.Pose();
        if (touchController.rotationSetting == true)
        {
            EventSystem.current.SetSelectedGameObject(rotateRight);
        }
        else if(touchController.rotationSetting == false)
        {
            EventSystem.current.SetSelectedGameObject(rotateLeft);
        }
    }

    /// <summary>
    /// ポーズを解除する
    /// </summary>
    public void PoseOff()
    {
        pose = false;
        UnityEngine.Time.timeScale = 1f;
        timeLineMove.MoveStart();
        poseMenu.SetActive(false);
    }

    /// <summary>
    /// アプリケーションを終了
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
