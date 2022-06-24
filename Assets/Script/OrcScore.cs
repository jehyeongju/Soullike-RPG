using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OrcScore : MonoBehaviour
{
    public static int orcKilled;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        orcKilled = 0;
    }

    private void Update()
    {
        text.text = "Orc Killed : " + orcKilled;
    }

}
