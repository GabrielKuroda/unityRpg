using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    public GameObject objectToActivate;
    public string questToCheck;
    public bool activeIfComplete;
    private bool inicialCheckDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inicialCheckDone){
            inicialCheckDone = true;
    
            CheckCompletion();
        }

    }

    public void CheckCompletion(){
        //Verifica se a Quest foi completada
        if(QuestManager.instance.CheckIfComplete(questToCheck)){
            //Ativa o Objeto
            objectToActivate.SetActive(activeIfComplete);
        }
    }
}
