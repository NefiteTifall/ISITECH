namespace VroomVroom.Vehicules;

public class ScooterEssence : Scooter
{
    public ScooterEssence(string modele, string couleur, int puissance) : base(modele, couleur, puissance)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
    }
}