using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PurchaseMenu : MonoBehaviour{
    // public variables
        // reference to purchase menu UI
        public GameObject purchaseMenu;

        // tracks total number of pumpkins collected
            public GameObject pumpkinsCollected;
            public static TextMeshProUGUI pumpkinsCollectedText;
            public int initialPumpkinTotal;
            public static int totalPumpkins;

    // called once before the first execution of Update
    private void Start(){
        // sets up the initial state of the purchase menu
        purchaseMenu.SetActive(false);

        // gets text UI from pumpkins collected game object
        pumpkinsCollectedText = pumpkinsCollected.GetComponent<TextMeshProUGUI>();

        // sets up inital number of pumpkins
        totalPumpkins = initialPumpkinTotal;
        pumpkinsCollectedDisplay();
    }

    private void Update(){
        // cast a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // stores detail regarding the collider intersected by the ray
        RaycastHit hit;
        
        // checks when ray hits an object collider with a left mouse click
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)){
            // actives purchase menu visibility if ray hits pet
            if (hit.collider.CompareTag("Cat")){
                purchaseMenu.SetActive(true);

                // pauses gameplay
                Time.timeScale = 0;

                // prevents potion from being thrown
                ThrowPotion.setCanThrow(false);

                // frees the cursor from being locked in the center
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // temp quits game if player press escape
        if (Input.GetKeyDown(KeyCode.Escape)){
            // Only for testing in the Unity Editor
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
        }
    }

    // manages purchase menu visibility when player closes purchase menu
    public void closePurchaseMenu(){
        // turns off visibility of purchase menu
        if (purchaseMenu != null){
            purchaseMenu.SetActive(false);

            // resumes gameplay
            Time.timeScale = 1;

            // allows potion to be thronw
            ThrowPotion.setCanThrow(true);

            // locks the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // potions are puchased by using pumpkins as exchange
    public void purchasingPotion(){
        if (totalPumpkins > 0){
            // subtracts one pumpkin from total amount greater than zero
            totalPumpkins --;
            pumpkinsCollectedDisplay();
            
            // increase total number of potions to throw
            ThrowPotion.totalThrows ++;
            ThrowPotion.potionNumberDisplay();

            Debug.Log("Purchased potion");
        }
    }

    // stun time can be extended by using pumpkins as exchange
    public void purchasingStunTime(){
        if (totalPumpkins > 0){
            // subtracts two pumpkins from total amount greater than zero
            totalPumpkins -= 2;
            pumpkinsCollectedDisplay();
            
            // increase the number of seconds zombie can not move
            EnemyMovement.stunTime += 2;
            Debug.Log("Increased zombie wait time");
        }
    }

    public void purchasingHealth(){
        if (totalPumpkins > 0){
            // subtracts three pumpkins from total amount greater than zero
            totalPumpkins -= 3;
            pumpkinsCollectedDisplay();

            // increases player health
            Health.playerHealth += 10;
            Health.updateHealth();
            Debug.Log("Player healed");
        }
    }

    // updates and display number of pumpkins player is holding
    public static void pumpkinsCollectedDisplay(){
        pumpkinsCollectedText.text = "Pumpkins Collected: " + totalPumpkins;
    }
}
