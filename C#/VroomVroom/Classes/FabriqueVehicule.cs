using VroomVroom.Vehicules;

namespace VroomVroom.Classes;

public interface IFabriqueVehicule 
{
    Automobile CreateAutomobile(string modele, string couleur, int puissance, double espace);
    Scooter CreateScooter(string modele, string couleur, int puissance);
}