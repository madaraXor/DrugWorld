using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public string name = "none";
    public float value = 0;
    public TextMeshProUGUI affSlot;

    void Awake()
    {
        affSlot = transform.FindChild("AffSlot").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetAff()
    {
        // Formatage de l'affichage
        string aff = "";
        aff = string.Format("{0} :\n{1}", name, value);
        // Affichage
        affSlot.text = aff;
    }

}
