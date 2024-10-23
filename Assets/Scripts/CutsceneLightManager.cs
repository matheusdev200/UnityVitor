using UnityEngine;

public class CutsceneLightManager : MonoBehaviour
{
    public Light[] lampadas;
    public Color corLampada;
    int index;

    public void TurnLampOn(int lampIndex)
    {
        lampadas[lampIndex].intensity = 100f;
        lampadas[lampIndex].color = corLampada;
    }
    public void TurnLampOn()
    {
        lampadas[index].intensity = 100f;
        lampadas[index].color = corLampada;
        index++;
    }
}