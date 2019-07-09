using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13
{
    public class ReservationRoomDTO
    {
        public DateTime CheckIn { get; set; }
        
        public DateTime CheckOut { get; set; }
        
        public int Hotel { get; set; }

        public int Fk_user { get; set; }

        public int Fk_pay{ get; set; }
    }
}