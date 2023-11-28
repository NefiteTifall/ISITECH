using VroomVroom.Classes;

namespace VroomVroom.Vehicules;

public class Automobile : Catalogue
{
    public Automobile(string modele, string couleur, int puissance, double espace)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
        this.espace = espace;
    }
}