using UnityEngine;

public class CollectPumpkins : MonoBehaviour{
    // manages pumpking collecting when player picks up a pumpkin
    private void OnTriggerEnter(Collider other){
        // increases total number of pumpkins collected and destroys pumpkin game object
        if (other.gameObject.CompareTag("Pumpkin")){
            PurchaseMenu.totalPumpkins ++;
            PurchaseMenu.pumpkinsCollectedDisplay();
            Debug.Log("Pumpkin collected!");
            Destroy(other.gameObject);
        }
        
    }
}
