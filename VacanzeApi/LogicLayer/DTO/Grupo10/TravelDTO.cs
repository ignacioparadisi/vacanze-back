using System;
using System.Collections.Generic;
namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo10{

     public class TravelDTO
    {
        public string tra_descr  { get; set; }
        public int tra_use_fk { get; set; }
         public int Idtravel { get; set; }
         
         public string tra_name  { get; set; }

         public DateTime tra_ini { get; set; }

        public DateTime tra_end { get; set; }
    }
}