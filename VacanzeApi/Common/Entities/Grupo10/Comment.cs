using System;
using System.Collections.Generic;
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo10
{
    public class Comment
    {
        /*esta clase generara objetos de tipo "comentario", que son los que contienen las
        opiniones de los usuarios hacerca de los servicios que usaron. */
        private int _idcoment;
        public int idcoment{ get{ return _idcoment; } set{ _idcoment = value; } }

        private string _description;
        public String  description{ get{ return _description; } set{ _description = value; } }
        private  DateTime _datetime;
        public DateTime datetime{ get{ return _datetime; } set{ _datetime = value; } }
        private int _idforanea;
        public int idforanea{ get{ return _idforanea; } set{ _idforanea = value; } }

        public Comment (int idcoment , string description, DateTime datetime, int idforanea){
            this._idcoment= idcoment;
            this._description= description;
            this._datetime=datetime;
            this._idforanea= idforanea;
        }
    }
}