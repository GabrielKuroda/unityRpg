using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        //Verifica se o nome da transa��o do Objeto linkado ao script � o mesmo que se encontra no Player
        if(transitionName == PlayerController.instance.areaTransitionName)
        {
            //Set na posi��o do Player na mesma pos que o objeto do script
            PlayerController.instance.transform.position = transform.position;
        }

        UIFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
