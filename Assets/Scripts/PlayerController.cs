using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;		//Velocidad del jugador
	public Text countText;	//Para la puntuación
	//public Text winText;	//Lo use para probar que funcionara
	public Slider life;		//La vida del juegador

	private Rigidbody rb;	//No le hagas caso
	private int count;		//Para la putuación
	private bool collision=  false;	//Para que cuente las colisiones en el slider

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;			//Tu puntuación empieza en cero
		SetCountText ();	//Para hacer la puntuación String
		//winText.text = ""; 
	}

	void Update () {
		SetCountSlider();	//Para que baje el slider
	}


	// Update is called once per frame
	void FixedUpdate () {

		float moveHorzontal = Input.GetAxis ("Horizontal");	//Para mover a jugador
		float moveVertical = Input.GetAxis ("Vertical");	//Para mover a jugador

		Vector3 movement = new Vector3 (moveHorzontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed); //Puedes cambiar la velocidad en el GameObject

	}

	void OnTriggerEnter(Collider other){	//Colisiones
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 10;		//Cambia la puntuación si lo ves necesario
			SetCountText ();
			collision = true;

		}
	}

	void SetCountText(){ //La puntuación
		countText.text = "Count: " + count.ToString ();
		if (count >= 1) {							//Cambia el valor a uno mayor para que gane
			//winText.text = "победитель";
			Invoke ("Change1", 1);
		}
	}

	void SetCountSlider(){
		life.value = Mathf.MoveTowards (life.value, 0.0f, 0.15f);	//Bájale al último valor para que la barra no se mueva tan rápido
		if (collision == true) {
			life.value += 10f;		//Sube o baja cuanto va a incrementar la barra cuando haga la colisión
			collision = false;
		}
		if(life.value <=0f){		//Cuando tienes 0 en el slider pierdes
			//winText.text = ".l.";
			Invoke ("Change2",1);
		}
	}
	void Change1(){
		SceneManager.LoadScene("Win");
	}

	void Change2(){
		SceneManager.LoadScene("GameOver");
	}
}
