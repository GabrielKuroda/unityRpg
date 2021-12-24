using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;
    private bool justStarted;

    public static DialogManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o Box de dialogo est� ativo
        if (dialogBox.activeInHierarchy)
        {
            //Verifica se o bot�o Fire1 foi solto
            if (Input.GetButtonUp("Fire1"))
            {
                //Verifica se � o primeiro click
                if (!justStarted)
                {
                    currentLine++;

                    //Verifica se o Array de frases terminou
                    if (currentLine >= dialogLines.Length)
                    {
                        //Habilita o Dialogo
                        dialogBox.SetActive(false);

                        //Indica que o Player não está em dialogo
                        GameManager.instance.dialogActive = false;
                    }
                    else
                    {
                        //Verifica se é nome
                        checkIfName();

                        //Display do texto
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    //Indica que não é o primeiro click
                    justStarted = false;
                }
                

            }
        }
    }

    //Disponibiliza o DialogBox para o jogador
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        //Set novas falas
        dialogLines = newLines;
        //Set Inicio
        currentLine = 0;

        //Verifica se é nome
        checkIfName();

        //Mostra o texto
        dialogText.text = dialogLines[currentLine];
        //Habilita DialogBox
        dialogBox.SetActive(true);
        justStarted = true;
        //Mostra a box de nome se o objeto for uma pessoa
        nameBox.SetActive(isPerson);
        //Indica que o player está em dialogo
        GameManager.instance.dialogActive = true;
    }

    public void checkIfName()
    {
        //Se o texto começa co n-, indica que é nome
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            //Pega o nome do NPC ou Player
            nameText.text = dialogLines[currentLine].Replace("n-","");
            // Passa para a proxima linha
            currentLine++;
        }
    }
}
