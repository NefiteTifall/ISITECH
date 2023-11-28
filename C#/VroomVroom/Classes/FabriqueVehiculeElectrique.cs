using VroomVroom.Vehicules;

namespace VroomVroom.Classes;

public class FabriqueVehiculeElectrique : IFabriqueVehicule
{
    public Automobile CreateAutomobile(string modele, string couleur, int puissance, double espace)
    {
        return new AutomobileElectrique(modele, couleur, puissance, espace);
    }
    public Scooter CreateScooter(string modele, string couleur, int puissance) 
    {
        return new ScooterElectrique(modele, couleur, puissance);
    }
}