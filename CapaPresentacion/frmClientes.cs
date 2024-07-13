using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            int[] regimenes = { 601, 603, 605, 606, 607, 608, 609, 610, 611, 612, 614, 615, 616, 620, 621, 622, 623, 624, 625, 626, 628, 629, 630 };


            foreach (int regimen in regimenes)
            {
                string nombreRegimen = ObtenerNombreRegimen(regimen);

                if (!string.IsNullOrWhiteSpace(nombreRegimen))
                {
                    cboregimenfiscal.Items.Add(new OpcionCombo() { Valor = regimen, Texto = nombreRegimen });
                    cboregimenfiscal.DisplayMember = "Texto";
                    cboregimenfiscal.ValueMember = "Valor";
                    cboregimenfiscal.SelectedIndex = 0;
                }
            }



            //MOSTRAR TODOS LOS USUARIOS
            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdCliente,
                    item.Codigo,
                    item.NombreCompleto,
                    item.RFC,
                    item.Correo, 
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.CodigoPostal,
                    item.Colonia,
                    item.Calle,
                    item.NoExt,
                    item.NoInt,
                    item.Municipio,
                    item.Poblacion, 
                    item.Edo,
                    item.Regimen
                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpcionCombo regimenSelected = (OpcionCombo)cboregimenfiscal.SelectedItem;
            int regimenSeleccionado = (int)regimenSelected.Valor;

            Cliente objcliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Codigo = txtcodigo.Text,
                NombreCompleto = txtnombrecompleto.Text,
                RFC = txtrfc.Text,
                Calle = txtcalle.Text,
                Correo = txtcorreo.Text,
                NoExt = txtnoext.Text,
                NoInt = txtnoint.Text,
                Municipio = txtmunicipio.Text,
                Colonia = txtcolonia.Text,
                CodigoPostal = txtcodigo.Text,
                Poblacion = txtpoblacion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false,
                Edo = txtestado.Text,
                Regimen = regimenSeleccionado.ToString(),
            };

            if (objcliente.IdCliente == 0)
            {
                int idgenerado = new CN_Cliente().Registrar(objcliente, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {
                        "",
                        idgenerado,
                        txtcodigo.Text,
                        txtnombrecompleto.Text, 
                        txtrfc.Text, 
                        txtcorreo.Text,
                        ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboestado.SelectedItem).Texto.ToString(),
                        txtcp.Text,
                        txtcolonia.Text,
                        txtcalle.Text, 
                        txtnoext.Text,
                        txtnoint.Text,
                        txtmunicipio.Text,
                        txtpoblacion.Text,
                        txtestado.Text,
                        regimenSeleccionado.ToString()
                    });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }


            }
            else
            {
                bool resultado = new CN_Cliente().Editar(objcliente, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Codigo"].Value = txtcodigo.Text;
                    row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                    row.Cells["RazonSocial"].Value = txtrfc.Text;
                    row.Cells["RFC"].Value = txtcalle.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txtnoext.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                    row.Cells["CP"].Value = txtnoint.Text;
                    row.Cells["DIRECCION"].Value = txtmunicipio.Text;
                    row.Cells["Colonia"].Value = txtcolonia.Text;
                    row.Cells["Numero"].Value = txtcp.Text;
                    row.Cells["Ciudad"].Value = txtpoblacion.Text;
                    row.Cells["Edo"].Value = txtestado.Text;
                    row.Cells["RegimenFiscal"].Value = regimenSeleccionado.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private string ObtenerNombreRegimen(int regimenFiscal)
        {
            switch (regimenFiscal)
            {
                case 601:
                    return "601 - General de Ley Personas Morales";
                case 603:
                    return "603 - Personas Morales con Fines no Lucrativos";
                case 605:
                    return "605 - Sueldos y Salarios e Ingresos Asimilados a Salarios";
                case 606:
                    return "606 - Arrendamiento";
                case 607:
                    return "607 - Régimen de Enajenación o Adquisición de Biene";
                case 608:
                    return "608 - Demás Ingresos";
                case 609:
                    return "609 - Consolidación";
                case 610:
                    return "610 - Residentes en el Extranjero sin Establecimiento Permanente en México";
                case 611:
                    return "611 - Ingresos por Dividendos (socios y accionistas)";
                case 612:
                    return "612 - Personas Físicas con Actividades Empresariales y Profesionales";
                case 614:
                    return "614 - Ingresos por intereses";
                case 615:
                    return "615 - Régimen de los ingresos por obtención de premios";
                case 616:
                    return "616 - Sin obligaciones fiscales";
                case 620:
                    return "620 - Sociedades Cooperativas de Producción que optan por diferir sus ingresos";
                case 621:
                    return "621 - Incorporación Fiscal";
                case 622:
                    return "622 - Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras";
                case 623:
                    return "623 - Opcional para Grupos de Sociedades";
                case 624:
                    return "624 - Coordinados";
                case 625:
                    return "625 - Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas";
                case 626:
                    return "626 - Régimen Simplificado de Confianza";
                case 628:
                    return "628 - Hidrocarburos";
                case 629:
                    return "629 - De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales";
                case 630:
                    return "630 - Enajenación de acciones en bolsa de valores";
                default:
                    return "";
            }
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtcodigo.Text = "";
            txtnombrecompleto.Text = "";
            txtrfc.Text = "";
            txtcalle.Text = "";
            txtcorreo.Text = "";
            txtnoext.Text = "";
            cboestado.SelectedIndex = 0;
            txtcodigo.Select();
            txtnoint.Text = "";
            txtmunicipio.Text = "";
            txtestado.Text = "";
            txtcolonia.Text = "";
            txtpoblacion.Text = "";
            txtcp.Text = "";
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.checked__1_.Width;
                var h = Properties.Resources.checked__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.checked__1_, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtcodigo.Text = dgvdata.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtrfc.Text = dgvdata.Rows[indice].Cells["RFC"].Value.ToString();
                    txtcalle.Text = dgvdata.Rows[indice].Cells["Calle"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    txtnoext.Text = dgvdata.Rows[indice].Cells["NoExt"].Value.ToString();
                    txtnoint.Text = dgvdata.Rows[indice].Cells["NoInt"].Value.ToString();
                    txtmunicipio.Text = dgvdata.Rows[indice].Cells["Municipio"].Value.ToString();
                    txtcolonia.Text = dgvdata.Rows[indice].Cells["Colonia"].Value.ToString();
                    txtcp.Text = dgvdata.Rows[indice].Cells["CP"].Value.ToString();
                    txtpoblacion.Text = dgvdata.Rows[indice].Cells["Poblacion"].Value.ToString();
                    txtestado.Text = dgvdata.Rows[indice].Cells["Edo"].Value.ToString();

                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    //foreach (OpcionCombo oc in cboregimenfiscal.Items)
                    //{
                    //    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["RegimenFiscal"].Value))
                    //    {
                    //        int indice_combo = cboregimenfiscal.Items.IndexOf(oc);
                    //        cboregimenfiscal.SelectedIndex = indice_combo;
                    //        break;
                    //    }
                    //}
                    try
                    {
                        bool itemFound = false;
                        foreach (OpcionCombo oc in cboregimenfiscal.Items)
                        {
                            // Verificar si los valores son nulos o vacíos antes de intentar la conversión
                            if (string.IsNullOrWhiteSpace(oc.Valor.ToString()) || string.IsNullOrWhiteSpace(dgvdata.Rows[indice].Cells["Regimen"].Value.ToString()))
                            {
                                continue;
                            }

                            if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["Regimen"].Value))
                            {
                                int indice_combo = cboregimenfiscal.Items.IndexOf(oc);
                                cboregimenfiscal.SelectedIndex = indice_combo;
                                itemFound = true;
                                break;
                            }
                        }

                        // Si no se encontró ningún elemento coincidente, seleccionar el primer elemento
                        if (!itemFound)
                        {
                            cboregimenfiscal.SelectedIndex = 0;
                        }
                    }
                    catch (Exception)
                    {
                        // En caso de cualquier excepción, seleccionar el primer elemento
                        cboregimenfiscal.SelectedIndex = 0;
                    }
                }


            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el cliente", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objcliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                    };
                    bool respuesta = new CN_Cliente().Eliminar(objcliente, out mensaje);
                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void clean_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
