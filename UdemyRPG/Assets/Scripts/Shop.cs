using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop instance;
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    public Text goldText;
    public string[] itemsForSale;
    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButton;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        //Abre Shop Menu
        shopMenu.SetActive(true);
        //Informa que o Shop está aberto
        GameManager.instance.shopActive = true;
        //Pega o Gold
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        //Fecha o Shop MEnu
        shopMenu.SetActive(false);
        //Informa que o Shop está fechado
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu(){
        //Abre o menu de compra e fecha o de venda
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        //Percorre os botões de Items
        for(int i = 0; i < buyItemButtons.Length; i++){
            //Deifine os valores dos btns
            buyItemButtons[i].buttonValue = i;
            //Verifica se há Item para mostrar
            if(itemsForSale[i] != ""){
                //Torna o Item Ativo no menu
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                //PEga a Sprite do Item
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                //Pega a Quantidade do item
                buyItemButtons[i].amountText.text = "";
            }else{
                //Desativa o Item
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu(){
        //Abre o menu de venda e fecha o de compra
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        GameManager.instance.SortItems();
        //Percorre os botões de Items
        for(int i = 0; i < sellItemButton.Length; i++){
            //Deifine os valores dos btns
            sellItemButton[i].buttonValue = i;
            //Verifica se há Item para mostrar
            if(GameManager.instance.itemsHeld[i] != ""){
                //Torna o Item Ativo no menu
                sellItemButton[i].buttonImage.gameObject.SetActive(true);
                //PEga a Sprite do Item
                sellItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                //Pega a Quantidade do item
                sellItemButton[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }else{
                //Desativa o Item
                sellItemButton[i].buttonImage.gameObject.SetActive(false);
                sellItemButton[i].amountText.text = "";
            }
        }
    }

    public void CloseBuyMenu(){
        //Fecha o menu de compra
        buyMenu.SetActive(false);
    }

    public void CloseSellMenu(){
        //Fecha o menu de venda
        sellMenu.SetActive(false);
    }
}
