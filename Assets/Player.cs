using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	void Awake() { }
	// so we can set settings in another scene
	public Rigidbody2D rb;
	public Transform[] deathParticles;  // particles of death array (all children of Player)
	public ParticleSystem particles;	// variable for each particle
	public ColorManager ColorManager;	// colorChanger
	public Collider2D collid;			// collider of the Player
	public GameObject Turret;			// turret object to shoot the player
	public Collider2D turret;			// turret collider to detect player
	public UIController userInterface;	// UIcontroller Script
	public Camera cam;					// camera
	public BarScript Bar;				// Health bar script that deteriorates with jumping
	public GameObject BarItself;		// reference to the healthBar itself
	public AudioManager audioManager;	// has all audio in it
	public GameManager gameManager;		// manages gameStates (player alive=1/dead=0/won=2)
	public GameObject VolumeSlow;		// For Post Processing effects
	public EffectsController effects;	// for animating effects
	public PauseMenu pauseMenu;			// for pause menu
	public Animator animCont;	// time control animation controller
	public ProceduralGeneration procGen;// procedural generation reference (for detecting player death)

	public float jumpForce;				// how high the player jumps with each space click
	public float gravityScale;
	public int health;					// current health of the player (dont edit)
	public int maxHealth;				// maximum health of player (30 works fine)
	public string currentColor;			// currentColor (managed thru script - dont edit)
	bool dead;							// if player is dead (dont edit)
	bool enableBar;						// checks if bar is enabled (cant edit)
	public float jumpForceBit;			// single bit of force (dont edit)
	private float pitchBit;				// single bit of pitch (dont edit)
	private bool won;
	private bool normalizeJump;
	private float timeScaleBit;
	private int lvlToBeginTimeControl;

	void Start ()
	{
		dead = false;
		won = false;
		normalizeJump = false;
		gravityScale = 3f;
		rb.gravityScale = gravityScale;
		lvlToBeginTimeControl = 6; // this is level 3 (Level03)
		setupBar();
		setupGameSettings();
		if(normalizeJump) normalizeJumpForce();
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			procGen = GameObject.Find("ProceduralGeneration").GetComponent<ProceduralGeneration>();
		}
		if(SceneManager.GetActiveScene().buildIndex >= lvlToBeginTimeControl) doVolumeRelatedStuff();
		Turret = GameObject.Find("Turret");
		if (Turret != null) turret = Turret.GetComponent<Collider2D>();
		ColorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		collid = gameObject.GetComponent<Collider2D>();
		userInterface = GameObject.Find("Canvas").GetComponent<UIController>();
		deathParticles = GetComponentsInChildren<Transform>();
		// currentColor = ColorManager.GetComponent<ColorManager>().getCurrentColor();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		audioManager.PitchBackToNormal("Jump");
		
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log("deathParticles Count: "+deathParticles.Length);
		Debug.Log("color: "+currentColor);
		if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			rb.velocity = Vector2.up * jumpForce;
			FindObjectOfType<AudioManager>().Play("Jump");
			if (enableBar) { TakeDamage(1); }
		}
		
		if(Input.GetKeyDown(KeyCode.Q) && !pauseMenu.GameIsPaused)
		{
			if(SceneManager.GetActiveScene().buildIndex >= lvlToBeginTimeControl)
				slowDownTime();
		}
		if(Input.GetKeyDown(KeyCode.X) && PlayerPrefs.GetInt("levelReached", 0) >= 8)
		{	
			collid.enabled = !collid.enabled;
		}

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
		{	
			audioManager.Play("Button");
			pauseMenu.determineState();
		}
		
		if(dead)
		{
			//disable input
			// disable collisions on player
			collid.enabled = false;
		}
	}

	public bool isDead()
	{
		return dead;
	}

	public bool isWon()
	{
		return won;
	}

	public void whenHealthEnabled()
	{
		BarItself.SetActive(true);
		health = maxHealth;
		Bar.SetMaxBar(maxHealth);
		Bar.SetBar(health);
	}

	public void setupBar()
	{
		enableBar = GameSettings.enableBar;
		if (GameObject.Find("Bar") != null)
			{
				BarItself = GameObject.Find("Bar");
				Bar = BarItself.GetComponent<BarScript>();
			}
		else { Debug.LogWarning("Could Not Find Bar Object in the scene! !! ! !"); }
		BarItself.SetActive(false);
		if (enableBar) whenHealthEnabled();
	}
		

	public void TakeDamage(int damage)
	{
		if (health > 0)
		{
			health -= damage;
		}
		Bar.SetBar(health);
		if (jumpForce > jumpForceBit)
		{
			jumpForce -= jumpForceBit;
		}
		audioManager.ModulatePitch("Jump", -pitchBit);
	}

	public void FillHealth()
	{
		health = maxHealth;
		Bar.SetBar(health);
		jumpForce = GameSettings.jumpForce;
		audioManager.PitchBackToNormal("Jump");
	}

	private void setupGameSettings()
	{
		GameSettings.initializeFirst();
		jumpForce = GameSettings.jumpForce;
		jumpForceBit = (jumpForce - 3f) / maxHealth;
		pitchBit = 3f / maxHealth;
	}

	private void normalizeJumpForce()
	{
		if(normalizeJump)
		{
			if (jumpForce > 10f || jumpForce <4f)
			{
				jumpForce = 9f;
			}
		}
		
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag != currentColor && (col.tag == "blue" || col.tag == "pink" || col.tag == "yellow" || col.tag == "green"))
		{
			GameOver();
		}
		
		if (col.tag == "LevelEnd")
		{
			playerJustWon();
		}
	}

	public void playerJustWon()
	{
		Destroy(collid);
		enabled = false;
		if(!won) completedLvl();
		rb.gravityScale = gravityScale;
	}


	void doVolumeRelatedStuff()
	{
		animCont.SetFloat("SpeedMultiplier", 0f);
		// VolumeSlow = GameObject.Find("GlobalVolumeSlow");	
		// gameManager.setVolumeSlow(VolumeSlow);
		// VolumeSlow.SetActive(false);
	}

	void OnBecameInvisible()
    {
        enabled = false;
		GameOver();
    }

	void completedLvl()
	{
		gameManager.CompleteLevel();
		won = true;
	}

	public void GameOver()
	{
		if(!dead && !won && gameManager.getState() != 3)
		{
			rb.gravityScale = gravityScale;
			Explode();
			dead = true;
			enabled = false;
			//Invoke("slowDownTime", 3);
			gameManager.gameOver();
			if(SceneManager.GetActiveScene().buildIndex == 1) procGen.die();
		}
	}

	public void destroyPlayer()
	{
		Destroy(gameObject);
	}

	void Explode ()
	{
		if((!dead) && (!pauseMenu.GameIsPaused) && (Camera.main != null)) StartCoroutine(cam.GetComponent<CameraShake>().CamShake(0.4f, 0.1f));
		// Debug.Log(deathParticles.Length);
		if (deathParticles.Length > 0);
		{
			Debug.LogWarning("Size of particle array:	"+deathParticles.Length);
			switch (currentColor)
			{
				// deathParticles[0] refers to the Player itself!
				case "blue":
					particles = deathParticles[1].GetComponent<ParticleSystem>();
					break;
				case "yellow":
					particles = deathParticles[2].GetComponent<ParticleSystem>();
					break;
				case "pink":
					particles = deathParticles[3].GetComponent<ParticleSystem>();
					break;
				case "green":
					particles = deathParticles[4].GetComponent<ParticleSystem>();
					break;
				default:
					particles = deathParticles[1].GetComponent<ParticleSystem>();
					break;
			}
		}
		
		particles.Play();
		Debug.Log("Explode been called	");
    }

	void slowDownTime()
	{
		if (Time.timeScale > 1f || Time.timeScale < 1f)
		{
			animCont.SetFloat("SpeedMultiplier", -1f);
			Time.timeScale = 1f;
			audioManager.PitchBackToNormalAll();
			// VolumeSlow.SetActive(false);
			rb.gravityScale = gravityScale;
			return;
		}
			
		if (Time.timeScale == 1f)
		{
			animCont.SetFloat("SpeedMultiplier", 1f);
			Time.timeScale = 0.4f;
			string[] names = { "PlayerDeath", "ColorChange", "Jump", "OtherSound", "Music"};
			foreach (string name in names)
			{
				audioManager.ModulatePitch(name, -0.5f);
			}
			// VolumeSlow.SetActive(true);
			rb.gravityScale = gravityScale;
			return;
		}
	}
	void speedUpTime()
	{
		if (Time.timeScale < 1f)
		{
			Time.timeScale = 1f;
			audioManager.PitchBackToNormalAll();
			// VolumeSlow.SetActive(false);
			rb.gravityScale = gravityScale;
			return;
		}
			

		if (Time.timeScale == 1f)
		{
			Time.timeScale = 1.6f;
			string[] names = { "PlayerDeath", "ColorChange", "Jump", "OtherSound", "Music"};
			foreach (string name in names)
			{
				audioManager.ModulatePitch(name, 0.5f);
			}
			// VolumeSlow.SetActive(false);
			rb.gravityScale = 1.5f;
			return;
		}
			
	}
}