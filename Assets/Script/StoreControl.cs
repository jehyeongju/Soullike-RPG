using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreControl : MonoBehaviour
{
    public bool isLongSword = false;
    public bool isKnife = false;
    public bool isGreatSword = false;
    public bool ShopActivated = false;
    public bool LsSelected = false;
    public bool knifeSelected = false;
    public bool GsSelected = false;
    public bool LsBuyed = false;
    public bool KnifeBuyed = false;
    public bool GsBuyed = false;
    public bool HealBuyed = false;
    private bool isPause;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject SpellShop;
    private PlayerAttackCollision playerAttack;
    [Header("무기 가격")]
    private int LongSwordPrice = 300;
    private int KnifePrice = 500;
    private int GreatSwordPrice = 1000;
    private int HealSpellPrice = 1500;
    [SerializeField]
    private GameObject[] curWeapon;
    private int i;
    [Header("무기 버튼")]
    [SerializeField]
    private Button LSButton;
    [SerializeField]
    private Button KnifeButton;
    [SerializeField]
    private Button GSButton;
    [Header("무기 텍스트")]
    [SerializeField]
    private Text LSText;
    [SerializeField]
    private Text KnifeText;
    [SerializeField]
    private Text GSText;
    [SerializeField]
    private Text HealText;
    [Header("무기 데미지")]
    private int LSDamage = 15;
    private int KnifeDamge = 20;
    private int GSDamage = 30;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttackCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenShop();
    }
    private void TryOpenShop()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShopActivated = !ShopActivated;


            if (ShopActivated)
            {
                OpenShop();
                Time.timeScale = 0;
                return;
            }
               
            else
            {
                CloseShop();
                Time.timeScale = 1;
                return;
            }
                
                
        }
    }

    private void OpenShop()
    {
        Shop.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void CloseShop()
    {
        Shop.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLongSword()
    {
        if (isLongSword == false)
        {
            if (PlayerController.Instance.money >= LongSwordPrice)
            {
            
                isLongSword = true;
                PlayerController.Instance.money -= LongSwordPrice;
                curWeapon[i].SetActive(false);
                curWeapon[1].SetActive(true);
                i = 1;
                PlayerAttackCollision.Inst.AttackDamage = LSDamage;
                LsSelected = true;
                LsBuyed = true;
            }

        }
        else if(LsBuyed == true)
        {
            curWeapon[i].SetActive(false);
            curWeapon[1].SetActive(true);
            i = 1;
            PlayerAttackCollision.Inst.AttackDamage = LSDamage;
            LSText.text = string.Format("Select");
            LsSelected = true;
            
        }
        else if (LsSelected == true)
        {
            LSButton.interactable = false;
            LSText.text = string.Format("Sold");
        }
    }
    public void OnKnife()
    {
        if (isKnife == false)
        {
            if (PlayerController.Instance.money >= KnifePrice)
            {
                isKnife = true;
                PlayerController.Instance.money -= KnifePrice;
                curWeapon[i].SetActive(false);
                curWeapon[2].SetActive(true);
                i = 2;
                PlayerAttackCollision.Inst.AttackDamage = KnifeDamge;
                knifeSelected = true;
                KnifeBuyed = true;
            }

        }
        else if (KnifeBuyed == true)
        {
            curWeapon[i].SetActive(false);
            curWeapon[2].SetActive(true);
            i = 2;
            PlayerAttackCollision.Inst.AttackDamage = KnifeDamge;
            KnifeText.text = string.Format("Select");
            knifeSelected = true;
        }
        else if(knifeSelected == true)
        {
            KnifeButton.interactable = false;
            KnifeText.text = string.Format("Sold");
        }
    }
    public void OnGreatSword()
    {
        if (isGreatSword == false)
        {
            if (PlayerController.Instance.money >= GreatSwordPrice)
            {
                isGreatSword = true;
                PlayerController.Instance.money -= GreatSwordPrice;
                curWeapon[i].SetActive(false);
                curWeapon[3].SetActive(true);
                i = 3;
                PlayerAttackCollision.Inst.AttackDamage = GSDamage;
                GsSelected = true;
                GsBuyed = true;
            }
           
        }
        else if (GsBuyed == true)
        {
            curWeapon[i].SetActive(false);
            curWeapon[3].SetActive(true);
            i = 3;
            PlayerAttackCollision.Inst.AttackDamage = GSDamage;
            GSText.text = string.Format("Select");
            GsSelected = true;
        }
        else if(GsSelected == true)
        {
            GSButton.interactable = false;
            GSText.text = string.Format("Selected");
        }
    }
    public void OnHealSpell()
    {
        if(HealBuyed == false)
        {
            HealBuyed = true;
            PlayerController.Instance.money -= HealSpellPrice;
            HealText.text = string.Format("Sold");
            GSButton.interactable = false;
        }
    }
    
    public void OnSwordClose()
    {
        Shop.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnSpellClose()
    {
        SpellShop.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnChangeSpellShop()
    {
        Shop.SetActive(false);
        SpellShop.SetActive(true);
    }

    public void OnChangeSwordShop()
    {
        SpellShop.SetActive(false);
        Shop.SetActive(true);
    }
}   
