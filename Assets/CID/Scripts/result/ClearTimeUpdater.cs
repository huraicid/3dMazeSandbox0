using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrearTimeUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        float currentTime = GameVariables.currentTime;
        textMeshPro.text = string.Format("{0:D2}:{1:D2}.{2:D2}",
            (int)currentTime / 60,
            (int)currentTime % 60,
            (int)(currentTime * 100) % 60
            );
    }

}
