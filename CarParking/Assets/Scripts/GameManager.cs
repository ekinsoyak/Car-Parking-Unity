using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("--Araba Ayarları--")]
    public GameObject[] arabalar;
    public int KacArabaOlsun;
    int aktifAracIndex;
    int KalanAracSayisiDegeri;

    [Header("--Canvas Ayarları--")]
    public GameObject[] arabaCanvasGorselleri;
    public Sprite aracGeldiGorseli;
    public TextMeshProUGUI[] Textler;
    public GameObject[] Panellerim;
    public GameObject[] TapToButonlar;


    [Header("--Platform Ayarları--")]
    public GameObject platform_1;
    public GameObject platform_2;
    public float [] donusHizlari;
    bool donusVarMi=false;


    [Header("--Level Ayarları--")]
    public int elmasSayisi;
    public ParticleSystem carpmaEfekti;
    public AudioSource[] Sesler;
    public bool platformYukselsinMi;
    bool dokunmaKilidi;

    private void Start() {
        dokunmaKilidi=true;
        donusVarMi=true;
        VarsayilanDegerler();

        KalanAracSayisiDegeri=KacArabaOlsun;

        for (int i = 0; i < KacArabaOlsun; i++)
        {
          arabaCanvasGorselleri[i].SetActive(true);  
        }
        
        arabalar[aktifAracIndex].SetActive(true);
    }

    public void YeniArabaGetir(){

        KalanAracSayisiDegeri--;

        if(aktifAracIndex<KacArabaOlsun){
            arabalar[aktifAracIndex].SetActive(true);
        }else{
            Kazandin();
        }
        
        
        
        arabaCanvasGorselleri[aktifAracIndex-1].GetComponent<Image>().sprite=aracGeldiGorseli;

    }

    private void Update() {


        if(Input.GetKeyDown(KeyCode.G)){

            arabalar[aktifAracIndex].GetComponent<araba>().ileri=true;
            aktifAracIndex++;
        }
        if(Input.GetKeyDown(KeyCode.H)){
            Panellerim[0].SetActive(false);
            Panellerim[3].SetActive(true);
        }

        if(Input.touchCount==1){

            Touch touch = Input.GetTouch(0);

            if(touch.phase==TouchPhase.Began){

                if(dokunmaKilidi){

                    Panellerim[0].SetActive(false);
                    Panellerim[3].SetActive(true);
                    dokunmaKilidi=false;

                }else{

                    arabalar[aktifAracIndex].GetComponent<araba>().ileri=true;
                    aktifAracIndex++;
                }

            }

        }

        if(donusVarMi){
            platform_1.transform.Rotate(new Vector3(0,0,donusHizlari[0]),Space.Self);
            if(platform_2!=null)
            platform_2.transform.Rotate(new Vector3(0,0,-donusHizlari[1]),Space.Self);
        }
        
    }

    public void Kaybettin(){

        donusVarMi=false;

        Textler[7].text=PlayerPrefs.GetInt("Elmas").ToString();
        Textler[6].text=SceneManager.GetActiveScene().name;
        Textler[8].text=(KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[9].text=elmasSayisi.ToString();

        Sesler[1].Play();
        Sesler[2].Play();

        Invoke("KaybettinTapToButonBaslat",2f);

        Panellerim[1].SetActive(true);
        Panellerim[3].SetActive(false);

    }

    void KaybettinTapToButonBaslat(){

        TapToButonlar[0].SetActive(true);
    }
    void KazandinTapToButonBaslat(){

        TapToButonlar[1].SetActive(true);
    }
    
    void Kazandin(){
        PlayerPrefs.SetInt("Elmas",PlayerPrefs.GetInt("Elmas")+elmasSayisi);

        Invoke("KazandinTapToButonBaslat",2f);

        Textler[3].text=PlayerPrefs.GetInt("Elmas").ToString();
        Textler[2].text=SceneManager.GetActiveScene().name;
        Textler[4].text=(KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[5].text=elmasSayisi.ToString();

        Sesler[3].Play();

        Panellerim[2].SetActive(true);
        Panellerim[3].SetActive(false);

    }

    //Bellek Yönetimi

    void VarsayilanDegerler(){

        Textler[0].text=PlayerPrefs.GetInt("Elmas").ToString();
        Textler[1].text=SceneManager.GetActiveScene().name;

    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void sonrakiLevel(){
        PlayerPrefs.SetInt("Level",SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
