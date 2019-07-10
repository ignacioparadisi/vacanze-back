//aqui pongo todos los nombres de los metodos por alguna razon
using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo1
{
    public interface LoginDAO
    {
        Login SessionLogin(string email, string password);
        Login Recovery(string email);
    }
}