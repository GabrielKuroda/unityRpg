using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if(Input.GetKeyDown(KeyCode.O)){
            saveData();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            LoadData();
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

    public void saveData(){
        //Salva a cena em que o Player está
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        //Salva a posição do Player
        PlayerPrefs.SetFloat("Player_Position_X",PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y",PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z",PlayerController.instance.transform.position.z);
        //Salva Status Chars
        for(int i = 0; i < playerStats.Length; i++){
            if(playerStats[i].gameObject.activeInHierarchy){
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active",1);
            }else{
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active",0);
            }
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentExp", playerStats[i].currentEXP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Defence", playerStats[i].defence);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WpnPwr", playerStats[i].wpnPwr);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmrPwr", playerStats[i].armrPwr);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquippedWpn", playerStats[i].equippedWpn);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquippedArmr", playerStats[i].equippedArmr);
        }

        //Salva Inventario
        for(int i = 0; i < itemsHeld.Length; i++){
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }
    }

    public void LoadData(){
        //Carrega a Posição do Player
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_X"),PlayerPrefs.GetFloat("Player_Position_Y"),PlayerPrefs.GetFloat("Player_Position_Z"));
    
        //Carrega Status dos Chars
        for(int i = 0; i < playerStats.Length; i++){
            if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0){
                playerStats[i].gameObject.SetActive(false);
            }else{
                playerStats[i].gameObject.SetActive(true);
            }
            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level");
            playerStats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxMP");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength");
            playerStats[i].defence = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Defence");
            playerStats[i].wpnPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WpnPwr");
            playerStats[i].armrPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmrPwr");
            playerStats[i].equippedWpn = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedWpn");
            playerStats[i].equippedArmr = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedArmr");
        }

        //Carrega inventorio
        for(int i = 0; i < itemsHeld.Length; i++){
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}
