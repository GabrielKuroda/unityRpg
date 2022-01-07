using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{

    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press(){
        //Verifica o MP do player
        if(BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost){
            //Desativa o Menu de Magic
            BattleManager.instance.magicMenu.SetActive(false);
            //Abre o menu de Target
            BattleManager.instance.OpenTargetMenu(spellName);
            //Retira o MP necessario
            BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP -= spellCost;
        }else{
            //Avisa o Player que não tem MP suficiente 
            BattleManager.instance.battleNotice.theText.text = "Not Enough MP!";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.magicMenu.SetActive(false);
        }
    }
}
