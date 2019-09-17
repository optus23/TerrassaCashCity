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
        //ProductName[0] = PlayerPrefs.GetString("ProductName" + 0, "");
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
        if(NameInput.text != "" && CostInput.text != "" && DateInput.text != "")
        {
            move_check = true;
           // PlayerPrefs.SetInt("ProductName_count",0);

            product_count =  PlayerPrefs.GetInt("ProductName_count", 0);         
            //for (int i = 0; i < product_count; ++i)
            //{
            //    ProductName[i] = PlayerPrefs.GetString("ProductName" + i, "");
            //    ProductCost[i] = PlayerPrefs.GetString("ProductCost" + i, "");
            //    ProductDate[i] = PlayerPrefs.GetString("ProductDate" + i, "");
            //}

            ProductName.Add(NameInput.text);
            ProductCost.Add(CostInput.text);
            ProductDate.Add(DateInput.text);

            PlayerPrefs.SetInt("ProductName_count", ProductName.Count);

            for (int i = 0; i< ProductName.Count; i++)
            {
                PlayerPrefs.SetString("ProductName" + i, ProductName[i]);
                PlayerPrefs.SetString("ProductCost" + i, ProductCost[i]);
                PlayerPrefs.SetString("ProductDate" + i, ProductDate[i]);
            }

            if(ProductName.Count > 2)
                test.text = ProductName[0] + System.Environment.NewLine + ProductName[1] + System.Environment.NewLine + ProductName[2];

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
        DateInput.text = "";
    }
}
