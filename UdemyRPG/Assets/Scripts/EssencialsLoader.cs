using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;

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

        if(GameManager.instance == null){
            Instantiate(gameManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
