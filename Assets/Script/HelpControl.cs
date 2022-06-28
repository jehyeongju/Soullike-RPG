using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpControl : MonoBehaviour
{
    public GameObject HelpImage;
    private StoreControl store;
    public bool HelpActivated;
    // Start is called before the first frame update
    void Start()
    {
        store = FindObjectOfType<StoreControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(store.ShopActivated == false)
            {
                HelpActivated = !HelpActivated;

                if (HelpActivated)
                {
                    HelpImage.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                    return;
                }
                else
                {
                    HelpImage.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                    return;
                }
            }
            
        }

    }
    public void OnHelpClose()
    {
        HelpImage.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void OnHelpOpen()
    {
        HelpImage.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        return;
    }
}
