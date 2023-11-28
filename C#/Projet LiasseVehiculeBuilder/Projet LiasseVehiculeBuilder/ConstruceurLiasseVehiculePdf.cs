namespace Projet_LiasseVehiculeBuilder;

public class ConstruceurLiasseVehiculePdf : ConstructeurLiasseVehicule
{
    public ConstruceurLiasseVehiculePdf()
    {
        this.liasse = new LiassePdf();
    }

    public override void construitBonDeCommande(string nomClient)
    {
        string document;
        document = "Bon de commande Client (PDF) : " + nomClient + "\n";
        this.liasse.ajouteDocument(document);
    }
}