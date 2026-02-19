using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownTest : MonoBehaviour
{
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        List<Dropdown.OptionData> list = dropdown.options;
        list.Add(new Dropdown.OptionData("ELuoSi"));
        dropdown.options = list;//Ìí¼ÓÑ¡Ïî
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
