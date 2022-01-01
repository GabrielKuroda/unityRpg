using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{

    public string questToMark;
    public bool markComplete;
    public bool markOnEnter;
    private bool canMark;
    public bool deactivateOnMarking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se pode Marcar e se teve o click
        if(canMark && Input.GetButtonDown("Fire1")){
            canMark = false;
            MarkQuest();
        }
    }

    public void MarkQuest(){
        //Verifica se tem que completar a quest
        if(markComplete){
            //Completa a quest
            QuestManager.instance.MarkQuestComplete(questToMark);
        }else{
            //Não completa a Quest
            QuestManager.instance.MarkQuestIncomplete(questToMark);
        }
        //Desativa a area da Quest
        gameObject.SetActive(!deactivateOnMarking);
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Verifica se o player entrou no Trigger
        if(other.tag == "Player"){
            if(markOnEnter){
                MarkQuest();
            }else{
                canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        //Verifica se o Player saiu do Trigger
        if(other.tag == "Player"){
            canMark = false;
        }
    }
}
