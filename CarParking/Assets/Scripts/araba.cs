using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba : MonoBehaviour
{
    float yukselmeDegeri;
    bool durusNoktasiDurumu=false;
    bool platformYukselt;

    public bool ileri;
    public Transform Parent;
    public GameManager _gameManager;
    public GameObject partcPoint;
    public GameObject[] Tekerinizleri;

    private void Update() {
         if(!durusNoktasiDurumu)
        transform.Translate(7f*Time.deltaTime*transform.forward);

        if(ileri)
        transform.Translate(15f*Time.deltaTime*transform.forward);

        if(platformYukselt){

            if(yukselmeDegeri>_gameManager.platform_1.transform.position.y){

                _gameManager.platform_1.transform.position = Vector3.Lerp(_gameManager.platform_1.transform.position,
            new Vector3(_gameManager.platform_1.transform.position.x,
            _gameManager.platform_1.transform.position.y+1.3f,
            _gameManager.platform_1.transform.position.z),.010f);
            
            }else{

                platformYukselt=false;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.CompareTag("parkalani")){

            ileri=false;
            Tekerinizleri[0].SetActive(false);
            Tekerinizleri[1].SetActive(false);
            transform.SetParent(Parent);

            GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;

            if(_gameManager.platformYukselsinMi){
            
            yukselmeDegeri=_gameManager.platform_1.transform.position.y+1.3f;
            platformYukselt=true;
             
            }

            

            _gameManager.YeniArabaGetir();
        
        }else if(other.gameObject.CompareTag("araba")){
            
            _gameManager.carpmaEfekti.transform.position=partcPoint.transform.position;
            _gameManager.carpmaEfekti.Play();

            ileri=false;
            Tekerinizleri[0].SetActive(false);
            Tekerinizleri[1].SetActive(false);

            _gameManager.Kaybettin();
        }
}
        private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("durusnoktasi")){

            durusNoktasiDurumu=true;
        }else if(other.CompareTag("elmas")){

            other.gameObject.SetActive(false);
            _gameManager.elmasSayisi++;
            _gameManager.Sesler[0].Play();
            
        }else if(other.CompareTag("ortagobek")){

            ileri=false;
            Tekerinizleri[0].SetActive(false);
            Tekerinizleri[1].SetActive(false);

            _gameManager.carpmaEfekti.transform.position=partcPoint.transform.position;
            _gameManager.carpmaEfekti.Play();
            ileri=false;
            _gameManager.Kaybettin();
        }else if(other.CompareTag("onparkalani")){
            
            other.gameObject.GetComponent<onparkalani>().ParkAktifYap();
        }

            
}
}