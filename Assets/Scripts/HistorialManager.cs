using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistorialManager : MonoBehaviour
{

    public int product_count;
    public List<string> ProductName = new List<string>();
    public List<string> ProductCost = new List<string>();
    public List<string> ProductDate = new List<string>();

    List<Text> ProductName_txt = new List<Text>();
    List<Text> ProductCost_txt = new List<Text>();
    List<Text> ProductDate_txt = new List<Text>();

    public GameObject Historial;

    public Text historial1;
    public Text historial2;
    public Text historial3;

    public float offset;

    void Start()
    {      
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        product_count = PlayerPrefs.GetInt("ProductName_count", 0);
        for (int i = 0; i < product_count; ++i)
        {
            GameObject Name = new GameObject("Name " + i);
            Name.transform.SetParent(Historial.transform);
            GameObject Cost = new GameObject("Cost " + i);
            Cost.transform.SetParent(Historial.transform);
            GameObject Date = new GameObject("Date " + i);
            Date.transform.SetParent(Historial.transform);

            ProductName.Add(PlayerPrefs.GetString("ProductName" + (i), ""));
            ProductCost.Add(PlayerPrefs.GetString("ProductCost" + (i), ""));
            ProductDate.Add(PlayerPrefs.GetString("ProductDate" + (i), ""));

            historial1.text = ProductName[i];
            historial2.text = ProductCost[i];
            historial3.text = ProductDate[i];

            ProductName_txt.Add(historial1);
            ProductCost_txt.Add(historial2);
            ProductDate_txt.Add(historial3);

            ProductName_txt[i] = Name.AddComponent<Text>();
            ProductCost_txt[i] = Cost.AddComponent<Text>();
            ProductDate_txt[i] = Date.AddComponent<Text>();

            ProductName_txt[i].text = historial1.text;
            ProductCost_txt[i].text = historial2.text;
            ProductDate_txt[i].text = historial3.text;

            ProductName_txt[i].font = ArialFont;
            ProductCost_txt[i].font = ArialFont;
            ProductDate_txt[i].font = ArialFont;

            ProductName_txt[i].color = Color.black;
            ProductCost_txt[i].color = Color.black;
            ProductDate_txt[i].color = Color.black;

            ProductName_txt[i].transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
            ProductCost_txt[i].transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
            ProductDate_txt[i].transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);

            ProductName_txt[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130, 60);
            ProductCost_txt[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130, 60);
            ProductDate_txt[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130, 60);

            ProductName_txt[i].transform.localPosition = new Vector2(-26.3f, 98 + (offset * i));
            ProductCost_txt[i].transform.localPosition = new Vector2(44.6f, 98 + (offset * i));
            ProductDate_txt[i].transform.localPosition = new Vector2(114.6f, 98 + (offset * i));
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
