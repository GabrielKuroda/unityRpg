using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;

    private CharStats[] playerStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se botão foi apertado para abrir/fechar menu
        if(Input.GetButtonDown("Fire2")){
            if(theMenu.activeInHierarchy){
                //Esconde o menu
                theMenu.SetActive(false);
                //Indica que o Menu não está aberto
                GameManager.instance.gameMenuOpen = false;
            }else{
                //Abre Menu
                theMenu.SetActive(true);
                //Indica que o Menu está aberto
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }
}
