using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
    [SerializeField]
    Text Timertext;

    private void Start()
    {
        Timer();
    }
    void Timer()
    {
        Timertext.text = string.Format("ClearTime : {0:D2}:{1:D2}", TimerControl.Min, (int)TimerControl.Sec);
    }
}
