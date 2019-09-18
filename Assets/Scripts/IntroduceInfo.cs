using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroduceInfo : MonoBehaviour
{
    public InputField NameInput;
    public InputField CostInput;
    public InputField DateInput;

    public List<string> ProductName = new List<string>();
    public List<string> ProductCost = new List<string>();
    public List<string> ProductDate = new List<string>();
    public List<string> ProductPerson = new List<string>();

    public GameObject Check;
    public bool move_check;
    public float velocity;
    public float timer_check;
    public float initial_position_x;

    public Text test;
    public int product_count;

    // Start is called before the first frame update
    void Start()
    {
        initial_position_x = Check.transform.position.x;    
    }

    // Update is called once per frame
    void Update()
    {
        if(move_check && timer_check < 2f)
        {
            timer_check += Time.deltaTime;

            if (Check.transform.position.x > 1.9f)
            {
                Check.transform.position = new Vector2(Check.transform.position.x - velocity * Time.deltaTime, Check.transform.position.y);
            }
        }
        else if(Check.transform.position.x < initial_position_x)
        {
            move_check = false;
            timer_check = 0;
            Check.transform.position = new Vector2(Check.transform.position.x + velocity * Time.deltaTime, Check.transform.position.y);
            ResetInputs();
        }
    }

    public void Continue()
    {
        if(NameInput.text != "Enter text..." && CostInput.text != "Enter text..." && DateInput.text != "(xx/yy/zzz)")
        {
            move_check = true;

            product_count =  PlayerPrefs.GetInt("ProductName_count", 0);
            for (int i = 0; i < product_count; ++i)
            {
                ProductName.Add(PlayerPrefs.GetString("ProductName" + (i), ""));
                ProductCost.Add(PlayerPrefs.GetString("ProductCost" + (i), ""));
                ProductDate.Add(PlayerPrefs.GetString("ProductDate" + (i), ""));
                ProductPerson.Add(PlayerPrefs.GetString("ProductPerson" + (i), ""));
            }

            ProductName.Add(NameInput.text);
            ProductCost.Add(CostInput.text);
            ProductDate.Add(DateInput.text);
            ProductPerson.Add(SelectPerson.selected_person);

            PlayerPrefs.SetInt("ProductName_count", ProductName.Count);

            for (int i = 0; i < ProductName.Count; i++)
            {
                PlayerPrefs.SetString("ProductName" + i, ProductName[i]);
                PlayerPrefs.SetString("ProductCost" + i, ProductCost[i]);
                PlayerPrefs.SetString("ProductDate" + i, ProductDate[i]);
                PlayerPrefs.SetString("ProductPerson" + i, ProductPerson[i]);
            }
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetInputs()
    {
        NameInput.text = "";
        CostInput.text = "";
        DateInput.text = "(xx/yy/zzz)";
    }
}
