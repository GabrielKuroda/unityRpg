using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inn : MonoBehaviour
{
    public static Inn instance;
    public GameObject innMenu, innButtons;
    public int price;
    public Text goldText, priceText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInn(){
        //Toca o SFX
        AudioManager.instance.PlaySFX(5);
        //Abre Shop Menu
        innMenu.SetActive(true);
        //Informa que o Shop está aberto
        GameManager.instance.innActive = true;
        //Pega o Gold
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        priceText.text = "Hello! Would you like a Bed for " + price + "g?";
    }

    public void CloseInn()
    {
        //Fecha o Shop MEnu
        innMenu.SetActive(false);
        //Informa que o Shop está fechado
        GameManager.instance.innActive = false;
    }

    public void Stay(){
        StartCoroutine(StayInnCo());
    }

    public IEnumerator StayInnCo()
    {
        GameManager.instance.shopActive = true;
        GameManager.instance.currentGold -= price;
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        innButtons.gameObject.SetActive(false);
        priceText.text = "Thank you! Have a good rest!";
        yield return new WaitForSeconds(1f);
        CloseInn();
        UIFade.instance.FadeToBlack();
        for(int i = 0; i < GameManager.instance.playerStats.Length;i++)
        {
            GameManager.instance.playerStats[i].currentHP = GameManager.instance.playerStats[i].maxHP;
            GameManager.instance.playerStats[i].currentMP = GameManager.instance.playerStats[i].maxMP;
        }
        yield return new WaitForSeconds(2f);
        UIFade.instance.FadeFromBlack();
        GameManager.instance.shopActive = false;
    }
}
