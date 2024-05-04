using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Business;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloBusiness negocio = new ArticuloBusiness();
            try
            {
                art.Codigo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.Precio = decimal.Parse(txtPrecio.Text);
                art.ImagenUrl = txtImagen.Text;
                art.Marca = (Marca)cboMarca.SelectedItem;
                art.Categoria = (Categoria)cboCategoria.SelectedItem;

                negocio.Agregar(art);
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaBusiness marcaNegocio = new Business.MarcaBusiness();
            CategoriaBusiness categoriaNegocio = new CategoriaBusiness();
            try
            {
                cboMarca.DataSource = marcaNegocio.Listar();
                cboCategoria.DataSource = categoriaNegocio.Listar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtImagen.Text);
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                if (imagen.ToUpper().Contains("HTTPS"))
                {

                    pbxImagen.Load(imagen);
                }
                else
                {
                    pbxImagen.Image = Image.FromFile("C:\\Users\\Administrador\\Desktop\\Maxi Programa Nivel 2 C#\\TPFinalNivel2_Frenquelli\\Imagenes\\placeholder.png");
                }
            }
            catch (Exception ex)
            {

                pbxImagen.Image = Image.FromFile("C:\\Users\\Administrador\\Desktop\\Maxi Programa Nivel 2 C#\\TPFinalNivel2_Frenquelli\\Imagenes\\placeholder.png");
            }
        }
    }
}
