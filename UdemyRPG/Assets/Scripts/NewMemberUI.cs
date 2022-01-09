using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMemberUI : MonoBehaviour
{
    public Text textSaid, textName;
    public GameObject textPanel,charName;
    public Image charImage;
    public static NewMemberUI instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMemberMessage(string memberName, string memberMessage){
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++){
            if(GameManager.instance.playerStats[i].charName == memberName){
                GameManager.instance.playerStats[i].gameObject.SetActive(true);
                charImage.sprite = GameManager.instance.playerStats[i].charImage;
                textSaid.text = memberMessage;
                textName.text = GameManager.instance.playerStats[i].charName;
                charName.SetActive(true);
                textPanel.SetActive(true);
            }
        }
    }

    public void NotifyNewMEmber(string memberName){
        charName.SetActive(false);
        textSaid.text = memberName + " Joined your Party!";
    }

    public void Close(){
        charName.SetActive(false);
        textPanel.SetActive(false);
    }
}
