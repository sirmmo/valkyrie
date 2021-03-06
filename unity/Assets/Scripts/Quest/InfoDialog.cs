﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Content;
using Assets.Scripts.UI;
using ValkyrieTools;

// Monster information dialog (additional rules)
public class InfoDialog {

    public InfoDialog(Quest.Monster m)
    {
        if (m == null)
        {
            ValkyrieDebug.Log("Warning: Invalid monster type requested.");
            return;
        }

        // box with monster info
        UIElement ui = new UIElement();
        ui.SetLocation(10, 0.5f, UIScaler.GetWidthUnits() - 20, 12);
        ui.SetText(m.monsterData.info);
        new UIElementBorder(ui);

        // Unique monsters have additional info
        if (m.unique && m.uniqueText.KeyExists())
        {
            DialogBox db = new DialogBox(new Vector2(12, 13f), new Vector2(UIScaler.GetWidthUnits() - 24, 2), 
                m.uniqueTitle, Color.red);
            db.textObj.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetMediumFont();
            db.AddBorder();

            string uniqueText = EventManager.OutputSymbolReplace(m.uniqueText.Translate().Replace("\\n", "\n"));
            ui = new UIElement();
            ui.SetLocation(10, 15, UIScaler.GetWidthUnits() - 20, 8);
            ui.SetText(uniqueText);
            new UIElementBorder(ui, Color.red);
            new TextButton(new Vector2(UIScaler.GetWidthUnits() - 21, 23.5f), new Vector2(10, 2), CommonStringKeys.CLOSE, delegate { onClose(); });
        }
        else
        {
            new TextButton(new Vector2(UIScaler.GetWidthUnits() - 21, 13f), new Vector2(10, 2), CommonStringKeys.CLOSE, delegate { onClose(); });
        }
    }

    // Close cleans up
    public void onClose()
    {
        // Clean up everything marked as 'dialog'
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(Game.DIALOG))
            Object.Destroy(go);
    }
}
