using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPerson : MonoBehaviour
{
    public static string selected_person;
    public string youis;
    public string alejandro;
    public string marc;

    private void Start()
    {
        youis = "Youis";
        alejandro = "Alejandro";
        marc = "Marc";
    }

    public void Youis()
    {
        selected_person = youis;
        SceneManager.LoadScene("Menu");
    }
    public void Alejandro()
    {
        selected_person = alejandro;
        SceneManager.LoadScene("Menu");
    }
    public void Marc()
    {
        selected_person = marc;
        SceneManager.LoadScene("Menu");
    }
}
