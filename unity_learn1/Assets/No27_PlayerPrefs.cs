using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class No27_PlayerPrefs : MonoBehaviour
{
    public int id;
    public float exp;
    public int age;
    public string name;

    public Text expText,idText,ageText,nameText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("ID", id);
            PlayerPrefs.SetFloat("Exp", exp);
            PlayerPrefs.SetInt("Age", age);
            PlayerPrefs.SetString("Name", name);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            idText.text = PlayerPrefs.GetInt("ID").ToString();
            expText.text = PlayerPrefs.GetFloat("Exp").ToString();
            ageText.text = PlayerPrefs.GetInt("Age").ToString();
            nameText.text = PlayerPrefs.GetString("Name");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("ID");//É¾³ý
            PlayerPrefs.DeleteAll();//É¾³ýËùÓÐ
        }
    }
}
