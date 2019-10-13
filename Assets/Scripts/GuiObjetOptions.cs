using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiObjetOptions : GuiObjet
{
    protected GuiObjet _obj;
   /* private GameObject _gui;*/

   /* public override void Afficher()
    {

    }*/


    public GuiObjetOptions(GuiObjet obj)
    {
        
    }
}


class GuiObjetAvecAttaque : GuiObjetOptions
{
    public GuiObjetAvecAttaque(GuiObjet originale) : base(originale) { }
}

class GuiObjetAvecCharge : GuiObjetOptions
{
    public GuiObjetAvecCharge(GuiObjet originale) : base(originale) { }
}

class GuiObjetAvecPerception : GuiObjetOptions
{
    public GuiObjetAvecPerception(GuiObjet originale) : base(originale) { }
}

class GuiObjetAvecEscalade : GuiObjetOptions
{
    public GuiObjetAvecEscalade(GuiObjet originale) : base(originale) { }
}

class GuiObjetAvecDetectionPiege : GuiObjetOptions
{
    public GuiObjetAvecDetectionPiege(GuiObjet originale) : base(originale) { }
}

class GuiObjetAvecCoupSournois: GuiObjetOptions
{
    public GuiObjetAvecCoupSournois(GuiObjet originale) : base(originale) { }
}