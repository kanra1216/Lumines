using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバー時に起動するクラス
/// </summary>
public class GameOver : MonoBehaviour
{
    private Touch touch;//タッチ情報格納変数

    [SerializeField]
    private SceneController sceneCon = null;//シーンコントローラーをとる
    [SerializeField]
    private TimeLineController timeLineController = null;

    void Awake()
    {
        timeLineController.TimeLineDelete();
    }

    private void Update()
    {
        /*タッチされたらシーンコントローラーのメソッドを呼び出す*/
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                sceneCon.OnClickMenuButton("Stage1");
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Submit"))
        {
            sceneCon.OnClickMenuButton("Stage1");
        }
    }
}
