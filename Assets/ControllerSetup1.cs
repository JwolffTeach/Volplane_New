using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volplane;

public class ControllerSetup : VolplaneBehaviour {

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
        ChangeView(player, "Spaceteam");
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
        string element = buttonsAvailable[i];
        buttonsUsed.Add(element);
        buttonsAvailable.Remove(element);
        player.ShowElement(element);
        // Move button to correct spot.

        // Second Button
        i = Random.Range(0, buttonsAvailable.Count);
        element = buttonsAvailable[i];
        buttonsUsed.Add(element);
        buttonsAvailable.Remove(element);
        player.ShowElement(element);
        // Move button to correct spot.
    }

    private void RandomizeSwipes(VPlayer player) {
        HideAll(player, swipes);
        int i = Random.Range(0, swipesAvailable.Count);
        string element = swipesAvailable[i];
        swipesUsed.Add(element);
        swipesAvailable.Remove(element);
        player.ShowElement(element);
    }

    private List<string> CloneList(List<string> Original, List<string> New) {
        New.Clear();
        for (int i = 0; i < Original.Count; i++) {
            New.Add(Original[i]);
        }
        return New;
    }

}
