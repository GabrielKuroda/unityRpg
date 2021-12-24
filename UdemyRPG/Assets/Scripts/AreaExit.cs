using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance theEntrance;

    public float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        //Set o mesmo nome desse objeto ao Entrance
        theEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se o player está esperando o Load de cenas
        if (shouldLoadAfterFade)
        {
            //realiza um delay entre os laods
            waitToLoad -= Time.deltaTime;
            //Verifica se o Delay acabou
            if(waitToLoad <= 0)
            {
                //Indica que o load terminou
                shouldLoadAfterFade = false;

                //Carrega a cena
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Verifica se o Objeto possui a tag de player
        if(other.tag == "Player")
        {
            //Indica que o jogo deve carregar a nova cena
            shouldLoadAfterFade = true;

            //Indica que o player está transitando entre areas
            GameManager.instance.fadingBetweenAreas = true;

            //Indica que a Animação de transito deve ser realizada
            UIFade.instance.FadeToBlack();

            //Define a varivael areaTransitionName do Player
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
