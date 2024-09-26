using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient; 
namespace proyecto_practica
{
    public class Negocio
    {
        private SqlConnection Conexion;
        private SqlCommand Comando;
        private SqlDataReader Lector;

     
        public List<Articulo> listarArticulos()
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                Conexion = new SqlConnection("server=.\\SQLEXPRESS; database=PROMOS_DB; integrated security=true;");
                Comando = new SqlCommand("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion as Marca, C.Descripcion as Categoria, I.ImagenUrl " +
                                          "FROM ARTICULOS A " +
                                          "INNER JOIN MARCAS M ON A.IdMarca = M.Id " +
                                          "INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                                          "LEFT JOIN IMAGENES I ON A.Id = I.Id", Conexion);

                Conexion.Open();
                Lector = Comando.ExecuteReader();

                while (Lector.Read())
                {
                    Articulo articulo = new Articulo
                    {
                        Id = (int)Lector["Id"],
                        Codigo = (string)Lector["Codigo"],
                        Nombre = (string)Lector["Nombre"],
                        Descripcion = (string)Lector["Descripcion"],
                        Precio = (decimal)Lector["Precio"],
                        Marca = (string)Lector["Marca"],
                        Categoria = (string)Lector["Categoria"],
                        ImagenUrl = Lector["ImagenUrl"] != DBNull.Value ? (string)Lector["ImagenUrl"] : "https://via.placeholder.com/150"//SI NO TIENE IMAGEN DA UNA POR DEFECTO
                    };
                    lista.Add(articulo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Lector != null) Lector.Close();
                if (Conexion != null) Conexion.Close();
            }
            return lista;
        }


        public List<Marca> listarMarcas()
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.realizarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca
                    {
                        Id = (int)datos.Lector["Id"],
                        Descripcion = (string)datos.Lector["Descripcion"]
                    };
                    listaMarcas.Add(marca);
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaMarcas;
        }
    }
}
