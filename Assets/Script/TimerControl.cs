using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    public static float Sec
    {
        get
        {
            return _Sec;
        }
        set
        {
            _Sec = value;
        }
    }

    public static int Min
    {
        get
        {
            return _Min;
        }
        set
        {
            _Min = value;
        }
    }
    // Start is called before the first frame update
    private static float _Sec;
    
    private static int _Min;

    [SerializeField]
    Text Timertext;

    // Update is called once per frame


    private void Start()
    {
        _Sec = 0;
    }

    void Update()
    {
        if(EnemySpawn.inst.isBoss == true)
        {
            if (BossController.Instance.BossDead == true)
            {

            }
            else
            {
                Timer();
            }
            
        }
        else
        {
            Timer();
        }
       
        
    }

    void Timer()
    {
        _Sec += Time.deltaTime;

        Timertext.text = string.Format("Time : {0:D2}:{1:D2}", _Min, (int)_Sec);

        if((int)_Sec > 59)
        {
            _Sec = 0;
            _Min++;
        }
    }
}
