using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace proyecto_practica
{
    public partial class ejemplo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//SI LA PAGINA NO SE ESTA CARGANDO POR PRIMERA VEZ
            {
                CargarMarcas();
                CargarArticulos();
            }
        }

        private void CargarMarcas()
        {
            Negocio negocio = new Negocio();
            List<Marca> listaMarcas = negocio.listarMarcas();

            ddlMarcas.Items.Clear();
            ddlMarcas.Items.Add(new ListItem("SELECCIONA UNA MARCA", "0"));

            foreach (var marca in listaMarcas)
            {
                ddlMarcas.Items.Add(new ListItem(marca.Descripcion, marca.Id.ToString()));
            }
        }

        private void CargarArticulos()
        {
            Negocio negocio = new Negocio();
            List<Articulo> listaArticulos = negocio.listarArticulos(); 

            tablaArticulos.Controls.Clear(); 
            foreach (var articulo in listaArticulos)
            {
                TableRow fila = new TableRow();

                
                TableCell celdaImagen = new TableCell();
                Image imagenArticulo = new Image
                {
                    ImageUrl = articulo.ImagenUrl, 
                    Width = Unit.Pixel(100),
                    Height = Unit.Pixel(100)
                };
               

                fila.Cells.Add(new TableCell { Text = articulo.Codigo });
                fila.Cells.Add(new TableCell { Text = articulo.Nombre });
                fila.Cells.Add(new TableCell { Text = articulo.Marca });
                fila.Cells.Add(new TableCell { Text = articulo.Categoria });
                fila.Cells.Add(new TableCell { Text = articulo.Precio.ToString("C") }); 
                celdaImagen.Controls.Add(imagenArticulo);
                fila.Cells.Add(celdaImagen);
                tablaArticulos.Controls.Add(fila);
            }
        }

        protected void FiltrarArticulos(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "SELECT A.Codigo, A.Nombre, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl " +
                              "FROM Articulos A " +
                              "INNER JOIN Marcas M ON A.IdMarca = M.Id " +
                              "INNER JOIN Categorias C ON A.IdCategoria = C.Id " +
                              "LEFT JOIN Imagenes I ON A.Id = I.Id " +
                              "WHERE 1=1"; 

            // FILTRO PARA MARCAS
            if (ddlMarcas.SelectedValue != "0")
            {
                consulta += " AND A.IdMarca = @marca"; 
                datos.setearParametro("@marca", ddlMarcas.SelectedValue);
            }

            // FILTRO PARA NOMBRES
            if (!string.IsNullOrEmpty(filtroNombre.Text))
            {
                consulta += " AND A.Nombre LIKE @nombre";
                datos.setearParametro("@nombre", "%" + filtroNombre.Text + "%");
            }

            try
            {
                datos.setearConsulta(consulta);
                datos.realizarLectura();

                tablaArticulos.Controls.Clear();  
                while (datos.Lector.Read())
                {
                    TableRow fila = new TableRow();

                    fila.Cells.Add(new TableCell { Text = datos.Lector["Codigo"].ToString() });
                    fila.Cells.Add(new TableCell { Text = datos.Lector["Nombre"].ToString() });
                    fila.Cells.Add(new TableCell { Text = datos.Lector["Marca"].ToString() });
                    fila.Cells.Add(new TableCell { Text = datos.Lector["Categoria"].ToString() });
                    fila.Cells.Add(new TableCell { Text = ((decimal)datos.Lector["Precio"]).ToString("C") }); 

                    // CELDA DE IMAGEN
                    Image imagenArticulo = new Image
                    {
                        ImageUrl = !string.IsNullOrEmpty(datos.Lector["ImagenUrl"].ToString()) ? datos.Lector["ImagenUrl"].ToString() : "ruta/a/imagen/predeterminada.jpg",
                        Width = Unit.Pixel(100), 
                        Height = Unit.Pixel(100) 
                    };

                    TableCell cellImagen = new TableCell();
                    cellImagen.Controls.Add(imagenArticulo);
                    fila.Cells.Add(cellImagen);

                    tablaArticulos.Controls.Add(fila);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}