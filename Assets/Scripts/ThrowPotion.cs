using TMPro;
using UnityEngine;

public class ThrowPotion : MonoBehaviour{
    //public variables
        // gets potion game object that will be thrown
        public GameObject potion;

        // gets player's transform component
        public Transform player;


        // reference to text component for displaying total number of potions
        public GameObject PotionsInventory;
        public static TextMeshProUGUI PotionsInventoryText;

        // controls how hard and high the potion will be thrown
            public float throwForce;
            public float throwForceUp;

        // number of times player can throw potion
        public int initialThrows;
        public static int totalThrows;


    // private variables
        // checks if potion can be thrown or not
        private static bool canThrow;


    // called once before the first execution of Update
    private void Start(){
        // gets text UI from potions inventory game object
        PotionsInventoryText = PotionsInventory.GetComponent<TextMeshProUGUI>();

        // displays initial number of potions
        totalThrows = initialThrows;
        potionNumberDisplay();

        // ensures potion clone can be thrown once game starts
        canThrow = true;
    }

    // updates every frame
    private void Update(){
        // checks if there is any potion to be thrown when player right clicks
        if (Input.GetMouseButtonDown(1) && totalThrows > 0 && canThrow){
            Throw();
        }
    }

    // player throws a clone of a potion game object
    private void Throw(){
        // makes a clone of potion game object
        GameObject potionClone = Instantiate(potion, player.position, player.rotation);

        // gets rigidbody component of clone
        Rigidbody rb = potionClone.GetComponent<Rigidbody>();

        // calculates and adds force to clone
        Vector3 force = player.forward * throwForce + transform.up * throwForceUp;
        rb.AddForce(force, ForceMode.Impulse);

        // subtracts one throw from total number of throws
        totalThrows --;
        potionNumberDisplay();
    }

    // displays current number of potions
    public static void potionNumberDisplay(){
        PotionsInventoryText.text = "Potions Left: " + totalThrows;
    }

    // setter for can throw boolean
    public static void setCanThrow(bool logicValue){
        canThrow = logicValue;
    }
}
