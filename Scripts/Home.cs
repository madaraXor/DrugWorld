using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public int selected = 0;
    public string drogue = "Shit";
    public float gramme;
    public GameObject[] buttons;
    public GameObject buttonDetailler;
    public GameObject slider;
    public GameObject affGramme;
    public GameObject dropdown;

    // Inventaire
    public Inventaire inv;

    void Start()
    {
        inv = GameObject.Find("Inventaire").GetComponent<Inventaire>();
    }

    public void ButtonDetailler()
    {
        buttonDetailler.SetActive(false);
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        dropdown.SetActive(true);
    }

    public void ButtonDetaillerFermer()
    {
        buttonDetailler.SetActive(true);
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        affGramme.SetActive(false);
        slider.SetActive(false);
        dropdown.SetActive(false);
    }

    public void SelectQuantity(int quantite)
    {
        selected = quantite;
        affGramme.SetActive(true);
        slider.SetActive(true);
        if (quantite == 10)
        {
            slider.GetComponent<Slider>().minValue = 12;
            slider.GetComponent<Slider>().maxValue = 24;
        }
        if (quantite == 20)
        {
            slider.GetComponent<Slider>().minValue = 12*2;
            slider.GetComponent<Slider>().maxValue = 24*2;
        }
        if (quantite == 30)
        {
            slider.GetComponent<Slider>().minValue = 12*3;
            slider.GetComponent<Slider>().maxValue = 24*3;
        }
    }

    public void SetValueSlider()
    {
        float newValue = slider.GetComponent<Slider>().value / 10;
        gramme = newValue;
        affGramme.GetComponent<TextMeshProUGUI>().text = newValue.ToString() + "g";
    }

    public void SetValueDropdown()
    {
        if (dropdown.GetComponent<TMP_Dropdown>().value == 0)
        {
            drogue = "Shit";
        }
        if (dropdown.GetComponent<TMP_Dropdown>().value == 1)
        {
            drogue = "Beuh";
        }
    }

    public void Valider()
    {
        float nb_drogue = 0;
        foreach (KeyValuePair<string, float> item in inv.listeObjet)
        {
            if (item.Key.StartsWith(drogue))
            {
                nb_drogue = item.Value;
            }
        }
        if (nb_drogue==0 || nb_drogue < gramme)
        {
            Debug.Log("pas asser de " + drogue);
            return;
        }
        inv.Add(selected + drogue + "(" + gramme + ")", 1);
        inv.Remove(drogue, gramme);
    }
}

