namespace VroomVroom.Vehicules;

public class ScooterElectrique : Scooter
{
    public ScooterElectrique(string modele, string couleur, int puissance) : base(modele, couleur, puissance)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
    }
}