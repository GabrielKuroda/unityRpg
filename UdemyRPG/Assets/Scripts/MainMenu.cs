using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    public GameObject continueButton;

    public string loadGameScene;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Current_Scene")){
            continueButton.SetActive(true);
        }else{
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue(){
        //Abre a tela de Loading
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame(){
        //Carrega a Cena
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit(){
        //Fecha o Jogo
        Application.Quit();
    }
}
