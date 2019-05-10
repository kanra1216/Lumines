using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///　シーンの遷移を実行するクラス
/// </summary>
public class SceneController : MonoBehaviour
{
    /*ロードするシーンを格納する変数を作成*/
    public static string loadScene;

    /*読み込むシーンをローディングシーンに送る*/
    public void OnClickMenuButton(string scene)
    {
        loadScene = scene;
        SceneManager.LoadScene("Loading");
    }

    /*ロードシーンで読み込むシーンを渡すメソッド*/
    public static string LoadSceneGet()
    {
        return loadScene;
    }

    /*ゲーム終了するメソッド*/
    public void QuitGame()
    {
        Application.Quit();
    }
}
