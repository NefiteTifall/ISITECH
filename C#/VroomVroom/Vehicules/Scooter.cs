using VroomVroom.Classes;

namespace VroomVroom.Vehicules;

public class Scooter : Catalogue
{
    public Scooter(string modele, string couleur, int puissance)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
    }
}