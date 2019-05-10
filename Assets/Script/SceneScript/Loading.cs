using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ローディング画面を制御するクラス
/// </summary>
public class Loading : MonoBehaviour
{
    private AsyncOperation ope;//ロードの読み込み状況を図る変数

    private string nextscene;//もらった読み込むシーンを入れる変数

    /*開始時にメソッドを起動してコルーチン開始*/
    void Start()
    {
        nextscene = SceneController.LoadSceneGet();

        StartCoroutine(LoadSceneAndWait());
    }

    /*次のシーンを読み込むコルーチン*/
    IEnumerator LoadSceneAndWait()
    {
        float start = UnityEngine.Time.realtimeSinceStartup;
        ope = SceneManager.LoadSceneAsync(nextscene);
        ope.allowSceneActivation = false;

        while (UnityEngine.Time.realtimeSinceStartup - start < 4.5f)
        {
            yield return null;
        }

        ope.allowSceneActivation = true;
    }
}
