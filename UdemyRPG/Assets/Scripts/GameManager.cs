using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] refereceItems;

    public int currentGold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica situação do Menu, Load e Sialogo
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive){
            //Impede movimento do Player
            PlayerController.instance.canMove = false;
        }else{
            //Autoriza Movimento do player
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemDetails(string itemToGrab){
        //Percorre os Itens do jogo
        for(int i = 0; i < refereceItems.Length;i++){
            //Verifica se é o item que esta procurando
            if(refereceItems[i].itemName == itemToGrab){
                return refereceItems[i];
            }
        }
        return null;
    }

    public void SortItems(){
        bool itemAfterSpace = true;

        while(itemAfterSpace){
            itemAfterSpace = false;
            //Percorre os Items do Player
            for(int i = 0; i < itemsHeld.Length - 1; i++){
                //Verifica se a posição está vazia
                if(itemsHeld[i] == ""){
                    //Se estiver vazio pega o valor da proxima
                    itemsHeld[i] = itemsHeld[i+1];                
                    itemsHeld[i+1] = "";
                    //Se estiver vazio pega o valor da proxima
                    numberOfItems[i] = numberOfItems[i+1];
                    numberOfItems[i+1] = 0;
                    //Se a posição atual não estiver vazia, continua no While para garantir o Sort
                    if(itemsHeld[i] != ""){
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd){
        int newItemPosition = 0;
        bool foundSpace = false;

        //Percorre o Inventario do Player
        for(int i = 0; i < itemsHeld.Length; i++){
            //Verifica por um espaço vazio ou Item igual
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd){
                //Set na posição que o item vai ficar
                newItemPosition = i;
                foundSpace = true;
                break;
            }
        }
        
        //Verifica se encontrou um espaço para o item
        if(foundSpace){
            bool itemExists = false;
            //Percorre os Items do jogo
            for(int i =0; i < refereceItems.Length; i++){
                //Verifica se o Item existe
                if(refereceItems[i].itemName == itemToAdd){
                    itemExists = true;
                    break;
                }
            }

            if(itemExists){
                //Adiciona um Item ao inventário
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }else{
                //Log de Erro
                Debug.LogError(itemToAdd + " Does not Exist!!");
            }
        }
        //Atualiza o Menu
        GameMenu.instance.ShowItems();

    }

    public void RemoveItem(string itemToRemove){
        bool foundItem = false;
        int itemPosition = 0;

        //Percorre o Inventario do Player
        for(int i = 0; i < itemsHeld.Length; i++){
            //Verifica se o Item está no inventario
            if(itemsHeld[i] == itemToRemove){
                foundItem = true;
                itemPosition = i;
                break;
            }
        }

        //Verifica se o Item foi encontrado
        if(foundItem){
            //Diminui a Quantidade de item
            numberOfItems[itemPosition]--;
            //Verifica se a quantidade é 0
            if(numberOfItems[itemPosition] <= 0){
                //Retira o Item
                itemsHeld[itemPosition] = "";
            }
            //Atualiza o Menu
            GameMenu.instance.ShowItems();
        }else{
            //Log Erro
            Debug.LogError("Couldn't Find "+ itemToRemove);
        }
    }
}
