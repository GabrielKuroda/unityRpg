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
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
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
            
            shouldLoadAfterFade = true;
            UIFade.instance.FadeToBlack();

            //Define a varivael areaTransitionName do Player
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
