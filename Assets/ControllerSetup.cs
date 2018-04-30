using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volplane;

public class ControllerSetup : VolplaneBehaviour {

    [SerializeField] List<string> buttons;
    [SerializeField] List<string> buttonsUsed;
    [SerializeField] List<string> swipes;
    [SerializeField] List<string> swipesUsed;

    ElementProperties hide = new ElementProperties();
    ElementProperties show = new ElementProperties();

    private void Start() {
        hide.Hidden = true; // hide element
        show.Hidden = false; // show element
    }

    public void LoadController(VPlayer player) {
        HideAll(player);
        ChangeView(player, "Spaceteam");
        RandomizeButtons(player);
        RandomizeSwipes(player);
    }

    public void HideAll(VPlayer player) {
        foreach (string item in buttons) {
            player.ChangeElementProperties(item, hide);
        }
        foreach (string item in swipes) {
            player.ChangeElementProperties(item, hide);
        }
    }

    private void RandomizeButtons(VPlayer player) {
        int i = Random.Range(0, buttons.Count);
        string element = buttons[i];
        buttonsUsed.Add(element);
        player.ShowElement(element);
    }

    private void RandomizeSwipes(VPlayer player) {
        int i = Random.Range(0, swipes.Count);
        string element = swipes[i];
        swipesUsed.Add(element);
        player.ShowElement(element);
    }

}
