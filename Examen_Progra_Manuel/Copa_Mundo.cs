using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA_WORLD_CUP
{
    internal class Copa_Mundo()
    {
        // Información de conexión a la base de datos
        private string connectionString = "Server = localhost; Database = db_universidad; Uid=root;Pwd=alex123";


        //constructor
        public Copa_Mundo(string servidor, string usur, string pwd)
        {
            connectionString = "Server=" + servidor + ";Database=db_universidad;Uid=" + usur + ";Pwd=" + pwd + "alex123";
        }

        // Método para leer todos los personajes
        public DataTable LeerSelecciones()
        {
            DataTable personajes = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM db_universidad.mundial2018";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(personajes);
                    }
                }
            }

            return personajes;
        }

        public int CrearSleccion(int Id_Selecciones, string Nombre_Selecciones, string Jugadores_Destacados, int No_Clasificaciones_Mundial, string Frases_Selecciones, DateTime Fecha_Ultimo_Mundial_Ganado, int Valor_Plantilla)
        {
            // Validar los datos antes de la inserción
            if (Id_Selecciones <= 0 || string.IsNullOrWhiteSpace(Nombre_Selecciones) ||
            string.IsNullOrWhiteSpace(Jugadores_Destacados) || No_Clasificaciones_Mundial <= 0 ||
            string.IsNullOrWhiteSpace(Frases_Selecciones) || Fecha_Ultimo_Mundial_Ganado == null ||
            Valor_Plantilla <= 0 || string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Uno o más parámetros son inválidos.");
            }



            // Intentar la inserción y manejar posibles errores
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO mundial2018 (Id_Selecciones, Nombre_Selecciones, Jugadores_Destacados, No_Clasificaciones_Mundial, Frases_Selecciones, Fecha_Ultimo_Mundial_Ganado, Valor_Plantilla) VALUES (@Id_Selecciones, @Nombre_Selecciones, @Jugadores_Destacados, @No_Clasificaciones_Mundial, @Frases_Selecciones, @Fecha_Ultimo_Mundial_Ganado, @Valor_Plantilla)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Selecciones", Id_Selecciones);
                        command.Parameters.AddWithValue("@Nombre_Selecciones", Nombre_Selecciones);
                        command.Parameters.AddWithValue("@Jugadores_Destacados", Jugadores_Destacados);
                        command.Parameters.AddWithValue("@No_Clasificaciones_Mundial", No_Clasificaciones_Mundial);
                        command.Parameters.AddWithValue("@Frases_Selecciones", Frases_Selecciones);
                        command.Parameters.AddWithValue("@Fecha_Ultimo_Mundial_Ganado", Fecha_Ultimo_Mundial_Ganado);
                        command.Parameters.AddWithValue("@Valor_Plantilla", Valor_Plantilla);

                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar errores específicos de MySQL
                throw new Exception("Error de MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                throw new Exception("Error: " + ex.Message);
            }
        }

        public int ActualizarSelecciones(int Id_Selecciones, string Nombre_Selecciones, string Jugadores_Destacados, int No_Clasificaciones_Mundial, string Frases_Selecciones, DateTime Fecha_Ultimo_Mundial_Ganado, int Valor_Plantilla)
        {
            // Validar los datos antes de la inserción
            if (Id_Selecciones <= 0 || string.IsNullOrWhiteSpace(Nombre_Selecciones) ||
            string.IsNullOrWhiteSpace(Jugadores_Destacados) || No_Clasificaciones_Mundial <= 0 ||
            string.IsNullOrWhiteSpace(Frases_Selecciones) || Fecha_Ultimo_Mundial_Ganado == null ||
            Valor_Plantilla <= 0 || string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Uno o más parámetros son inválidos.");
            }

            //ACTUALIZACION DE SELECCIONES
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE db_universidad.mundial2018 SET Nombre_Selecciones = @Nombre_Selecciones, Jugadores_Destacados = @Jugadores_Destacados, No_Clasificaciones_Mundial = @No_Clasificaciones_Mundial, Frases_Selecciones = @Frases_Selecciones, Fecha_Ultimo_Mundial_Ganado = @Fecha_Ultimo_Mundial_Ganado, Valor_Plantilla = @Valor_Plantilla WHERE Id_Selecciones = @Id_Selecciones;";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Selecciones", Id_Selecciones);
                        command.Parameters.AddWithValue("@Nombre_Selecciones", Nombre_Selecciones);
                        command.Parameters.AddWithValue("@Jugadores_Destacados", Jugadores_Destacados);
                        command.Parameters.AddWithValue("@No_Clasificaciones_Mundial", No_Clasificaciones_Mundial);
                        command.Parameters.AddWithValue("@Frases_Selecciones", Frases_Selecciones);
                        command.Parameters.AddWithValue("@Fecha_Ultimo_Mundial_Ganado", Fecha_Ultimo_Mundial_Ganado);
                        command.Parameters.AddWithValue("@Valor_Plantilla", Valor_Plantilla);

                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar errores específicos de MySQL
                Console.WriteLine("Error de MySQL: " + ex.Message);
                return -1; // O cualquier otro valor que indique un error
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine("Error: " + ex.Message);
                return -1; // O cualquier otro valor que indique un error
            }


        }

        //ELIMINAR SELECCION
        public int EliminarSeleccion(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM db_universidad.mundial2018 WHERE Id_Selecciones = @Id_Selecciones;";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Selecciones", Id_Selecciones);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de MySQL: " + ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }


    }
}