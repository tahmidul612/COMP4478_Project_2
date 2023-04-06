// Tahmidul Islam @tahmidul612
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SetHelpText : MonoBehaviour
{
    // This script can be attached to any object to make it show a help text when the player collides with it
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            GeneratePopup.Instance.showHelpText(GenerateHelpText());
            if (name != "NextLevel")
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private string GenerateHelpText()
    {
        return name switch
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
