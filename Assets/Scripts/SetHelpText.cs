using System.Collections;
using UnityEngine;

public class SetHelpText : MonoBehaviour
{
    private Coroutine writeTextActive;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            GeneratePopup.Instance.showHelpText(generateHelpText());
            if (this.name != "NextLevel")
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private string generateHelpText()
    {
        return this.name switch
        {
            "Slippery" => "Watch out for the slime on the floor! It's slippery",
            "Spike" => "Watch out for the spikes! They're sharp",
            "Sticky" => "You can stick to the walls with the sticky slime",
            "Bouncy" => "You can bounce off the bouncy slime",
            "NextLevel" => "Jump to go through the portal to the next level",
            _ => ""
        };
    }
}
