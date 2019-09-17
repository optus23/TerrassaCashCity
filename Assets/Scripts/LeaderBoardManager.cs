using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{

    public Text richer_txt;
    public Text youis_txt;
    public Text alejandro_txt;
    public Text marc_txt;

    private float youis_balance;
    private float alejandro_balance;
    private float marc_balance;

    // Start is called before the first frame update
    void Start()
    {
        richer_txt.text = "Richer: " + System.Environment.NewLine + MenuManager.actual_richer;

        youis_balance = PlayerPrefs.GetFloat("balance_youis");
        alejandro_balance = PlayerPrefs.GetFloat("balance_alejandro");
        marc_balance = PlayerPrefs.GetFloat("balance_marc");

        youis_txt.text = "Youis Cash: " + youis_balance;
        alejandro_txt.text = "Alejandro Cash: " + alejandro_balance;
        marc_txt.text = "Marc Cash: " + marc_balance;
    }

   public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
