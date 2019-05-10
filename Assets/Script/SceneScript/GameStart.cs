using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル画面の動作を制御するクラス
/// </summary>
public class GameStart : MonoBehaviour
{
    private Touch touch;//タッチ情報格納変数

    private float count = 60f;

    [SerializeField]
    private SceneController sceneCon = null;//シーンコントローラーをとる

    private void Update()
    {
        if (count > 0) 
        {
            count -= 1f;
            return;
        }
        /*タッチされたらシーンコントローラーのメソッドを呼び出す*/
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                sceneCon.OnClickMenuButton("Stage1");
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)||Input.GetButtonDown("Submit"))
        {
            sceneCon.OnClickMenuButton("Stage1");
        }
    }
}
