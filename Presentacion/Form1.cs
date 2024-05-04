using Business;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulo;
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
            
        }

        
        
        private void cargar()
        {
            ArticuloBusiness negocio = new ArticuloBusiness();
            try
            {
                listaArticulo = negocio.Listar();
                dgvArticulos.DataSource = listaArticulo;
                OcultarColumna();
                CargarImagen(listaArticulo[0].ImagenUrl);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void OcultarColumna()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }


        private void CargarImagen(string imagen)
        {
           try
            {
                if (imagen.ToUpper().Contains("HTTPS"))
                {
                    
                    pbxArticulo.Load(imagen);
                }
                else
                {
                    pbxArticulo.Image = Image.FromFile("C:\\Users\\Administrador\\Desktop\\Maxi Programa Nivel 2 C#\\TPFinalNivel2_Frenquelli\\Imagenes\\placeholder.png");
                }
            }
            catch (Exception ex)
            {
                
                pbxArticulo.Image = Image.FromFile("C:\\Users\\Administrador\\Desktop\\Maxi Programa Nivel 2 C#\\TPFinalNivel2_Frenquelli\\Imagenes\\placeholder.png");
            }
        }



        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            
                if(dgvArticulos.CurrentRow != null)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    CargarImagen(seleccionado.ImagenUrl);

                }

            
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            frmAltaArticulo nuevo = new frmAltaArticulo();
            nuevo.ShowDialog();
            cargar();
        }
    }
}
