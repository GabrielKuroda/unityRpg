using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{

    public string[] lines;

    private bool canActivate;

    public bool isPerson = true;

    public bool ShouldActivateQuest;
    public bool markComplete;
    public string questToMark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o DialogBox pode ser exibido
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy 
            && !GameManager.instance.gameMenuOpen)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            //Ativa a Quest
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Verifica se o objeto que entrou � o Player
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Verifica se o objeto que saiu � o Player
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
