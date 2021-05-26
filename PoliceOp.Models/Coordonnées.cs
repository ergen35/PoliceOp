﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
namespace PoliceOp.Models
{
    public class Coordonnées
    {
        public int ID { get; set; }
        public string Rue { get; set; }
        public string NumeroParcelle { get; set; }
        public string NumeroChambre { get; set; }
        public GeoCoordinate CoordonneesGeo { get; set; }

    }
}
