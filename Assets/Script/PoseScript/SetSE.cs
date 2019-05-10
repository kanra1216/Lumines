using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SEの音量を制御するクラス
/// </summary>
public class SetSE : MonoBehaviour
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
        text.text = ((int)slider.value + setVol) + "%";
    }

    /// <summary>
    /// SEの大きさを設定
    /// </summary>
    public void SEVolume(float vol)
    {
        mixer.SetFloat("SE", vol);
    }
}
