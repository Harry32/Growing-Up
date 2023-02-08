using UnityEngine;

public class LossScript : MonoBehaviour
{
    private CharacterMovementScript characterMovementScript;
    private PanelScript panelScript;

    // Start is called before the first frame update
    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        panelScript = GameObject.Find("Panel").GetComponent<PanelScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            characterMovementScript.StopMoving();
            panelScript.ShowLossPanel();
        }
    }
}