using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerNames;
    public bool[] questMarkersComplete;
    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Instancia com o mesmo tamanho do Names
        questMarkersComplete = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetQuestNumber(string questToFind){
        //percorre a array de quests
        for(int i = 0; i < questMarkerNames.Length; i++){
            //Verifica se o nome bate
            if(questMarkerNames[i] == questToFind){
                return i;
            }
        }

        Debug.LogError("Quest "+ questToFind + " Does Not Exists!");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck){
        //Verifica se está em quests
        if(GetQuestNumber(questToCheck) != 0){
            //Verifica se está completo
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        //MArca a quest como Completa
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
    }

    public void MarkQuestIncomplete(string questToMark){
        //MArca como incompleta
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
    }
}
