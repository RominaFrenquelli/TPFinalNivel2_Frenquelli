using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Business
{
    public class ArticuloBusiness
    {
        public List<Articulo> Listar()
        {
            List<Articulo> listado = new List<Articulo>();
            ConectarDB datos = new ConectarDB();

            try
            {
                datos.SetearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, C.Descripcion Categoria, M.Descripcion Marca, ImagenUrl, Precio from ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id = A.IdCategoria AND M.Id = A.IdMarca");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo art= new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Marca = new Marca();
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Categoria = new Categoria();
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        art.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    art.Precio = (decimal)datos.Lector["Precio"];

                    listado.Add(art);

                }
                return listado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Articulo nuevo)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, Precio, IdMarca, IdCategoria)values(@codigo, @nombre, @descripcion, @imagenUrl, @precio, @idMarca, @idCategoria)");
                datos.SetearParametro("@codigo", nuevo.Codigo);
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@descripcion", nuevo.Descripcion);
                datos.SetearParametro("@imagenUrl", nuevo.ImagenUrl);
                datos.SetearParametro("@precio", nuevo.Precio);
                datos.SetearParametro("@idMarca", nuevo.Marca.Id);
                datos.SetearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
