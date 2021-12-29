using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 
    //Adiciona um Cabeçalho no Unity
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Weapom/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn){
        //Pega o Char
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];
        //Verifica se é um Item
        if(isItem){
            //Verifica se afeta o HP
            if(affectHP){
                //Add HP
                selectedChar.currentHP += amountToChange;
                //Verifica se o HP atual passou o Maximo
                if(selectedChar.currentHP > selectedChar.maxHP){
                    //Set o Hp = o maximo
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }
            //Verifica se afeta o MP
            if(affectMP){
                //Add MP
                selectedChar.currentMP += amountToChange;
                //Verifica se o MP atual passou o Maximo
                if(selectedChar.currentMP > selectedChar.maxMP){
                    //Set o MP = o maximo
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }
            //Verifica se afeta str
            if(affectStr){
                //Add Str
                selectedChar.strength += amountToChange;
            }
        }
        //Verifica se é arma
        if(isWeapon){
            //Verifica se tem arma equipada
            if(selectedChar.equippedWpn != ""){
                //Add a arma equipada ao Inventario
                GameManager.instance.AddItem(selectedChar.equippedWpn);
            }
            //Equipa Arma
            selectedChar.equippedWpn = itemName;
            selectedChar.wpnPwr = weaponStrength;
        }
        //Verifica se é aramadura
        if(isArmor){
            //Verifica se tem Aramadura equipada
            if(selectedChar.equippedArmr != ""){
                //Add a armadura equipada ao Inventario
                GameManager.instance.AddItem(selectedChar.equippedArmr);
            }
            //Equipa Armadura
            selectedChar.equippedArmr = itemName;
            selectedChar.armrPwr = armorStrength;
        }
        //Tira o item do inventario
        GameManager.instance.RemoveItem(itemName);
    }
}
