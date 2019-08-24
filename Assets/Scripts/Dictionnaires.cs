using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Dictionnaires 
{
    public enum LANGUES : int
    {
        FR  = 0,
        EN  = 1,
        MAX = 2
    }

    private static Dictionnaires instance;
    private Dictionary<string, string> dictionnaireCourant;
    private LANGUES langueCourante;


    public static Dictionnaires Dictionnaire
    {
        get
        {
            if (instance == null)
                instance = new Dictionnaires();

            return instance;
        }
    }


    private Dictionnaires()
    {
        // lecture dans les donnée persistantes pour connaitre la langue choisi
        // sinon on prend la langue du tel si elle match avec celle du jeu
        // sinon on prend anglais
        if (PlayerPrefs.HasKey("langue"))
            langueCourante = (LANGUES)PlayerPrefs.GetInt("langue");
        else
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.French:
                    langueCourante = LANGUES.FR;
                    break;

                case SystemLanguage.English:
                    langueCourante = LANGUES.EN;
                    break;

                default:
                    langueCourante = LANGUES.EN;
                    break;
            }

            //sauvegarde
            PlayerPrefs.SetInt("langue", (int)langueCourante);
            PlayerPrefs.Save();
        }

        Debug.Log("lg = " + langueCourante);
        ChargerDictionnaire();
    }

    // parseur xml  : retourn -1 si erreur. 0 sinon
    public int ChargerDictionnaire()
    {
        XmlDocument document;

        if (dictionnaireCourant == null)
            dictionnaireCourant = new Dictionary<string, string>();

        if (dictionnaireCourant.Count > 0)
            dictionnaireCourant.Clear();


        string path ="pasdeChemin";
        document = new XmlDocument();
        try
        {
#if UNITY_EDITOR
            path = Application.dataPath + "/Resources/Langues/" + langueCourante.ToString() + ".xml";
            document.Load(path);
#elif UNITY_ANDROID
            path = "Langues/" + langueCourante.ToString();
            TextAsset textFile = Resources.Load<TextAsset>(path);
            document.LoadXml(textFile.text.ToString());

#endif
        }
        catch (Exception e)
        {
            Debug.Log("erreur chagement xml :" + path.ToString() + "   "+ e.ToString());
            return -1;
        }

        XmlNodeList langueList = document.ChildNodes;
        for(int i=0; i < langueList.Count; i++) // dico
        {
            string key;
            string word;

            XmlNodeList motsList = langueList[i].ChildNodes;// mots
            for (int j = 0; j < motsList.Count; j++)
            {
                XmlNodeList motList = motsList[j].ChildNodes;// mot
                for (int k =0; k < motList.Count;k++)
                {
                    key = motList[k].Attributes[0].InnerText;
                    word = motList[k].ChildNodes.Item(0).InnerText;
                    dictionnaireCourant.Add(key, word);
                }
            }
        }
        return 0;
    }

    public void ChangerLangue(LANGUES nouvelleLangue)
    {
        // normalement pas de probleme car le type d'entree est langue
        if (nouvelleLangue < LANGUES.FR && nouvelleLangue >= LANGUES.MAX)
            return;

        langueCourante = nouvelleLangue; 
        PlayerPrefs.SetInt("langue", (int)langueCourante);
        ChargerDictionnaire();
    }

    public LANGUES DonnerLangue { get { return langueCourante; } }

    public string DonnerMot(string key)
    {
        string mot = "";

        if(dictionnaireCourant == null)
            ChargerDictionnaire();

        bool result = dictionnaireCourant.TryGetValue(key, out mot);

        if (result == false)
            return "No Translate";
        else
            return mot;
    }
}
