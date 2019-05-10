using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインメニューを管理するクラス
/// </summary>
public class MenuController : MonoBehaviour
{
//    /// <summary>
//    /// 接続クラス設定
//    /// </summary>
//#if UNIITY_IOS || UNITY_ANDROID
//    [SerializeField]
//    private TouchController touchController = null;
//#endif

//    /// <summary>
//    /// 各ステージ選択用
//    /// </summary>
//    [SerializeField]
//    private RectTransform stageCover1 = null;
//    [SerializeField]
//    private RectTransform stageCover2 = null;
//    [SerializeField]
//    private Image stageButton1 = null;
//    [SerializeField]
//    private Image stageButton2 = null;

//    /// <summary>
//    /// 中心点
//    /// </summary>
//    [SerializeField]
//    private Transform center = null;

//    /// <summary>
//    /// 距離判定用変数
//    /// </summary>
//    private float firstDistance;
//    private float secondDistance;

//    //オフセット用
//    readonly Vector3 ROW = new Vector3(10f, 0f,0f);

//    void Awake()
//    {
//        stageButton1.raycastTarget = true;
//        stageButton2.raycastTarget = false;
//    }


//    void Update()
//    {
//#if UNIITY_IOS || UNITY_ANDROID
//        if(selectStage == 1&&touchController.menuActive == true)
//        {
//            stageButton1.raycastTarget = true;
//        }
//        else
//        {
//            stageButton2.raycastTarget = true;
//        }

//        firstDistance = Vector3.Distance(stageCover1.position, center.position);
//        secondDistance = Vector3.Distance(stageCover2.position, center.position);

//        if(firstDistance <= secondDistance && touchController.menuActive == true)
//        {
//            for(; firstDistance < 0f; firstDistance=firstDistance-10f)
//            {
//                RightMove();
//            }
//        }
//        else if(secondDistance <= firstDistance && touchController.menuActive == true)
//        {
//            for (; secondDistance < 0f; secondDistance = secondDistance - 10f)
//            {
//                LeftMove();
//            }
//        }
//#endif


//#if UNIITY_IOS || UNITY_ANDROID
//        if (touchController.leftMove == true && touchController.fastFall == false)
//        {
//            touchController.leftMove = false;

//            //左移動
//            LeftMove();
//        }

//        if (touchController.rightMove == true && touchController.fastFall == false)
//        {
//            touchController.rightMove = false;

//            //右移動
//            RightMove();
//        }
//#endif
//    }
//    ///<summary>
//    ///左移動
//    ///</summary>
//    private void LeftMove()
//    {
//        //左移動
//        stageCover1.localPosition -= ROW;
//        stageCover2.localPosition -= ROW;
//    }

//    ///<summary>
//    ///右移動
//    ///</summary>
//    private void RightMove()
//    {
//        //右移動
//        stageCover1.localPosition += ROW;
//        stageCover2.localPosition += ROW;
//    }
}
