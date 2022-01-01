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
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log(CheckIfComplete("Quest Test"));
            MarkQuestComplete("Quest Test");
        }

        if(Input.GetKeyDown(KeyCode.O)){
            SaveQuestData();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            LoadQuestData();
        }
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
        //Atualiza Objetos
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark){
        //MArca como incompleta
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        //Atualiza Objetos
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects(){
        //Pega todos os Objetos que contenham o Script QuestObjectActivator
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        //Verifica se há objetos
        if(questObjects.Length > 0){
            for(int i = 0; i < questObjects.Length; i++){
                //Verifica se a Quest está completa
                questObjects[i].CheckCompletion();
            }
        }
    }

    public void SaveQuestData(){
        //Percorre as Quests do Jogo
        for(int i = 0; i < questMarkerNames.Length; i++){
            //Verifica o status da Quest
            if(questMarkersComplete[i]){
                //Salva Quest completa
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i],1);
            }else{
                //Salva Quest incompleta
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i],0);
            }
        }
    }

    public void LoadQuestData(){
        //Percorre as Quests do Jogo
        for(int i = 0; i < questMarkerNames.Length; i++){
            int valueToSet = 0;
            //Verifica se há save para está quest
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i])){
                //Pega o Save
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }
            //Seta o valor do save na quest
            if(valueToSet == 0){
                questMarkersComplete[i] = false;
            }else{
                questMarkersComplete[i] = true;
            }
        }
    }
}
