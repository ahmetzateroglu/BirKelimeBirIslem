using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static int zorluk;

    public Dropdown dropdown;

    
    public GameObject ayarlarpanel;  
    public GameObject puanpanel;
    public GameObject islemPanel;

    public Text totalpuantext;
    public Text rekabetciskortext;
    public static int totalpuan;
    public static int rekabetciskor;

    public static string mod;



    void Start()
    {

        ayarlarpanel.SetActive(false);       
        puanpanel.SetActive(false);
        islemPanel.SetActive(false);


        if (PlayerPrefs.HasKey("zorluk")) 
        {
            zorluk = PlayerPrefs.GetInt("zorluk");
        }
        else 
        {
            PlayerPrefs.SetInt("zorluk", 0);
        }
        
         dropdown.value = zorluk;


        
        if (PlayerPrefs.HasKey("totalpuan")) 
        {
            totalpuan = PlayerPrefs.GetInt("totalpuan"); 
            totalpuantext.text = totalpuan.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("totalpuan", 0);
            totalpuantext.text = PlayerPrefs.GetInt("totalpuan").ToString();
        }


        if (PlayerPrefs.HasKey("rekabetciskor")) 
        {
            rekabetciskor = PlayerPrefs.GetInt("rekabetciskor"); 
            rekabetciskortext.text = rekabetciskor.ToString(); 
        }
        else 
        {
            PlayerPrefs.SetInt("rekabetciskor", 0);
            rekabetciskortext.text = PlayerPrefs.GetInt("rekabetciskor").ToString();
        }



    }


    void Update()
    {
        
    }

    public void ModSec(string modsec)
    {
        mod = modsec; 
    }


    public void ZorlukSec(int deger)
    {

        if (deger == 0)
        {
            zorluk = 0;
            PlayerPrefs.SetInt("zorluk", 0);

        }
        if (deger == 1)
        {
            zorluk = 1;
            PlayerPrefs.SetInt("zorluk", 1);
        }
        if (deger == 2)
        {
            zorluk = 2;
            PlayerPrefs.SetInt("zorluk", 2);
        }
    }

    public void PanelAyarlari(int x)
    {
        if (x == 0)
        {
            ayarlarpanel.SetActive(true);

        }
        if (x == 1)
        {
            ayarlarpanel.SetActive(false);
        }
        if (x == 2)
        {
            puanpanel.SetActive(true);
        }
        if (x == 3)
        {
            puanpanel.SetActive(false);
        }
        if (x == 4)
        {
            islemPanel.SetActive(true);
        }
        if (x == 5)
        {
            islemPanel.SetActive(false);
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }
    public void PlayGame2()
    {
        SceneManager.LoadScene(2);
    }


    public void QuitGame()
    {
        Debug.Log("Çýktýk");
        Application.Quit(); 
    }






}
