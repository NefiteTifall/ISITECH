namespace VroomVroom.Vehicules;

public class AutomobileElectrique : Automobile
{
    // Constructor
    public AutomobileElectrique(string modele, string couleur, int puissance, double espace) : base(modele, couleur, puissance, espace)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
        this.espace = espace;
    }
}