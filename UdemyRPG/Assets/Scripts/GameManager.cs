using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica situação do Menu, Load e Sialogo
        if(gameMenuOpen || dialogActive || fadingBetweenAreas){
            //Impede movimento do Player
            PlayerController.instance.canMove = false;
        }else{
            //Autoriza Movimento do player
            PlayerController.instance.canMove = true;
        }
    }
}
