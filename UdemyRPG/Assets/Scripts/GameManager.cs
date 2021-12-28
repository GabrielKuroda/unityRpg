using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] refereceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica situação do Menu, Load e Sialogo
        if(gameMenuOpen || dialogActive || fadingBetweenAreas){
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
}
