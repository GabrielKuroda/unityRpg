using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press(){
        //Verifica se está no Menu
        if(GameMenu.instance.theMenu.activeInHierarchy){
            //Verifica se há um Item no botão
            if(GameManager.instance.itemsHeld[buttonValue] != ""){
                //Chama a função que selecionara o item
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
        //Verifica se está na loja
        if(Shop.instance.shopMenu.activeInHierarchy){
            //Verifica se há item
            if(Shop.instance.itemsForSale[buttonValue] != ""){
                //Verifia se é Mnu de Buy
                if(Shop.instance.buyMenu.activeInHierarchy){
                    Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
                }
            }
            //Verifica se há item
            if(GameManager.instance.itemsHeld[buttonValue] != ""){
                //Verifica se é Menu de Sell
                if(Shop.instance.sellMenu.activeInHierarchy){
                    Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
                }
            }
        }
    }
}
