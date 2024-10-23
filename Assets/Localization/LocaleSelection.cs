using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;//biblioteca do controlador de idiomas

public class LocaleSelection : MonoBehaviour
{
    bool active = false; //variavel pra controlar a troca de idiomas
    void Start()
    {
        int id = PlayerPrefs.GetInt("LocaleSelected", 0);
        ButtonSetLocale(id);
    }
    public void ButtonSetLocale(int loc)
    {
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(loc));
    }
    IEnumerator SetLocale(int localeIndex)
    {
        active = true;

        yield return LocalizationSettings.InitializationOperation; //aguarda a tabela de idiomas carregar
        //acessa a tabela, encontra os idiomas (Locales) disponíveis (o array chamado AvailableLocales) e assinala
        //o valor do idioma selecionado à posição nova no array.
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];

        PlayerPrefs.SetInt("LocaleSelected", localeIndex);//guarda num playerprefs as configurações de
                                                          //idioma que foram usadas por último
        active = false;
    }
}