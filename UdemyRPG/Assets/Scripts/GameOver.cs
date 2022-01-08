using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string mainMenuScene;
    public string loadGameScene;
    // Start is called before the first frame update
    void Start()
    {
        //Toca a musica
        AudioManager.instance.PlayBgm(4);
        PlayerController.instance.gameObject.SetActive(false);
        GameMenu.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitToMain(){
        //Destroi Todos Objetos do Jogo
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
        //Abre Main Menu
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastSave(){
        //Destroi Objetos
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
        //Carrega tela de Loading
        SceneManager.LoadScene(loadGameScene);
    }
}
