using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{

    public static BattleReward instance;
    public Text expText, itemText, goldText;
    public GameObject rewardScreen;
    public string[] rewardItems;
    public int expEarned, rewardGold;
    public bool markQuestComplete;
    public string questToMark;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenRewardScreen(int xp,int gold, string[] rewards){
        //Set nas variaveis
        expEarned = xp;
        rewardItems = rewards;
        //Vria os textos
        expText.text = "Everyone earned "+ expEarned + " xp!";
        goldText.text = "You earned "+ rewardGold + " g!";
        itemText.text = "";
        for(int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewards[i] + "\n";
        }
        //Ativa a tela
        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen(){
        //Percorre todos os Players
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            //Se o Player estiver ativo
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                //Add Exp
                GameManager.instance.playerStats[i].AddExp(expEarned);
            }
        }
        //Percorre os Reward Items
        for(int i = 0; i < rewardItems.Length; i++)
        {   
            //Add os items
            GameManager.instance.AddItem(rewardItems[i]);
        }
        GameManager.instance.currentGold += rewardGold;
        //Fecha a Tela
        rewardScreen.SetActive(false);
        //Indica que a Batalha acabou
        GameManager.instance.battleActive = false;
        //Finaliza Quest
        if(markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
    }
}
