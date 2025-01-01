using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfEngineer.Components.DataFiles
{
    public class Produit
    {
        public int ProduitId { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Prix { get; set; }
    }

    public class Stat
    {
        public int StatId { get; set; }
        public int ProduitId { get; set; }
        public double Vente { get; set; }
        public DateTime DateVente { get; set; }
    }

    public class Magasigner
    {
        public int MagasignerId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;
        public string NumTel { get; set; } = string.Empty;
        public string Horaire { get; set; } = string.Empty;
        public decimal Salaire { get; set; }
    }

    public class Client
    {
        public int ClientId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string NumTel { get; set; } = string.Empty;
    }

    public class Commande
    {
        public int CommandeId { get; set; }
        public int ClientId { get; set; }
        public int ProduitId { get; set; }
        public int Quantite { get; set; }
        public DateOnly DateCommande { get; set; }
    }
}
