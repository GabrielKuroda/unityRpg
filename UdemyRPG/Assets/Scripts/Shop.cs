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
    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

    public GameObject buyInfoPanel, buyActionPanel;

    public GameObject sellInfoPanel, sellActionPanel;

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
        //Toca o SFX
        AudioManager.instance.PlaySFX(5);
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
        //Fecha o Buy MEnu
        CloseBuyMenu();
        //Fecha o Sell MEnu
        CloseSellMenu();
        //Informa que o Shop está fechado
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu(){

        //Abre o menu de compra e fecha o de venda
        buyMenu.SetActive(true);
        CloseSellMenu();

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
        
        CloseBuyMenu();

        ShowSellItems();
    }

    private void ShowSellItems(){
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
        //Fecha MEnus de ação e info
        buyInfoPanel.SetActive(false);
        buyActionPanel.SetActive(false);
    }

    public void CloseSellMenu(){
        //Fecha o menu de venda
        sellMenu.SetActive(false);
        //Fecha MEnus de ação e info
        sellInfoPanel.SetActive(false);
        sellActionPanel.SetActive(false);
    }

    public void SelectBuyItem(Item buyItem){
        //Abre o menu de Info e action
        buyInfoPanel.SetActive(true);
        buyActionPanel.SetActive(true);
        //Pega o Item selecionado e popula as informações
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }

    public void SelectSellItem(Item sellItem){
        //Abre o menu de Info e action
        sellInfoPanel.SetActive(true);
        sellActionPanel.SetActive(true);
        //Pega o Item selecionado e popula as informações
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text = "Value: " + (Mathf.FloorToInt(selectedItem.value * .5f)) + "g";
    }

    public void BuyItem(){
        //Verifica se tem item selecionado
        if(selectedItem != null){
            //Verifica o Gold do Player
            if(GameManager.instance.currentGold >= selectedItem.value){
                //Retira o Gold
                GameManager.instance.currentGold -= selectedItem.value;
                //Add o item no Inventario
                GameManager.instance.AddItem(selectedItem.itemName);
            }
            //Atualiza as infos do Gold
            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }

    public void SellItem(){
        //Verifica se tem item selecionado
        if(selectedItem != null){
            int itemPosition = 0;
            //Pega a posição do item no Inventario
            for(int i = 0; i < sellItemButton.Length; i++){
                if(GameManager.instance.itemsHeld[i] == selectedItem.itemName){
                    itemPosition = i;
                }
            }
            //Add gold
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);
            //Remove o item do inventario
            GameManager.instance.RemoveItem(selectedItem.itemName);
            //Verifica se ainda tem esse tipo de item no inventario
            if(GameManager.instance.itemsHeld[itemPosition] == "" || 
                GameManager.instance.itemsHeld[itemPosition] != selectedItem.itemName){
                //Fecha as infos e action de sell
                sellInfoPanel.SetActive(false);
                sellActionPanel.SetActive(false);
            }
        }
        //Atualiza as infos do Gold
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        //Atualiza a tela de sell
        ShowSellItems();
    }
}
