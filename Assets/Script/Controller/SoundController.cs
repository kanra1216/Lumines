using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドの制御をするクラス
/// </summary>
public class SoundController : MonoBehaviour
{
    /// <summary>
    /// 移動の効果音
    /// </summary>
    [SerializeField]
    private AudioSource moveSound = null;

    /// <summary>
    /// 落下の効果音
    /// </summary>
    [SerializeField]
    private AudioSource downSound = null;

    /// <summary>
    /// 回転の効果音
    /// </summary>
    [SerializeField]
    private AudioSource rotationSound = null;

    /// <summary>
    /// ブロックが揃った時の効果音
    /// </summary>
    [SerializeField]
    private AudioSource deleteCheckSound = null;

    /// <summary>
    /// タイムラインでチェックされた時の効果音
    /// </summary>
    [SerializeField]
    private AudioSource timeLineCheckSound = null;

    /// <summary>
    /// ブロックが消去される時の効果音
    /// </summary>
    [SerializeField]
    private AudioSource deleteSound = null;

    public void MoveSound()
    {
        moveSound.Play();
    }

    public void DownSound()
    {
        downSound.Play();
    }

    public void RotationSound()
    {
        rotationSound.Play();
    }

    public void DeleteCheckSound()
    {
        deleteCheckSound.Play();
    }

    public void TimeLineCheckSound()
    {
        timeLineCheckSound.Play();
    }

    public void DeleteSound()
    {
        deleteSound.Play();
    }
}
