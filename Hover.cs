using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler
{
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "Button - Background") {
            Play.instance.onBoard = false;
            for(int i=0; i<Play.instance.buttons.Length; i++) {
                Play.instance.buttons[i].enabled = false;
            }
        }
        else {
            Play.instance.onBoard = true;
        }
        for (int i=0; i<Play.instance.buttons.Length; i++) {
            if (Play.instance.buttons[i].name == gameObject.name) {
                Play.instance.tile = i;
            }
        }
        for (int i=0; i<Play.instance.sprites.Length; i++) {
            if (Play.instance.sprites[i] == gameObject.GetComponent<Image>().sprite) {
                Play.instance.letter = i;
            }
        }
    }


}
