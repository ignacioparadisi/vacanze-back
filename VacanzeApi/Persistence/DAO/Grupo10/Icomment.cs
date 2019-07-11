using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using System.Data;
using System.Collections.Generic;
namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo10

{
   public interface Icomment 
   {
       int addcomment(Comment comment);
       
       List<Comment>  Getcomment(int i );

       int Deletecomment(int id);

       bool Updatecomment(Comment comment);
        
    }
}