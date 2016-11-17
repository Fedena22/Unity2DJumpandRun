using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthControler : MonoBehaviour {

	public float startHealth = 5;
	public int startLifePoints = 3;
	public float health = 5;
    float maxhealth = 5;
	int lifePoints = 3 ;
    //GUI
    public Image healthgui;
    public Text messageText;

	Animator anim;
	playerController playerController;
	private bool isDead = false;
	private bool isDamageable = true ; 

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator>();
		playerController = GetComponent<playerController> ();

		if(Application.loadedLevel == 0) { //Change if Menue is scene zero
			health = startHealth;
			lifePoints = startLifePoints;
		} else {
			health = PlayerPrefs.GetFloat ("Healt");
			lifePoints = PlayerPrefs.GetInt ("Lifepoints");
		}
        messageText.text = "";
        UpdateView();
	}
	
	void ApplayDamage(float damage)
	{
		if (isDamageable) {
			health -= damage;
			print (health);
			print ("DMG");
			health = Mathf.Max (0, health);

			if (!isDead) {
				if (health == 0) {
					isDead = true;
					Dying ();
				} else {
					if (isDamageable) {
						Damaging ();
					}
				}
				isDamageable = false;
				Invoke ("RestetIsDamageable", 1);
			}
		}
	}

	void Dying() {
		anim.SetBool ("Dying", true);

		playerController.enabled = false;
		lifePoints--;
        UpdateView();
		print (lifePoints);
		if (lifePoints <= 0) {
            //Startgame
            messageText.text = "Game Over";

            Invoke("StartGame",3);
		}
		else
		{
			//Restartlevel
			Invoke("RestartLevel", 1);
		}
	}

	void RestetIsDamageable() {
		isDamageable = true;
	}

	void StartGame()
	{
		Application.LoadLevel(0);
	}


	void RestartLevel() {
		health = startHealth;
		anim.SetBool ("Dying", false);
		playerController.enabled = true;
		isDead = false;
		if (!playerController.lookingRight) {
			playerController.Flip ();
		}

		//Level neu genereiren und Spieler zurücksetzten

	}

	void Damaging() {
        //anim.SetTrigger ("Damage");
        UpdateView();

	}
	void OnDestroy() {
		PlayerPrefs.SetFloat ("Healt", health);
		PlayerPrefs.SetInt ("Lifepoints", lifePoints);
	}

    void UpdateView()
    {
        healthgui.fillAmount = health / maxhealth;

    }
}