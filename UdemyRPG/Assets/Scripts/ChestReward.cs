using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestReward : MonoBehaviour
{
    
    public static ChestReward instance;
    public GameObject chestScreen;
    public Text itemText;
    public string item;
    public string chestName;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChest(string itemInside, string name){
        chestName = name;
        item = itemInside;
        itemText.text = item;
        GameManager.instance.chestOpen = true;
        //Ativa a tela
        chestScreen.SetActive(true);
    }

    public void CloseChest(){
        GameManager.instance.AddItem(item);
        GameManager.instance.CloseChest(chestName);
        //Fecha a Tela
        chestScreen.SetActive(false);
    }
}
