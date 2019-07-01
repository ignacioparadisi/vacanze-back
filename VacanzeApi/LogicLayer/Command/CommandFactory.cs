using System;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Command
{
    public class CommandFactory{

        public static AddBrandCommand createAddBrandCommand(Brand brand){
            return new AddBrandCommand(brand);
        }

        public static GetBrandsCommand createGetBrandsCommand(){
            return new GetBrandsCommand();
        }

        public static GetRestaurantCommand CreateGetRestaurantCommand(int id)
        {
            return new GetRestaurantCommand(id);
        }
    }
}