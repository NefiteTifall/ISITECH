using VroomVroom.Vehicules;

namespace VroomVroom.Classes;

public class FabriqueVehiculeEssence : IFabriqueVehicule
{
    public Automobile CreateAutomobile(string modele, string couleur, int puissance, double espace)
    {
        return new AutomobileEssence(modele, couleur, puissance, espace);
    }
    public Scooter CreateScooter(string modele, string couleur, int puissance) 
    {
        return new ScooterEssence(modele, couleur, puissance);
    }
}