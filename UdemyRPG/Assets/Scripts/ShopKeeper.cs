using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    private bool canOpen;

    public string[] itemsForSale = new string[40];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se é possivel falar com o ShopKeeper
        if(canOpen && Input.GetButtonDown("Fire1") 
            && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            //Envia os items a venda para o script de Shop
            Shop.instance.itemsForSale = itemsForSale;
            //Abre o Shop Menu
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Verifica se o player entrou no Tregger
        if(other.tag == "Player"){
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        //Verifica se o Player saiu do Trigger
        if(other.tag == "Player"){
            canOpen = false;
        }
    }
}
