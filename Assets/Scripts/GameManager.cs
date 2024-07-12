using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{


    int sayi1, sayi2, sayi3, sayi4, sayi5, sayi6, hedef;
    
    public Text sayi1Text, sayi2Text, sayi3Text, sayi4Text, sayi5Text, sayi6Text, hedefText;


    public Text islemlerText;
    public GameObject islemlerPaneli, testPanel, rekabetciPanel;

    public Button button1, button2, button3, button4, button5, button6, toplamaButton, cikarmaButton, carpmaButton, bolmeButton, cevabiGorButton, onaylaButton, yeniSoruButton;
    public Color beyazRenk, sariRenk, yesilRenk, kirmiziRenk, inaktifRenk;

    public  float sure=30;
    public float uyarýtemizle = 0;

    public Text puanText, sureText, uyariText;
    public int suredurdur=0;

    int sayia = 0;
    int sayib = 0;
    int puan = 0;
    int ilksayi = 0;
    string iþlem = "yok";

    string mod; int soruSayisi;

    // Geri tuþu için
    Stack<int> ilkButton = new Stack<int>(); 
    Stack<int> ikinciButton = new Stack<int>();
    Stack<int> eskiDeger = new Stack<int>();

    int cevapUretildiMi;

    public Text panelhedefText, panel1Text, panel2Text, panel3Text, panel4Text, panel5Text, panel6Text;

    public Text testPanelPuan, testPanelOrtalama;

    public Text rekabetciPanelPuan, rekabetciPanelOrtalama, rekabetciPanelSoruSayisi, rekabetciPanelSkor;

    void Start()
    {
        soruSayisi = 1;

        islemlerPaneli.SetActive(false);
        testPanel.SetActive(false);
        rekabetciPanel.SetActive(false);
        SoruEkle();
     

    }


    void Update()
    {
        if (uyarýtemizle > -10)  
            uyarýtemizle -= Time.deltaTime;

        if (uyarýtemizle <= 0 && sure>=0 && suredurdur != 1)  
            uyariText.text = "";
     
        if (sure > 0 && suredurdur != 1)                                                                      
        {
            sure -= Time.deltaTime;
            sureText.text = sure.ToString("00");
        }

        if (sure <= 0 && suredurdur == 0) 
        {

            suredurdur = 1;
            puan -= 10;
            MainMenu.totalpuan -= 10;
            puanText.text = puan.ToString();

            onaylaButton.enabled = false; 
            onaylaButton.image.color = inaktifRenk;
            Onayla();

            cevabiGorButton.enabled = true;
            cevabiGorButton.image.color = beyazRenk;
            yeniSoruButton.enabled = true;
            yeniSoruButton.image.color = beyazRenk;


        }



    }

    public void SoruEkle()
    {

        if (MainMenu.mod == "test") 
        {


            if (soruSayisi <= 3)
            {
                MainMenu.zorluk = 0;
                SoruUret();
            }
            else if (soruSayisi > 3 && soruSayisi <= 5)
            {
                MainMenu.zorluk = 1;
                SoruUret();
            }
            else if (soruSayisi == 6)
            {
                MainMenu.zorluk = 2;
                SoruUret();
            }
            else if (soruSayisi > 6)
            {
                Debug.Log("Test Bitti");
                testPanelPuan.text = puan.ToString();
                testPanelOrtalama.text = (puan / (soruSayisi-1)).ToString();

                testPanel.SetActive(true);
            }

           


        }
        else if (MainMenu.mod == "rekabetci")
        {

            if (soruSayisi <= 2)
            {
                MainMenu.zorluk = 0;
            }
            else if (soruSayisi > 3 && soruSayisi <= 6)
            {
                MainMenu.zorluk = 1;
            }
            else if (soruSayisi > 6)
            {
                MainMenu.zorluk = 2;
            }

            SoruUret();

        }
        else if (MainMenu.mod == "normal")
        {
            SoruUret();
        }



    }



    public void SoruUret() 
    {
       
        
        Sifirla();  

        cevabiGorButton.enabled = false;
        cevabiGorButton.image.color = inaktifRenk;
        yeniSoruButton.enabled = false;
        yeniSoruButton.image.color = inaktifRenk;

        sure = 60;
        suredurdur = 0;


        do 
        {
            if (MainMenu.zorluk == 0)  
            {


                sayi1 = Random.Range(1, 6);  
                sayi2 = Random.Range(1, 6);
                sayi3 = Random.Range(1, 6);
                sayi4 = Random.Range(1, 6);
                sayi5 = Random.Range(1, 6);
                sayi6 = (Random.Range(10, 40) / 10) * 10;  
                hedef = Random.Range(120, 250);
            }
            else if (MainMenu.zorluk == 1)
            {
                sayi1 = Random.Range(1, 10);
                sayi2 = Random.Range(1, 10);
                sayi3 = Random.Range(1, 10);
                sayi4 = Random.Range(1, 10);
                sayi5 = Random.Range(1, 10);
                sayi6 = (Random.Range(20, 65) / 10) * 10;  
                hedef = Random.Range(300, 550);
            }
            else if (MainMenu.zorluk == 2)
            {
                sayi1 = Random.Range(1, 10);
                sayi2 = Random.Range(1, 10);
                sayi3 = Random.Range(1, 10);
                sayi4 = Random.Range(1, 10);
                sayi5 = Random.Range(1, 10);
                sayi6 = (Random.Range(10, 85) / 10) * 10;   
                hedef = Random.Range(550, 999);

            }

            Hesapla();

        } while (cevapUretildiMi!=hedef);
        

        sayi1Text.text = sayi1.ToString();
        sayi2Text.text = sayi2.ToString();
        sayi3Text.text = sayi3.ToString();
        sayi4Text.text = sayi4.ToString();
        sayi5Text.text = sayi5.ToString();
        sayi6Text.text = sayi6.ToString();
        hedefText.text = hedef.ToString();

        

        onaylaButton.enabled = true; 
        onaylaButton.image.color = beyazRenk;

        ilkButton.Clear(); 
        ikinciButton.Clear();
        eskiDeger.Clear();

        soruSayisi += 1;


        
    }

    public void Onayla()  
    {

        suredurdur = 1;

        int yakýnlýk;

        yakýnlýk = Mathf.Abs(int.Parse(sayi1Text.text) - hedef);

        if ((Mathf.Abs(int.Parse(sayi2Text.text) - hedef)) < yakýnlýk)
            yakýnlýk = Mathf.Abs(int.Parse(sayi2Text.text) - hedef);
        if ((Mathf.Abs(int.Parse(sayi3Text.text) - hedef)) < yakýnlýk)
            yakýnlýk = Mathf.Abs(int.Parse(sayi3Text.text) - hedef);
        if ((Mathf.Abs(int.Parse(sayi4Text.text) - hedef)) < yakýnlýk)
            yakýnlýk = Mathf.Abs(int.Parse(sayi4Text.text) - hedef);
        if ((Mathf.Abs(int.Parse(sayi5Text.text) - hedef)) < yakýnlýk)
            yakýnlýk = Mathf.Abs(int.Parse(sayi5Text.text) - hedef);
        if ((Mathf.Abs(int.Parse(sayi6Text.text) - hedef)) < yakýnlýk)
            yakýnlýk = Mathf.Abs(int.Parse(sayi6Text.text) - hedef);

        if (sure > 0)  
        {
            if (yakýnlýk == 0)
            {
                MainMenu.totalpuan += 10;
                puan = puan + 10;
                uyariText.text = (" Hedef Bulundu \n + 10 Puan");
                puanText.text = puan.ToString();

            }
            else if (yakýnlýk > 0 && yakýnlýk <= 3)  
            {

                puan = puan + 8;
                MainMenu.totalpuan += 8;
                uyariText.text = (" Þu kadar yaklaþtýnýz: " + yakýnlýk + " \n + 8 Puan");
                puanText.text = puan.ToString();
            }
            else if (yakýnlýk > 3 && yakýnlýk <= 10)
            {
                puan = puan + 6;
                MainMenu.totalpuan += 6;
                uyariText.text = (" Þu kadar yaklaþtýnýz: " + yakýnlýk + " \n + 6 Puan");
                puanText.text = puan.ToString();
            }
            else if (yakýnlýk > 10 && yakýnlýk <= 20)
            {
                puan = puan + 4;
                MainMenu.totalpuan += 4;
                uyariText.text = (" Þu kadar yaklaþtýnýz: " + yakýnlýk + " \n + 4 Puan");
                puanText.text = puan.ToString();
            }
            else if (yakýnlýk > 20 && yakýnlýk <= 30)
            {
                puan = puan + 2;
                MainMenu.totalpuan += 2;
                uyariText.text = (" Þu kadar yaklaþtýnýz: " + yakýnlýk + " \n + 2 Puan");
                puanText.text = puan.ToString();
            }
            else if (yakýnlýk > 30) 
            {

                MainMenu.totalpuan -= 10;
                puan -= 10;
                puanText.text = puan.ToString();

                if (MainMenu.mod == "rekabetci")  
                {
                    Debug.Log("Kaybetiniz");
                    RekabetciBitis();
                }
                else
                {
                    uyariText.text = (" Hedefe Yeterince Yaklaþamadýnýz -10 Puan \n Þu kadar yaklaþtýnýz: " + yakýnlýk);
                   
                }

               

                
            }

            

        }
        else
        {

            if (MainMenu.mod == "rekabetci")  
            {
                Debug.Log("Kaybetiniz");
                RekabetciBitis();
            }
            else
            {
                uyariText.text = (" Süre Doldu -10 Puan \n Yeni Soruya Geçiniz.");
            }
        }

        onaylaButton.enabled = false;
        onaylaButton.image.color = inaktifRenk;

        cevabiGorButton.enabled = true; 
        cevabiGorButton.image.color = beyazRenk;
        yeniSoruButton.enabled = true; 
        yeniSoruButton.image.color = beyazRenk;

        PlayerPrefs.SetInt("totalpuan", MainMenu.totalpuan);


    }

    public void RekabetciBitis()
    {

        
        rekabetciPanelSoruSayisi.text = (soruSayisi-1).ToString();
        rekabetciPanelPuan.text = puan.ToString();
        rekabetciPanelOrtalama.text = (puan / (soruSayisi - 1)).ToString();

        if(puan > MainMenu.rekabetciskor)
        {
            Debug.Log("Yeni Rekor");
            PlayerPrefs.SetInt("rekabetciskor", puan);
            rekabetciPanelSkor.text = puan.ToString();
        }
        else
        {
            rekabetciPanelSkor.text = (MainMenu.rekabetciskor).ToString();
        }

        rekabetciPanel.SetActive(true);

    }




    public void GeriTusu()
    {

        


        if(iþlem=="yok")
        {
            if ((eskiDeger.Count) != 0) 
            {

                if (ilkButton.Peek() == 1)
                {
                    button1.enabled = true;
                    ButtonSec(1);
                    ilkButton.Pop();
                }
                else if (ilkButton.Peek() == 2)
                {
                    button2.enabled = true;
                    ButtonSec(2);
                    ilkButton.Pop();
                }
                else if (ilkButton.Peek() == 3)
                {
                    button3.enabled = true;
                    ButtonSec(3);
                    ilkButton.Pop();
                }
                else if (ilkButton.Peek() == 4)
                {
                    button4.enabled = true;
                    ButtonSec(4);
                    ilkButton.Pop();
                }
                else if (ilkButton.Peek() == 5)
                {
                    button5.enabled = true;
                    ButtonSec(5);
                    ilkButton.Pop();
                }
                else if (ilkButton.Peek() == 6)
                {
                    button6.enabled = true;
                    ButtonSec(6);
                    ilkButton.Pop();
                }


                if (ikinciButton.Peek() == 1)
                {
                    button1.image.color = beyazRenk;
                    sayi1Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }
                else if (ikinciButton.Peek() == 2)
                {
                    button2.image.color = beyazRenk;
                    sayi2Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }
                else if (ikinciButton.Peek() == 3)
                {
                    button3.image.color = beyazRenk;
                    sayi3Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }
                else if (ikinciButton.Peek() == 4)
                {
                    button4.image.color = beyazRenk;
                    sayi4Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }
                else if (ikinciButton.Peek() == 5)
                {
                    button5.image.color = beyazRenk;
                    sayi5Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }
                else if (ikinciButton.Peek() == 6)
                {
                    button6.image.color = beyazRenk;
                    sayi6Text.text = eskiDeger.Peek().ToString();
                    eskiDeger.Pop();
                    ikinciButton.Pop();
                }

            }
            else
            {
               // Debug.Log("boþ");
            }

        }
        else
        {
            iþlem = "yok";
            toplamaButton.image.color = beyazRenk;
            cikarmaButton.image.color = beyazRenk;
            carpmaButton.image.color = beyazRenk;
            bolmeButton.image.color = beyazRenk;
        }
        

      



    }

   


    public void IþlemYap(int button)
    {


        toplamaButton.image.color = beyazRenk;
        cikarmaButton.image.color = beyazRenk;
        carpmaButton.image.color = beyazRenk;
        bolmeButton.image.color = beyazRenk;


        int sonuç = 0;
        if (iþlem == "topla")
        {

            sonuç = (sayia + sayib);

            sayia = 0;
            sayib = 0;
            iþlem = "yok";
            EnabledYap(ilksayi);
            uyarýtemizle = 0;   
        }
        if (iþlem == "cikar")   
        {
            if (sayia - sayib <= 0)
            {

                uyariText.text = "Sonuç Negatif veya Sýfýr Tekrar Deneyiniz";
                uyarýtemizle = 10;
                button = -1;
                sayib = 0;
                iþlem = "yok";

                ikinciButton.Pop();
                eskiDeger.Pop();


            }
            else
            {
                sonuç = (sayia - sayib);
                sayia = 0;
                sayib = 0;
                iþlem = "yok";
                EnabledYap(ilksayi);
                uyarýtemizle = 0;
            }

        }
        if (iþlem == "carp")
        {
            sonuç = (sayia * sayib);
            sayia = 0;
            sayib = 0;
            iþlem = "yok";
            EnabledYap(ilksayi);
            uyarýtemizle = 0;
        }
        if (iþlem == "bol")
        {

            if (sayia % sayib != 0)
            {

                uyariText.text = "Sayýlar Tam Bölünmüyor Tekrar Deneyiniz";
                uyarýtemizle = 10;
                button = -1;
                sayib = 0;
                iþlem = "yok";


                ikinciButton.Pop();
                eskiDeger.Pop();

            }
            else
            {
                sonuç = (sayia / sayib);
                sayia = 0;
                sayib = 0;
                iþlem = "yok";
                EnabledYap(ilksayi);
                uyarýtemizle = 0;
            }

        }



        if (button == 1)
        {
            sayi1Text.text = (sonuç).ToString();
            IlkSayiAyarla(1);
            sayia = int.Parse(sayi1Text.text);     
        }
        if (button == 2)
        {
            sayi2Text.text = (sonuç).ToString();
            IlkSayiAyarla(2);
            sayia = int.Parse(sayi2Text.text);
        }
        if (button == 3)
        {
            sayi3Text.text = (sonuç).ToString();
            IlkSayiAyarla(3);
            sayia = int.Parse(sayi3Text.text);
        }
        if (button == 4)
        {
            sayi4Text.text = (sonuç).ToString();
            IlkSayiAyarla(4);
            sayia = int.Parse(sayi4Text.text);
        }
        if (button == 5)
        {
            sayi5Text.text = (sonuç).ToString();
            IlkSayiAyarla(5);
            sayia = int.Parse(sayi5Text.text);
        }
        if (button == 6)
        {
            sayi6Text.text = (sonuç).ToString();
            IlkSayiAyarla(6);
            sayia = int.Parse(sayi6Text.text);
        }

    }

    public void ButtonSec(int x)
    {

        if (iþlem == "yok")
        {


            if (x == 1)
                sayia = int.Parse(sayi1Text.text);
            if (x == 2)
                sayia = int.Parse(sayi2Text.text);
            if (x == 3)
                sayia = int.Parse(sayi3Text.text);
            if (x == 4)
                sayia = int.Parse(sayi4Text.text);
            if (x == 5)
                sayia = int.Parse(sayi5Text.text);
            if (x == 6)
                sayia = int.Parse(sayi6Text.text);

            if (ilksayi != x)
            {
                IlkSayiAyarla(x);
            }


        }

        if (sayia != 0 && iþlem != "yok")
        {

            if (x == 1)  
            {
                sayib = int.Parse(sayi1Text.text);
                ikinciButton.Push(1);                     
                eskiDeger.Push(sayib);
            }

            if (x == 2)
            {
                sayib = int.Parse(sayi2Text.text);
                ikinciButton.Push(2);
                eskiDeger.Push(sayib);
            }
            if (x == 3)
            {
                sayib = int.Parse(sayi3Text.text);
                ikinciButton.Push(3);
                eskiDeger.Push(sayib);
            }
            if (x == 4)
            {
                sayib = int.Parse(sayi4Text.text);
                ikinciButton.Push(4);
                eskiDeger.Push(sayib);
            }
            if (x == 5)
            {
                sayib = int.Parse(sayi5Text.text);
                ikinciButton.Push(5);
                eskiDeger.Push(sayib);
            }
            if (x == 6)
            {
                sayib = int.Parse(sayi6Text.text);
                ikinciButton.Push(6);
                eskiDeger.Push(sayib);

            }

            IþlemYap(x);

        }
    }



    public void EnabledYap(int ilkiþlem)
    {


        if (ilkiþlem == 1)
        {
            button1.enabled = false;
            button1.image.color = kirmiziRenk;
            ilkButton.Push(1);

        }
        if (ilkiþlem == 2)
        {
            button2.enabled = false;
            button2.image.color = kirmiziRenk;
            ilkButton.Push(2);

        }
        if (ilkiþlem == 3)
        {
            button3.enabled = false;
            button3.image.color = kirmiziRenk;
            ilkButton.Push(3);
        }
        if (ilkiþlem == 4)
        {
            button4.enabled = false;
            button4.image.color = kirmiziRenk;
            ilkButton.Push(4);
        }
        if (ilkiþlem == 5)
        {
            button5.enabled = false;
            button5.image.color = kirmiziRenk;
            ilkButton.Push(5);
        }
        if (ilkiþlem == 6)
        {
            button6.enabled = false;
            button6.image.color = kirmiziRenk;
            ilkButton.Push(6);
        }


    }

    public void Sifirla()  
    {


        sayi1Text.text = sayi1.ToString();
        sayi2Text.text = sayi2.ToString();
        sayi3Text.text = sayi3.ToString();
        sayi4Text.text = sayi4.ToString();
        sayi5Text.text = sayi5.ToString();
        sayi6Text.text = sayi6.ToString();

        button1.image.color = beyazRenk;
        button2.image.color = beyazRenk;
        button3.image.color = beyazRenk;
        button4.image.color = beyazRenk;
        button5.image.color = beyazRenk;
        button6.image.color = beyazRenk;

        button1.enabled = true;
        button2.enabled = true;
        button3.enabled = true;
        button4.enabled = true;
        button5.enabled = true;
        button6.enabled = true;

        toplamaButton.image.color = beyazRenk;
        cikarmaButton.image.color = beyazRenk;
        carpmaButton.image.color = beyazRenk;
        bolmeButton.image.color = beyazRenk;


        ilksayi = 0;  
        iþlem = "yok";
        sayia = 0;
        sayib = 0;

        ilkButton.Clear(); 
        ikinciButton.Clear();
        eskiDeger.Clear();

       
      

    }


    
    public void IslemSec(string x)
    {
       
        if(sayia !=0)
        {
            iþlem = x;
            if(x=="topla")
            {
                toplamaButton.image.color = yesilRenk;
                cikarmaButton.image.color = beyazRenk;
                carpmaButton.image.color = beyazRenk;
                bolmeButton.image.color = beyazRenk;
            }
            if (x == "cikar")
            {
                cikarmaButton.image.color = yesilRenk;
                toplamaButton.image.color = beyazRenk;
                carpmaButton.image.color = beyazRenk;
                bolmeButton.image.color = beyazRenk;

            }
            if (x == "carp")
            {
                carpmaButton.image.color = yesilRenk;
                cikarmaButton.image.color = beyazRenk;
                toplamaButton.image.color = beyazRenk;
                bolmeButton.image.color = beyazRenk;

            }
            if (x == "bol")
            {
                bolmeButton.image.color = yesilRenk;
                cikarmaButton.image.color = beyazRenk;
                carpmaButton.image.color = beyazRenk;
                toplamaButton.image.color = beyazRenk;

            }
        }


    }

   

    public void IlkSayiAyarla(int x)
    {

        int a;
        a = ilksayi;
        ilksayi = x;

        if (a == 1 && a != x && button1.enabled != false)
        {
            button1.image.color = beyazRenk;
        }
        if (a == 2 && a != x && button2.enabled != false)
        {
            button2.image.color = beyazRenk;
        }
        if (a == 3 && a != x && button3.enabled != false)
        {
            button3.image.color = beyazRenk;
        }
        if (a == 4 && a != x && button4.enabled != false)
        {
            button4.image.color = beyazRenk;
        }
        if (a == 5 && a != x && button5.enabled != false)
        {
            button5.image.color = beyazRenk;
        }
        if (a == 6 && a != x && button6.enabled != false)
        {
            button6.image.color = beyazRenk;
        }


        if (ilksayi == 1)
        {
            button1.image.color = yesilRenk;
        }
        if (ilksayi == 2)
        {
            button2.image.color = yesilRenk;
        }
        if (ilksayi == 3)
        {
            button3.image.color = yesilRenk;
        }
        if (ilksayi == 4)
        {
            button4.image.color = yesilRenk;
        }
        if (ilksayi == 5)
        {
            button5.image.color = yesilRenk;
        }
        if (ilksayi == 6)
        {
            button6.image.color = yesilRenk;
        }




    }




    public void TestAnaMenuDon() 
    {
       
        SceneManager.LoadScene(0); 
    }

    public void YeniTest() 
    {
        SceneManager.LoadScene(1);
    }



    public void PanelYonetimi(int x)
    {

        if(x==0)
        {
            islemlerPaneli.SetActive(true);
            
        }
        if(x==1)
        {
            islemlerPaneli.SetActive(false);
        }

       


    }




    public class Islem
    {

        public string output { get; set; }
        public bool success { get; set; }
        public int target { get; set; }

    }
    
    public void Hesapla()
    {

        int[] sayilar = new int[] { sayi1, sayi2, sayi3, sayi4, sayi5, sayi6, hedef };


        List<int> list = new List<int>(new[] { sayi1, sayi2, sayi3, sayi4, sayi5, sayi6 });

        foreach (int item in sayilar)
        {
            List<int> runList = new List<int>(list);
            runList.Remove(item);

            Islem islemler = getOperations(runList, item, hedef);


            if (islemler.success)
            {

                Yazdir((item + islemler.output));   /////////////////

                return;
            }
        }                              
        Islem getOperations(List<int> numbers, int midNumber, int target)
        {
            Islem midResult = new Islem();

            if (midNumber == target)
            {
                midResult.success = true;
                midResult.output = "";
                midResult.target += midNumber;
                return midResult;
            }
            foreach (var number in numbers)
            {
                List<int> newList = new List<int>(numbers);

                newList.Remove(number);
       
                if (newList.Count == 0)
                {
                   
                    
                    if (midNumber - number == target)   
                    {                                              
                        midResult.success = true;                 
                        midResult.output = " - " + number;
                        midResult.target += (midNumber - number);
                        return midResult;
                    }
                    if (midNumber + number == target)  
                    {
                        midResult.success = true;
                        midResult.output = " + " + number;
                        midResult.target += (midNumber + number);
                        return midResult;
                    }
                    if (midNumber * number == target)
                    {
                        midResult.success = true;
                        midResult.output = " * " + number;
                        midResult.target += (midNumber * number);
                        return midResult;
                    }
                    if (midNumber / number == target)
                    {
                        midResult.success = true;
                        midResult.output = " / " + number;
                        midResult.target += (midNumber / number);
                        return midResult;
                    }
                    midResult.success = false;
                    midResult.output = "f" + number;
                    return midResult;
                }
                else
                {
                    if (midNumber - number>0)   
                    midResult = getOperations(newList, midNumber - number, target);
                    if (midResult.success)
                    {                      
                        midResult.output = " - " + number + midResult.output; 
                        return midResult;
                    }
                    midResult = getOperations(newList, midNumber + number, target);
                    if (midResult.success)
                    {
                        midResult.output = " + " + number + midResult.output; //
                        return midResult;
                    }
                    midResult = getOperations(newList, midNumber * number, target);
                    if (midResult.success)
                    {
                        midResult.output = " * " + number + midResult.output; //
                        return midResult;
                    }
                    midResult = getOperations(newList, midNumber / number, target);
                    if (midResult.success)
                    {
                        midResult.output = " / " + number + midResult.output; // 
                        return midResult;
                    }
                }

            }
            return midResult;
        }
    } 

    public void Yazdir(string x)
    {

        panel1Text.text = sayi1.ToString();
        panel2Text.text = sayi2.ToString();
        panel3Text.text = sayi3.ToString();
        panel4Text.text = sayi4.ToString();
        panel5Text.text = sayi5.ToString();
        panel6Text.text = sayi6.ToString();
        panelhedefText.text = "Hedef: " + hedef.ToString();

        //

        int sonuc = 0;
        int sonuc2;


        string[] karakterler = (x.Split(' '));   

        int sayaç = 0;
        for (int i = 2; i <= karakterler.Length - 1; i += 2)
        {


            if (karakterler[i - 1] == "-" && sayaç == 0)
            {
                sonuc = int.Parse((karakterler[i - 2])) - int.Parse(karakterler[i]);
                islemlerText.text = karakterler[i - 2] + "-" + karakterler[i] + "=" + sonuc.ToString() + "\n";
            }
            else if (karakterler[i - 1] == "+" && sayaç == 0)
            {
                sonuc = int.Parse((karakterler[i - 2])) + int.Parse(karakterler[i]);
                islemlerText.text = karakterler[i - 2] + "+" + karakterler[i] + "=" + sonuc.ToString() + "\n";
            }
            else if (karakterler[i - 1] == "*" && sayaç == 0)
            {
                sonuc = int.Parse((karakterler[i - 2])) * int.Parse(karakterler[i]);
                islemlerText.text = karakterler[i - 2] + "*" + karakterler[i] + "=" + sonuc.ToString() + "\n";
            }
            else if (karakterler[i - 1] == "/" && sayaç == 0)
            {
                sonuc = int.Parse((karakterler[i - 2])) / int.Parse(karakterler[i]);
                islemlerText.text = karakterler[i - 2] + "/" + karakterler[i] + "=" + sonuc.ToString() + "\n";
            }


            else if (karakterler[i - 1] == "-" && sayaç != 0)
            {
                sonuc2 = sonuc - (int.Parse(karakterler[i]));
                islemlerText.text += sonuc + "-" + karakterler[i] + "=" + sonuc2 + "\n";
                sonuc = sonuc2;

            }
            else if (karakterler[i - 1] == "+" && sayaç != 0)
            {
                sonuc2 = sonuc + int.Parse(karakterler[i]);
                islemlerText.text += sonuc + "+" + karakterler[i] + "=" + sonuc2 + "\n";
                sonuc = sonuc2;
            }
            else if (karakterler[i - 1] == "*" && sayaç != 0)
            {
                sonuc2 = sonuc * int.Parse(karakterler[i]);
                islemlerText.text += sonuc + "*" + karakterler[i] + "=" + sonuc2 + "\n";
                sonuc = sonuc2;
            }
            else if (karakterler[i - 1] == "/" && sayaç != 0)
            {
                sonuc2 = sonuc / int.Parse(karakterler[i]);
                islemlerText.text += sonuc + "/" + karakterler[i] + "=" + sonuc2 + "\n";
                sonuc = sonuc2;
            }

            sayaç++;

            cevapUretildiMi = sonuc;

        }
        

    }


    public void AnaMenuDon()
    {
        if (MainMenu.mod == "rekabetci")
        {
            RekabetciBitis();
        }
        else
        {

            if (suredurdur == 0) 
            {
                uyariText.text = "Cevap Vermeden Çýktýðýnýz için -10 Puan";
                MainMenu.totalpuan -= 10;  
                PlayerPrefs.SetInt("totalpuan", MainMenu.totalpuan); 
            }

            SceneManager.LoadScene(0); 


        }


    }




}
