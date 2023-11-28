namespace Projet_LiasseVehiculeBuilder;

public class ConstructeurLiasseVehicule
{
    public ILiasse liasse { get; set; }
    public virtual void construitBonDeCommande(string nomClient)
    {
        
    }
    public virtual void construitDemandeImmatriculation(string nomDemandeur)
    {
        
    }
    public ILiasse resultat()
    {
        return this.liasse;
    }
}