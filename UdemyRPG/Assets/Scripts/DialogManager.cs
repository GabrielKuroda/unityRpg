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
        //Verifica se o Box de dialogo está ativo
        if (dialogBox.activeInHierarchy)
        {
            //Verifica se o botão Fire1 foi solto
            if (Input.GetButtonUp("Fire1"))
            {
                //Verifica se é o primeiro click
                if (!justStarted)
                {
                    currentLine++;

                    //Verifica se o Array de frases terminou
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        //Habilita o movimento do Player
                        PlayerController.instance.canMove = true;
                    }
                    else
                    {
                        checkIfName();

                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
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

        checkIfName();

        dialogText.text = dialogLines[currentLine];
        //Habilita DialogBox
        dialogBox.SetActive(true);
        justStarted = true;

        nameBox.SetActive(isPerson);
        //Stop o Player
        PlayerController.instance.canMove = false;
    }

    public void checkIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }
}
