using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volplane;

public class ControllerSetup_Dynamic : VolplaneBehaviour {

    [SerializeField] List<string> buttons;
    [SerializeField] List<string> buttonsAvailable;
    [SerializeField] List<string> buttonsUsed;
    [SerializeField] List<string> swipes;
    [SerializeField] List<string> swipesAvailable;
    [SerializeField] List<string> swipesUsed;

    ElementProperties hide = new ElementProperties();
    ElementProperties show = new ElementProperties();

    private void Start() {
        hide.Hidden = true; // hide element
        show.Hidden = false; // show element
        ResetOptions();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            ResetOptions();
            foreach (VPlayer player in GetAllActivePlayers()) {
                LoadController(player);
            }
        }
    }

    public void LoadController(VPlayer player) {
        ChangeView(player, "QuadScreen");
        HideAll(player, buttons);
        HideAll(player, swipes);
        RandomizeButtons(player);
        RandomizeSwipes(player);
    }

    public void HideAll(VPlayer player, List<string> elementType) {
        foreach (string item in elementType) {
            player.ChangeElementProperties(item, hide);
        }
        foreach (string item in elementType) {
            player.ChangeElementProperties(item, hide);
        }
    }

    private void ResetOptions() {
        buttonsAvailable = CloneList(buttons, buttonsAvailable);
        buttonsUsed.Clear();
        swipesAvailable = CloneList(swipes, swipesAvailable);
        swipesUsed.Clear();
    }

    private void RandomizeButtons(VPlayer player) {

        // First Button
        int i = Random.Range(0, buttonsAvailable.Count);
        string btnText = buttonsAvailable[i];
        buttonsUsed.Add(btnText);
        buttonsAvailable.Remove(btnText);
        player.ShowElement("Top_Left");
        player.ChangeElementText("Top_Left", btnText);
        // Move button to correct spot.

        // Second Button
        i = Random.Range(0, buttonsAvailable.Count);
        btnText = buttonsAvailable[i];
        buttonsUsed.Add(btnText);
        buttonsAvailable.Remove(btnText);
        player.ShowElement("Top_Right");
        player.ChangeElementText("Top_Right", btnText);
        // Move button to correct spot.
    }

    private void RandomizeSwipes(VPlayer player) {

        // First Swipe
        int i = Random.Range(0, swipesAvailable.Count);
        string element = swipesAvailable[i];
        swipesUsed.Add(element);
        swipesAvailable.Remove(element);
        player.ShowElement("Bottom_Left");
        player.ChangeElementText("Bottom_Left", element);

        // Second Swipe
        i = Random.Range(0, swipesAvailable.Count);
        element = swipesAvailable[i];
        swipesUsed.Add(element);
        swipesAvailable.Remove(element);
        player.ShowElement("Bottom_Right");
        player.ChangeElementText("Bottom_Right", element);
    }

    private List<string> CloneList(List<string> Original, List<string> New) {
        New.Clear();
        for (int i = 0; i < Original.Count; i++) {
            New.Add(Original[i]);
        }
        return New;
    }

}
