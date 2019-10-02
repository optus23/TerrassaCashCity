using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public struct Balance_Youis
    {
        public float balance;
    }
    public struct Balance_Alejandro
    {       
        public float balance;
    }
    public struct Balance_Marc
    {      
        public float balance;
    }


    struct CurrentTime
    {
        public string Year;
        public string Month;
        public string Day;
        public int Y;
        public int M;
        public int D;
        public int S;

        public int NextMonth;

        public System.DateTime actual_day;
        public string last_rotate_day;
        public string first_rotate_day;
    }
    CurrentTime current_time;

    public Balance_Youis balance_youis;
    public Balance_Alejandro balance_alejandro;
    public Balance_Marc balance_marc;

    public Text WelcomingText;
    public Text balance_txt;
    public Text nextEntry_txt;
    public Text podium_txt;
    public Text trash_txt;
    public Text cook_txt;

    public float selected_balance;
    public string actual_trasher;
    public static string actual_richer;
    public string next_cash_entry;

    public bool this_month_is_payday;
    public int next_month;

    void Start()
    {

        //DataBase from FireBase





        //  Transform actual day to int
        current_time.Year = System.DateTime.Now.Year.ToString();
        current_time.Month = System.DateTime.Now.Month.ToString();
        current_time.Day = System.DateTime.Now.Day.ToString();

        current_time.Y = int.Parse(current_time.Year);
        current_time.M = int.Parse(current_time.Month);
        current_time.D = int.Parse(current_time.Day);

        //  Calculate next cash entry
        if(current_time.M < 12)
            next_cash_entry = "01/" + (current_time.M + 1) + "/" + current_time.Y;
        else
            next_cash_entry = "01/" + 1 + "/" + (current_time.Y + 1);

        current_time.NextMonth = (current_time.M) + 1;

        //  Assign the actual day
        current_time.actual_day = System.DateTime.Today;
        current_time.last_rotate_day = PlayerPrefs.GetString("last_rotate_day");

        //  Trasher
        actual_trasher = PlayerPrefs.GetString("actual_trasher", "Marc");
        if (actual_trasher == "")
        {
            PlayerPrefs.SetString("actual_trasher", "Marc");
            actual_trasher = "Marc";
        }

        if (CheckToRotateTrash(current_time.actual_day))
        {
            actual_trasher = RotateTrash(actual_trasher);
        }

        //  Balance
        next_month = PlayerPrefs.GetInt("next_month", current_time.M);
        if (!this_month_is_payday && current_time.M == next_month)
        {
            next_month++;
            PlayerPrefs.SetInt("next_month", next_month);
            this_month_is_payday = true;
        }

        if(this_month_is_payday)
        {
            balance_youis.balance = PlayerPrefs.GetFloat("balance_youis");
            balance_alejandro.balance = PlayerPrefs.GetFloat("balance_alejandro");
            balance_marc.balance = PlayerPrefs.GetFloat("balance_marc");
            balance_youis.balance += 5;
            balance_alejandro.balance += 5;
            balance_marc.balance += 5;
            PlayerPrefs.SetFloat("balance_youis", balance_youis.balance);
            PlayerPrefs.SetFloat("balance_alejandro", balance_alejandro.balance);
            PlayerPrefs.SetFloat("balance_marc", balance_marc.balance);
            this_month_is_payday = false;
        }


        if (SelectPerson.selected_person == "Youis")
        {
            balance_youis.balance = PlayerPrefs.GetFloat("balance_youis");
            selected_balance = balance_youis.balance;
            PlayerPrefs.SetFloat("balance_youis", balance_youis.balance);
        }
        else if (SelectPerson.selected_person == "Alejandro")
        {
            balance_alejandro.balance = PlayerPrefs.GetFloat("balance_alejandro");
            selected_balance = balance_alejandro.balance;
            PlayerPrefs.SetFloat("balance_alejandro", balance_alejandro.balance);
        }
        else if (SelectPerson.selected_person == "Marc")
        {
            balance_marc.balance = PlayerPrefs.GetFloat("balance_marc");
            selected_balance = balance_marc.balance;
            PlayerPrefs.SetFloat("balance_marc", balance_marc.balance);                        
        }

        //  Podium
        balance_youis.balance = PlayerPrefs.GetFloat("balance_youis");
        balance_alejandro.balance = PlayerPrefs.GetFloat("balance_alejandro");
        balance_marc.balance = PlayerPrefs.GetFloat("balance_marc");

        if(balance_youis.balance > balance_alejandro.balance && balance_youis.balance > balance_marc.balance)
        {
            actual_richer = "Youis";
        }
        if (balance_alejandro.balance > balance_youis.balance && balance_alejandro.balance > balance_marc.balance)
        {
            actual_richer = "Alejandro";
        }
        if (balance_marc.balance > balance_alejandro.balance && balance_marc.balance > balance_youis.balance)
        {
            actual_richer = "Marc";
        }
        if (balance_marc.balance == balance_alejandro.balance && balance_marc.balance == balance_youis.balance && balance_alejandro.balance == balance_youis.balance)
        {
            actual_richer = "We have the same amount!";
        }
        else if(balance_marc.balance == balance_alejandro.balance && balance_marc.balance > balance_youis.balance)
        {
            actual_richer = "Alejandro or Marc";
        }
        else if(balance_youis.balance == balance_alejandro.balance && balance_youis.balance > balance_marc.balance)
        {
            actual_richer = "Alejandro or Youis";
        }
        else if(balance_youis.balance == balance_marc.balance && balance_youis.balance > balance_alejandro.balance)
        {
            actual_richer = "Marc or Youis";
        }


        //  Output text
        WelcomingText.text = ("Welcome ") + SelectPerson.selected_person + ("!");
        balance_txt.text = ("Your acount balance: " + selected_balance);
        nextEntry_txt.text = ("Next Cash entry: " + next_cash_entry);
        podium_txt.text = ("Has to monetize next Payment: " + System.Environment.NewLine + "-" + actual_richer);
        trash_txt.text = ("Has to take out the Trash:  ") + System.Environment.NewLine + "-" + actual_trasher;
        cook_txt.text = ("Has to serve cooking:  ") + System.Environment.NewLine + "-" + actual_trasher;
    }

    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            balance_youis.balance++;
        }
        if (Input.GetKeyDown("w"))
        {
            balance_alejandro.balance++;
        }
        if (Input.GetKeyDown("e"))
        {
            balance_marc.balance++;
        }

    }

    private bool CheckToRotateTrash(System.DateTime rotate_day)
    {
        if (System.DateTime.Today.DayOfWeek.ToString() == "Monday" && rotate_day.ToString() != current_time.last_rotate_day)
        {
            current_time.last_rotate_day = rotate_day.ToString();
            PlayerPrefs.SetString("last_rotate_day", current_time.last_rotate_day);
            Debug.Log("SETTING STRING TO LAST ROTATE DAY");
            return true;
        }
        return false;
    }

    private string RotateTrash(string trasher)
    {
        if(trasher == "Youis")
        {
            PlayerPrefs.SetString("actual_trasher", "Alejandro");
            return trasher = "Alejandro";
        }
        else if (trasher == "Alejandro")
        {
            PlayerPrefs.SetString("actual_trasher", "Marc");
            return trasher = "Marc";
        }
        else if (trasher == "Marc")
        {
            PlayerPrefs.SetString("actual_trasher", "Youis");
            return trasher = "Youis";
        }
        else
            return trasher = "None";
    }

    public void Return()
    {
        SceneManager.LoadScene("Select Player");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void Historial()
    {
        SceneManager.LoadScene("Historial");
    }

    public void PayCash()
    {
        SceneManager.LoadScene("Pay Cash");
    }
    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("ActualDay", );
    }
}
