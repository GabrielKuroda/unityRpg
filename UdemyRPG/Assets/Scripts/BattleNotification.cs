using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNotification : MonoBehaviour
{

    public float awakeTime;
    private float awakeCounter;
    public Text theText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Diminui o contador
        if(awakeCounter > 0){
            awakeCounter -= Time.deltaTime;
            if(awakeCounter <= 0){
                //Desativa a Notificação
                gameObject.SetActive(false);
            }
        }
    }

    public void Activate(){
        //Ativa a Notificação 
        gameObject.SetActive(true);
        awakeCounter = awakeTime;
    }
}
