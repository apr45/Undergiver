using UnityEngine;
using TMPro;
public class Health : MonoBehaviour{
    // public variables
        // reference to player health
        public GameObject health;

        // reference to endgame menu UI
        public GameObject endgameMenu;

        // amount of damage player takes
        public int enemyDamage;

        // amount of health player has
        public static int playerHealth;

    // private variables
        // reference to health text UI to track player's health
        private static TextMeshProUGUI healthText;

    // called once before the first execution of Update
    private void Start(){
        // gets health text UI from health game object
        healthText = health.GetComponent<TextMeshProUGUI>();

        // sets up player initial health
        playerHealth = 100;
        updateHealth();

        // sets up initial visilibity of endgame menu
        endgameMenu.SetActive(false);
    }

    private void Update(){
        if (playerHealth == 0){
            Time.timeScale = 0;
            endgameMenu.SetActive(true);
        }
    }

    // 
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Zombie") && enemyDamage != 0){
            playerHealth -= enemyDamage;
            updateHealth();
        }
    }

    public static void updateHealth(){
        healthText.text = "Health: " + playerHealth;
    }
}