using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Press(int charToUseOn){
        
        StartCoroutine(ItemUseCo(charToUseOn));
        
    }

    public IEnumerator ItemUseCo(int charToUseOn){
        int used = BattleManager.instance.activeItem.UseInBattle(charToUseOn);
        GameManager.instance.SortItems();
        BattleManager.instance.UpdateItemUIStats();
        BattleManager.instance.UpdateUIStats();
        BattleManager.instance.ShowItems();
        //Espera 1s
        yield return new WaitForSeconds(1f);
        if(used == 0){
            BattleManager.instance.CloseItemMenu();
            BattleManager.instance.NextTurn();
        }
    }
}
