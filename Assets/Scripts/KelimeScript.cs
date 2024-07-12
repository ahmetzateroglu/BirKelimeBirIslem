using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KelimeScript : MonoBehaviour
{

    public Text harf1Text, harf2Text, harf3Text, harf4Text, harf5Text, harf6Text, harf7Text, harf8Text, harf9Text, cevapText, cevapAnahtariText, uyariText;
    public Button button1, button2, button3, button4, button5, button6, button7, button8, button9;
    public Color beyazRenk, griRenk;
    public GameObject cevaplarPaneli;

    Stack<int> yazilanlar = new Stack<int>();

    private List<string> kelimeler = new List<string>();
    List<string> bulunanKelimeler = new List<string>();

    private char[] girilenHarfler;

    void Start()
    {
        cevaplarPaneli.SetActive(false);

        KelimeAta();
        RandomHarfAta();
        KelimeBul();
    }



    public void Onayla()
    {

        string cevap = cevapText.text;
        bool bulundu = false;

        foreach (string kelime in bulunanKelimeler)
        {
            bool kelimeEslesiyor = true;

            for (int i = 0; i < kelime.Length; i++)
            {
                if (cevap[i] != kelime[i] && cevap[i] != '?')  
                {
                    kelimeEslesiyor = false;
                    break;
                }
            }

            if (kelimeEslesiyor && kelime.Length == cevap.Length)
            {
                bulundu = true;
                uyariText.text = "Buldunuz "+kelime + kelime.Length;
                Debug.Log("Buldunuz: " + kelime);
            }
        }

        if (!bulundu)
        {
            Debug.Log("Bulamadýnýz");

        }


    }


    private void KelimeBul()
    {
        girilenHarfler = new char[9];
        bool enAzBirYediHarfliKelimeVar = false;
        int sayac = 0;

        foreach (string kelime in kelimeler)
        {
            char[] ayrilmis = kelime.ToCharArray();
            int dogruHarfSayisi = 1; 

            KelimeAlmak(girilenHarfler);

            for (int i = 0; i < kelime.Length; i++)
            {
                for (int j = 0; j < girilenHarfler.Length; j++)
                {
                    if (ayrilmis[i] == girilenHarfler[j])
                    {
                        dogruHarfSayisi++;
                        girilenHarfler[j] = '\0'; 
                        break;
                    }
                }
            }


            if (dogruHarfSayisi == kelime.Length)
            {
                sayac += 1;
                bulunanKelimeler.Add(kelime);
                if (kelime.Length >= 6)
                {
                    enAzBirYediHarfliKelimeVar = true;

                    cevapAnahtariText.text += kelime + "\n";
                }
            }

        }

        if (enAzBirYediHarfliKelimeVar /*&& bulunanKelimeler.Count <= 10*/) // Bulmasý çok zorlaþýyor 
        {
            Debug.Log("7 harfli var");
            Debug.Log(sayac);
        }
        else
        {
            SiradakiSoru();
            //RandomHarfAta();
            //KelimeBul();
            Debug.Log("7 harfli yok");
        }



    }


    private void RandomHarfAta()
    {
        string harfler = "ABCÇDEFGÐHIÝJKLMNOÖPRSÞTUÜVYZ"; // Tüm harfleri içeren bir string
        string harfler1 = "AEIÝOÖUÜAE";
        string harfler2 = "BCÇDFGÐHJKLMNOPRSTUVYZKLMRNT";
        System.Random random = new System.Random();

        harf1Text.text = harfler1[random.Next(0, harfler1.Length)].ToString();
        harf2Text.text = harfler1[random.Next(0, harfler1.Length)].ToString();
        harf3Text.text = harfler2[random.Next(0, harfler2.Length)].ToString();
        harf4Text.text = harfler2[random.Next(0, harfler2.Length)].ToString();
        harf5Text.text = harfler[random.Next(0, harfler.Length)].ToString();
        harf6Text.text = harfler[random.Next(0, harfler.Length)].ToString();
        harf7Text.text = harfler[random.Next(0, harfler.Length)].ToString();
        harf8Text.text = harfler[random.Next(0, harfler.Length)].ToString();
        // harf9Text.text = harfler[random.Next(0, harfler.Length)].ToString();
    }

    public void SiradakiSoru()
    {
        Sifirla();
        RandomHarfAta();
        bulunanKelimeler.Clear();
        cevapAnahtariText.text = "";
        KelimeBul();

    }

    public void PanelYonetimi(int x)
    {

        if (x == 0)
        {
            cevaplarPaneli.SetActive(true);
        }
        if (x == 1)
        {
            cevaplarPaneli.SetActive(false);
        }
    }

    private void KelimeAta()
    {
        // Kelimeleri metin dosyasýndan okuyun
        TextAsset textAsset = Resources.Load<TextAsset>("kelimeler");
        string[] satirlar = textAsset.text.Split('\n');

        foreach (string satir in satirlar)
        {
            // Satýrdaki boþluklarý kaldýrarak kelimeleri listeye ekle
            string kelime = satir.Trim();
            if (!string.IsNullOrEmpty(kelime))
            {
                kelimeler.Add(kelime);
            }
        }
    }

    public void Sil()
    {
        if (yazilanlar.Peek() == 1)
        {
            yazilanlar.Pop();
            button1.enabled = true;
            button1.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]); // Son harf kýrpýlýyor
        }
        else if (yazilanlar.Peek() == 2)
        {
            yazilanlar.Pop();
            button2.enabled = true;
            button2.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 3)
        {
            yazilanlar.Pop();
            button3.enabled = true;
            button3.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 4)
        {
            yazilanlar.Pop();
            button4.enabled = true;
            button4.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 5)
        {
            yazilanlar.Pop();
            button5.enabled = true;
            button5.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 6)
        {
            yazilanlar.Pop();
            button6.enabled = true;
            button6.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 7)
        {
            yazilanlar.Pop();
            button7.enabled = true;
            button7.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 8)
        {
            yazilanlar.Pop();
            button8.enabled = true;
            button8.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }
        else if (yazilanlar.Peek() == 9)
        {
            yazilanlar.Pop();
            button9.enabled = true;
            button9.image.color = beyazRenk;
            cevapText.text = cevapText.text.TrimEnd(cevapText.text[cevapText.text.Length - 1]);
        }

    }

    public void Yazdir(int x)
    {
        if (x == 1)
        {
            cevapText.text += harf1Text.text;
            yazilanlar.Push(1);
            button1.enabled = false;
            button1.image.color = griRenk;
        }
        if (x == 2)
        {
            cevapText.text += harf2Text.text;
            yazilanlar.Push(2);
            button2.enabled = false;
            button2.image.color = griRenk;
        }
        if (x == 3)
        {
            cevapText.text += harf3Text.text;
            yazilanlar.Push(3);
            button3.enabled = false;
            button3.image.color = griRenk;
        }
        if (x == 4)
        {
            cevapText.text += harf4Text.text;
            yazilanlar.Push(4);
            button4.enabled = false;
            button4.image.color = griRenk;
        }
        if (x == 5)
        {
            cevapText.text += harf5Text.text;
            yazilanlar.Push(5);
            button5.enabled = false;
            button5.image.color = griRenk;
        }
        if (x == 6)
        {
            cevapText.text += harf6Text.text;
            yazilanlar.Push(6);
            button6.enabled = false;
            button6.image.color = griRenk;
        }
        if (x == 7)
        {
            cevapText.text += harf7Text.text;
            yazilanlar.Push(7);
            button7.enabled = false;
            button7.image.color = griRenk;
        }
        if (x == 8)
        {
            cevapText.text += harf8Text.text;
            yazilanlar.Push(8);
            button8.enabled = false;
            button8.image.color = griRenk;
        }
        if (x == 9)
        {
            cevapText.text += harf9Text.text;
            yazilanlar.Push(9);
            button9.enabled = false;
            button9.image.color = griRenk;
        }
    }

    private void KelimeAlmak(char[] gelenHarfler)
    {
        gelenHarfler[0] = harf1Text.text[0];
        gelenHarfler[1] = harf2Text.text[0];
        gelenHarfler[2] = harf3Text.text[0];
        gelenHarfler[3] = harf4Text.text[0];
        gelenHarfler[4] = harf5Text.text[0];
        gelenHarfler[5] = harf6Text.text[0];
        gelenHarfler[6] = harf7Text.text[0];
        gelenHarfler[7] = harf8Text.text[0];
        gelenHarfler[8] = harf9Text.text[0];
    }

    public void Sifirla()
    {
        cevapText.text = "";
        button1.enabled = true;
        button1.image.color = beyazRenk;
        button2.enabled = true;
        button2.image.color = beyazRenk;
        button3.enabled = true;
        button3.image.color = beyazRenk;
        button4.enabled = true;
        button4.image.color = beyazRenk;
        button5.enabled = true;
        button5.image.color = beyazRenk;
        button6.enabled = true;
        button6.image.color = beyazRenk;
        button7.enabled = true;
        button7.image.color = beyazRenk;
        button8.enabled = true;
        button8.image.color = beyazRenk;
        button9.enabled = true;
        button9.image.color = beyazRenk;

    }


    public void AnaMenuDon()
    {
        
            SceneManager.LoadScene(0); // 0 indexli sahneyi yükle

    }




}


