using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    public float waitToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se ainda tem de esperar
        if(waitToLoad > 0){
            //Diminui o Tempo
            waitToLoad -= Time.deltaTime;
            //Verifica se ja deu o Tempo
            if(waitToLoad <= 0){
                //Carrega os Dados
                SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
                GameManager.instance.LoadData();
                QuestManager.instance.LoadQuestData();
            }
        }
    }
}
