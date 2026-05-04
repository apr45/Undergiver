using UnityEngine;

public class DestroyPotion : MonoBehaviour{

    // destroys collided gameObject that is being thrown
    public void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Potion")){
            Destroy(collision.gameObject);
        }
        
    }
}

