using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BGMの音量を管理するクラス
/// </summary>
public class SetBGM : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer=null;

    [SerializeField]
    private Slider slider=null;

    [SerializeField]
    private Text text=null;

    private int setVol = 80;

    private void Update()
    {
        text.text = ((int)slider.value+ setVol) + "%";
    }

    /// <summary>
    /// BGMの大きさを設定
    /// </summary>
    public void BGMVolume(float vol)
    {
        mixer.SetFloat("BGM", vol);
    }
}