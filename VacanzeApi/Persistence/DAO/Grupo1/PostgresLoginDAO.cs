//Aqui va lo mismo a lo que iba en el Repository

using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo1;

using vacanze_back.VacanzeApi.Persistence.Repository.Grupo1;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo1{
    public class PostgresLoginDAO: LoginDAO{

        /// <summary>
        ///     Metodo para buscar si los valores introducidos por el usuario existen.
        /// </summary>
        /// <param name="email">Email unico del usuario almacenado en la base de datos</param>
        /// <param name="password">Contraseña que corresponde al email del usuario, llega sin encriptar</param>
        /// <returns>
        ///     Lista de los datos del usuario, los cuales son: ID, nombre del usuario, apellido del usuario
        ///     el nombre del ID y el ID de la n-n user_role
        /// </returns>
        /// <exception cref="LoginUserNotFoundException">Lanzada si la consulta no retorna nada </exception>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        /// <exception cref="InvalidOperationException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        public Login SessionLogin(string email, string password)
        {
            try{
                var passwordMD5 = Encryptor.Encrypt(password);
                Console.WriteLine(passwordMD5);
                var table=PgConnection.Instance.ExecuteFunction("loginFindInfo(@Email,@Password)", email, passwordMD5);

                if (table.Rows.Count == 0){
                    throw new LoginUserNotFoundException("La contraseña o el correo son incorrectos");
                }
                else{
                    var Id = Convert.ToInt32(table.Rows[0][0]);
                    var Name = table.Rows[0][1].ToString();
                    var LastName = table.Rows[0][2].ToString();
                    var roles = new List<Role>();
                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        Role objRol = new Role(Convert.ToInt32(table.Rows[i][3]), table.Rows[i][4].ToString());
                        roles.Add(objRol);
                    }
                    var user = new Login(Id, roles, email,password );
                    return user;
                }
            }
            catch (DatabaseException){

                Console.WriteLine("Database Exception");
                throw new LoginUserNotFoundException("No se puedo buscar el correo en la base de datos");
            }
            catch (InvalidOperationException){

                Console.WriteLine("InvalidOperationException Exception");
                throw new LoginUserNotFoundException("No se llenaron los campos necesarios para poder el login");            
            }

        }

        
        /// <summary>
        ///     Metodo para hacer el cambio de la contraseña.
        /// </summary>
        /// <param name="email">Email unico del usuario almacenado en la base de datos</param>
        /// <returns>
        ///     Lista de los datos del usuario, los cuales son: nombre del usuario, apellido del usuario,
        ///     email del ususario y la nueva contraseña.
        /// </returns>
        /// <exception cref="PasswordRecoveryException">Lanzada si la consulta no retorna nada </exception>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>

        public Login Recovery(string email){
            try{
                var table = PgConnection.Instance.ExecuteFunction("recoveryPassword(@email)",email);

                if(table.Rows.Count == 0){
                    throw new PasswordRecoveryException("El correo no existe");
                }
                else{
                    var Id = Convert.ToInt32(table.Rows[0][0]);
                    var user_email = table.Rows[0][1].ToString();
                    var password = table.Rows[0][2].ToString();
                    Console.WriteLine("Clave en texto plano: ");
                    Console.WriteLine(password);
                    var user = new Login(Id, user_email,password );
                    return user;
                }
            }
            catch (DatabaseException){

                Console.WriteLine("Database Exception");
                throw new PasswordRecoveryException("No se puedo buscar el correo en la base de datos");
            }
        }
    }
}