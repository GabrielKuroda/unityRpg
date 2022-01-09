using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;
    public GameObject audioManager;
    public GameObject battleMan;

    // Start is called before the first frame update
    void Start()
    {
        //Verifica se a UI n�o existe
        if(UIFade.instance == null)
        {
            //Instancia a UI
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        //Verifica se o player n�o existe
        if (PlayerController.instance == null)
        {
            //Instancia o Player
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        //Verifica se o GameManager não existe
        if (GameManager.instance == null)
        {
            //Instancia o GameManager
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        //Verifica se o AudioManager não existe
        if (AudioManager.instance == null)
        {
            //Instancia o AudioManager
            AudioManager.instance = Instantiate(audioManager).GetComponent<AudioManager>();
        }
        //Verifica se o BattleManager não existe
        if(BattleManager.instance == null)
        {
            //Instancia o BattleManager
            BattleManager.instance = Instantiate(battleMan).GetComponent<BattleManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
