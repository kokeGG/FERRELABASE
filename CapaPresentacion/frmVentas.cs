using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using XSDToXML.Utils;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;


namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        static string currentUser = Environment.UserName;
        //static private string path = $@"C:\Users\{currentUser}\source\repos\kokeGG\FERRELABASE";
        //static private string path = $@"C:\BASECOT\";

        //static string pathXML = path + @"XML.xml";
        //static string pathXML = path;


        private Usuario _Usuario;
        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }
        public string pathXML(string txtrfc, string numeroDocumento)
        {
            string path = $@"C:\PARAFACTURAR\{txtrfc}_{numeroDocumento}.xml";
            return path;
        }
        private void frmVentas_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Venta_General", Texto = "Venta General" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            int[] formasPago = { 01, 02, 03, 04, 05, 06, 08, 12, 13, 14, 15, 17, 23, 24, 25, 26, 27, 28, 29, 30, 31, 99 };


            foreach (int formaPago in formasPago)
            {
                string nombreFormaPago = ObtenerNombreFormaPago(formaPago);

                if (!string.IsNullOrWhiteSpace(nombreFormaPago))
                {
                    cboformapago.Items.Add(new OpcionCombo() { Valor = formaPago, Texto = nombreFormaPago });
                    cboformapago.DisplayMember = "Texto";
                    cboformapago.ValueMember = "Valor";
                    cboformapago.SelectedIndex = 0;
                }
            }

            //cbocfdi.Items.Clear();

            foreach (c_UsoCFDI cfdi in Enum.GetValues(typeof(c_UsoCFDI)))
            {
                string nombrecfdi = ObtenerNombreCFDI(cfdi);

                if (!string.IsNullOrWhiteSpace(nombrecfdi))
                {
                    cbocfdi.Items.Add(new OpcionCombo() { Valor = cfdi, Texto = nombrecfdi });
                    cbocfdi.DisplayMember = "Texto";
                    cbocfdi.ValueMember = "Valor";
                    cbocfdi.SelectedIndex = 0;
                }

                //cbocfdi.Items.Add(new OpcionCombo() { Valor = cfdi, Texto = nombrecfdi });
                //cbocfdi.DisplayMember = "Texto";
                //cbocfdi.ValueMember = "Valor";
                //cbocfdi.SelectedIndex = 0;
            }


            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidproducto.Text = "0";

            txtpagocon.Text = "";
            txtcambio.Text = "";
            txttotalpagar.Text = "0";
        }

        private string ObtenerNombreFormaPago(int formaPago)
        {
            switch (formaPago)
            {
                case 01:
                    return "Efectivo";
                case 02:
                    return "Cheque nominativo";
                case 03:
                    return "Transferencia electrónica de fondos";
                case 04:
                    return "Tarjeta de crédito";
                case 05:
                    return "Monedero electrónico";
                case 06:
                    return "Dinero electrónico";
                case 08:
                    return "Vales de despensa";
                case 12:
                    return "Dación en pago";
                case 13:
                    return "Pago por subrogación";
                case 14:
                    return "Pago por consignación";
                case 15:
                    return "Condonación";
                case 17:
                    return "Compensación";
                case 23:
                    return "Novación";
                case 24:
                    return "Confusión";
                case 25:
                    return "Remisión de deuda";
                case 26:
                    return "Prescripción o caducidad";
                case 27:
                    return "A satisfacción del acreedor";
                case 28:
                    return "Tarjeta de débito";
                case 29:
                    return "Tarjeta de servicios";
                case 30:
                    return "Aplicación de anticipos";
                case 31:
                    return "Intermediario pagos";
                case 99:
                    return "Por definir";
                default:
                    return "";
            }
        }

        private string ObtenerNombreCFDI(c_UsoCFDI cfdi)
        {
            switch (cfdi)
            {
                case c_UsoCFDI.G01:
                    return "Adquisición de mercancias";
                case c_UsoCFDI.G03:
                    return "Gastos en general";
                case c_UsoCFDI.I01:
                    return "Construcciones";
                case c_UsoCFDI.S01:
                    return "Sin efectos fiscales";
                default:
                    return "";
            }
        }

        private c_RegimenFiscal RegimenFiscal(string regimen)
        {
            switch (regimen)
            {
                case "601":
                    return c_RegimenFiscal.Item601;
                case "603":
                    return c_RegimenFiscal.Item603;
                case "605":
                    return c_RegimenFiscal.Item605;
                case "606":
                    return c_RegimenFiscal.Item606;
                case "607":
                    return c_RegimenFiscal.Item607;
                case "608":
                    return c_RegimenFiscal.Item608;
                case "609":
                    return c_RegimenFiscal.Item609;
                case "610":
                    return c_RegimenFiscal.Item610;
                case "611":
                    return c_RegimenFiscal.Item611;
                case "612":
                    return c_RegimenFiscal.Item612;
                case "614":
                    return c_RegimenFiscal.Item614;
                case "615":
                    return c_RegimenFiscal.Item615;
                case "616":
                    return c_RegimenFiscal.Item616;
                case "620":
                    return c_RegimenFiscal.Item620;
                case "621":
                    return c_RegimenFiscal.Item621;
                case "622":
                    return c_RegimenFiscal.Item622;
                case "623":
                    return c_RegimenFiscal.Item623;
                case "624":
                    return c_RegimenFiscal.Item624;
                case "625":
                    return c_RegimenFiscal.Item625;
                case "626":
                    return c_RegimenFiscal.Item626;
                case "628":
                    return c_RegimenFiscal.Item628;
                case "629":
                    return c_RegimenFiscal.Item629;
                case "630":
                    return c_RegimenFiscal.Item630;
                default:
                    return 0;
            }
        }


        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtdocumentocliente.Text = modal._Cliente.RFC;
                    txtdoccliente.Text = modal._Cliente.Codigo;
                    txtnombrecliente.Text = modal._Cliente.NombreCompleto;
                    txtcp.Text = modal._Cliente.CodigoPostal;
                    txtregimen.Text = modal._Cliente.Regimen;
                    txtcodigoproducto.Select();
                }
                else
                {
                    txtdocumentocliente.Select();
                }

            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodigoproducto.Text = modal._Producto.Codigo;
                    txtnombreproducto.Text = modal._Producto.Nombre;
                    txtprecio.Text = modal._Producto.Precio.ToString("0.00");
                    txtstock.Text = modal._Producto.Stock.ToString();
                    txtclavesat.Text = modal._Producto.ClaveSat.ToString();
                    txtunidadsat.Text = modal._Producto.UnidadSat.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodigoproducto.Select();
                }

            }
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            txttotalpagar.Text = total.ToString("0.00");
        }

        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodigoproducto.Text = "";
            txtnombreproducto.Text = "";
            txtprecio.Text = "";
            txtstock.Text = "";
            txtcantidad.Value = 1;
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete__1_.Width;
                var h = Properties.Resources.delete__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete__1_, new System.Drawing.Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvdata.Rows[index].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvdata.Rows[index].Cells["Cantidad"].Value.ToString()));

                    if (respuesta) {
                        dgvdata.Rows.RemoveAt(index);
                        calcularTotal();
                    }
                     
                }
            }
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }

            }
        }

        private void txtpagocon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtpagocon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }

            }
        }

        private void calcularcambio() {

            if (txttotalpagar.Text.Trim() == "") {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            decimal pagacon;
            decimal total = Convert.ToDecimal(txttotalpagar.Text);

            if (txtpagocon.Text.Trim() == "") {
                txtpagocon.Text = "0";
            }

            if (decimal.TryParse(txtpagocon.Text.Trim(), out pagacon)) {

                if (pagacon < total)
                {
                    txtcambio.Text = "0.00";
                }
                else {
                    decimal cambio = pagacon - total;
                    txtcambio.Text = cambio.ToString("0.00");

                }
            }
        }

        private void txtpagocon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) {
                calcularcambio();
            }
        }

        private void btncrearventa_Click(object sender, EventArgs e)
        {

            if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show("Debe ingresar rfc del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (txtnombrecliente.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IdProducto", typeof(int));
            detalle_venta.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_venta.Rows.Add(new object[] {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }

            string numeroDocumento;

            if(cbotipodocumento.Text == "Factura")
            {
                int idcorrelativo = new CN_Venta().ObtenerCorrelativoFactura();
                numeroDocumento = string.Format("{0:00000}", idcorrelativo);

            } else
            {
                int idcorrelativo = new CN_Venta().ObtenerCorrelativo();
                numeroDocumento = string.Format("{0:00000}", idcorrelativo);
            }
            calcularcambio();

            Venta oVenta = new Venta()
            {

                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtdocumentocliente.Text,
                NombreCliente = txtnombrecliente.Text,
                MontoPago = Convert.ToDecimal(txtpagocon.Text),
                MontoCambio = Convert.ToDecimal(txtcambio.Text),
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };


            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);
            //string usuarioActual = Environment.UserName;
            if (cbotipodocumento.Text == "Factura")
            {
                var datosCorrectos = MessageBox.Show("¿Los datos son correctos?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (datosCorrectos == DialogResult.Yes)
                {
                    if (cboformapago.SelectedItem == null)
                    {
                        MessageBox.Show("Debe seleccionar una forma de pago");
                        return;
                    }

                    bool respuestaAddFolio = new CN_Venta().RegistrarFolioFactura(oVenta, detalle_venta, out mensaje);
                    //Obtener numero de certificado ............................

                    //string pathCer = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\RFC para la autenticación de Certificación\CSD_Pruebas_CFDI_SPR190613I52\CSD_Pruebas_CFDI_SPR190613I52.cer";
                    //string pathKey = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\RFC para la autenticación de Certificación\CSD_Pruebas_CFDI_SPR190613I52\CSD_Pruebas_CFDI_SPR190613I52.key";

                    string pathCer = $@"C:\SELLOS 2021\00001000000506024244.cer";
                    string pathKey = $@"C:\SELLOS 2021\CSD_FERREBASE_VAZK980807PP2_20201216_103716.key";
                    string clavePrivada = "MIKIEM12";


                    //Obtenemos el numero
                    string numeroCertificado, aa, b, c;
                    SelloDigital.leerCER(pathCer, out aa, out b, out c, out numeroCertificado);

                    //LLENAR CLASE DE COMPROBANTEE
                    Comprobante oComprobante = new Comprobante();
                    oComprobante.Version = "4.0";
                    oComprobante.Serie = "B";
                    oComprobante.Folio = numeroDocumento;

                    oComprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                    //oComprobante.Sello = ""; //PROCESO XML SAT
                    //oComprobante.FormaPago = c_FormaPago.Item27;

                    OpcionCombo opcionSeleccionada = (OpcionCombo)cboformapago.SelectedItem;
                    int formaPagoSeleccionada = (int)opcionSeleccionada.Valor;
                    string formaPagoNombre = (string)opcionSeleccionada.Texto;

                    OpcionCombo cfdiSelected = (OpcionCombo)cbocfdi.SelectedItem;
                    c_UsoCFDI cfdiSeleccionado = (c_UsoCFDI)cfdiSelected.Valor;
                    string cfdiNombre = (string)cfdiSelected.Texto;



                    //oComprobante.FormaPago = formaPagoSeleccionada;
                    oComprobante.NoCertificado = numeroCertificado; //PROCESO XML SAT
                    //oComprobante.Certificado = ""; //PROCESO XML SAT
                    //oComprobante.SubTotal = 10m;
                    //oComprobante.Descuento = 1;
                    oComprobante.Moneda = c_Moneda.MXN;
                    //oComprobante.Total = 9;
                    oComprobante.TipoDeComprobante = c_TipoDeComprobante.I;
                    //oComprobante.Exportacion = c_Exportacion.Item01;
                    oComprobante.MetodoPago = c_MetodoPago.PUE;
                    oComprobante.LugarExpedicion = "76343"; //cpdigo postal
                    ComprobanteEmisor oEmisor = new ComprobanteEmisor();
                    //oEmisor.Rfc = "VAZK980807PP2";
                    oEmisor.Rfc = "XIA190128J61";
                    //oEmisor.Nombre = "KATE VIVIAN VAZQUEZ ZARATE";
                    oEmisor.Nombre = "XENON INDUSTRIAL ARTICLES";
                    oEmisor.RegimenFiscal = c_RegimenFiscal.Item622;

                    ComprobanteReceptor oReceptor = new ComprobanteReceptor();
                    oReceptor.Nombre = txtnombrecliente.Text.ToString();
                    //oReceptor.Nombre = "KATE VIVIAN VAZQUEZ ZARATE";
                    oReceptor.Rfc = txtdocumentocliente.Text.ToString();
                    //oReceptor.Rfc = "VAZK980807PP2";
                    oReceptor.UsoCFDI = cfdiSeleccionado;
                    //oReceptor.DomicilioFiscalReceptor = "39860".ToString();
                    oReceptor.DomicilioFiscalReceptor = txtcp.Text.ToString();
                    oReceptor.ResidenciaFiscal = c_Pais.MEX;
                    oReceptor.RegimenFiscalReceptor = RegimenFiscal(txtregimen.Text);
                    //oReceptor.RegimenFiscalReceptor = c_RegimenFiscal.Item606;
                    
                    //ASIGNO EMISOR Y RECEPTOR
                    oComprobante.Emisor = oEmisor;
                    oComprobante.Receptor = oReceptor;

                    decimal totalImpuestosTrasladados = 0.00m;
                    decimal subtotal = 0.00m;
                    decimal baseTotal = 0.0m;
                    List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();

                    var detalleElement = new XElement("DETALLE");

                    //TODO: ITERAR SOBRE LOS PRODUTOS SELECCIONADOS
                    foreach (DataGridViewRow row in dgvdata.Rows)
                    {
                        if (row.IsNewRow)
                            continue;
                        var precio = Math.Round(Convert.ToDouble(row.Cells["Precio"].Value), 2, MidpointRounding.AwayFromZero);
                        var cantidad = Math.Round(Convert.ToDouble(row.Cells["Cantidad"].Value), 2, MidpointRounding.AwayFromZero);
                        var total = precio * cantidad;
                        var precio_sin_impuesto = precio / 1.16;
                        var t_impuesto = (total - (precio_sin_impuesto * cantidad));
                        var renglonElement = new XElement("RENGLON",
                            new XAttribute("Clave", row.Cells["Codigo"].Value.ToString()),
                            new XAttribute("c_ClaveProdServ", row.Cells["ClaveSat"].Value.ToString()),
                            new XAttribute("Nombre", row.Cells["Producto"].Value.ToString()),
                            new XAttribute("Cantidad", row.Cells["Cantidad"].Value.ToString()),
                            new XAttribute("Unidad", row.Cells["ClaveSat"].Value.ToString()),//PZA, KG, ETC...
                            new XAttribute("c_ClaveUnidad", row.Cells["UnidadSat"].Value.ToString()),
                            //new XAttribute("Precio", row.Cells["Precio"].Value.ToString()),//Precio con impuesto unitario / 1.16
                            new XAttribute("Precio", precio_sin_impuesto.ToString()),//Precio con impuesto unitario / 1.16
                            new XAttribute("Descuento", "0.00"),
                            new XAttribute("Importe", precio_sin_impuesto * cantidad), //(precio con impuesto unitario / 1.16) * cantidad
                            new XAttribute("c_ObjetoImp", "02"),
                            new XAttribute("c_Impuesto", "002"),
                            new XAttribute("c_NombreImpuesto", "IVA"),
                            new XAttribute("c_TipoFactor", "TASA"),
                            new XAttribute("c_TasaOCuota", "0.160000"),
                            new XAttribute("t_impuesto", t_impuesto.ToString()),//(Precio * cantidad) - ((precio con impuesto unitario / 1.16) * cantidad)
                            new XAttribute("Total", total.ToString())//(Precio con impuesto * cantidad)
                        );

                        detalleElement.Add(renglonElement);

                        //ComprobanteConcepto oConcepto = new ComprobanteConcepto();

                        //// Crear una instancia de ComprobanteConceptoImpuestosRetencion
                        //ComprobanteConceptoImpuestosTraslado traslado = new ComprobanteConceptoImpuestosTraslado
                        //{
                        //    Impuesto = c_Impuesto.Item002, // ISR
                        //    TipoFactor = c_TipoFactor.Tasa,
                        //    TasaOCuota = 0.160000m, // Asigna el valor correcto para TasaOCuota
                        //    //Importe = Convert.ToDecimal(Convert.ToDouble(row.Cells["PrecioCompra"].Value) * 0.16),
                        //    //Importe = Convert.ToDecimal(Convert.ToDouble(txtpreciocompra.Text) * 0.16),
                        //    //Importe = 16.0000m,
                        //    Importe = Math.Round(Convert.ToDecimal(Convert.ToDouble(row.Cells["PrecioCompra"].Value) * 0.16), 2, MidpointRounding.AwayFromZero) * Convert.ToDecimal(row.Cells["Cantidad"].Value),
                        //    Base = Math.Round(Convert.ToDecimal(row.Cells["PrecioCompra"].Value) * Convert.ToDecimal(row.Cells["Cantidad"].Value), 2, MidpointRounding.AwayFromZero),
                        //    TasaOCuotaSpecified = true,
                        //    ImporteSpecified = true
                        //};
                        //baseTotal += traslado.Base;
                        ////totalImpuestosTrasladados += Convert.ToDecimal(Convert.ToDouble(row.Cells["PrecioCompra"].Value) * 0.16);
                        //totalImpuestosTrasladados += Math.Round(Convert.ToDecimal(Convert.ToDouble(row.Cells["PrecioCompra"].Value) * 0.16), 2, MidpointRounding.AwayFromZero) * Convert.ToDecimal(row.Cells["Cantidad"].Value);
                        //subtotal += Math.Round(Convert.ToDecimal(row.Cells["PrecioCompra"].Value) * Convert.ToDecimal(row.Cells["Cantidad"].Value), 2, MidpointRounding.AwayFromZero);
                        ////subtotal += Convert.ToDecimal(Convert.ToDouble(row.Cells["PrecioCompra"].Value));
                        //// Asignar la instancia de retencion a oConcepto.Impuestos.Retenciones
                        //oConcepto.Impuestos = new ComprobanteConceptoImpuestos
                        //{
                        //    Traslados = new ComprobanteConceptoImpuestosTraslado[] { traslado }
                        //};

                        //oConcepto.Importe = Math.Round(Convert.ToDecimal(row.Cells["PrecioCompra"].Value) * Convert.ToDecimal(row.Cells["Cantidad"].Value), 2, MidpointRounding.AwayFromZero);
                        //oConcepto.ClaveProdServ = row.Cells["ClaveSat"].Value.ToString();
                        //oConcepto.Cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value);
                        //oConcepto.ClaveUnidad = row.Cells["UnidadSat"].Value.ToString();
                        //oConcepto.Descripcion = row.Cells["Producto"].Value.ToString();
                        //oConcepto.ValorUnitario = Convert.ToDecimal(row.Cells["Precio"].Value.ToString());
                        ////oConcepto.Descuento = 0;
                        //oConcepto.ObjetoImp =  c_ObjetoImp.Item02;
                        //oConcepto.NoIdentificacion = row.Cells["Codigo"].Value.ToString();

                        //lstConceptos.Add(oConcepto);
                    }
                    oComprobante.SubTotal = Math.Truncate(subtotal * 100) / 100;
                    oComprobante.Total = decimal.Parse(txttotalpagar.Text);
                    oComprobante.Conceptos = lstConceptos.ToArray();

                    ComprobanteImpuestosTraslado trasladoCFDI = new ComprobanteImpuestosTraslado
                    {
                        Impuesto = c_Impuesto.Item002, // ISR
                        TipoFactor = c_TipoFactor.Tasa,
                        // Asignar los valores correspondientes a las demás propiedades de retencion
                        TasaOCuota = 0.160000m, // Asigna el valor correcto para TasaOCuota
                                                //Importe = Convert.ToDecimal(row.Cells["SubTotal"].Value),
                                                //Importe = Math.Round((Math.Truncate(totalImpuestosTrasladados * 100) / 100), 2, MidpointRounding.AwayFromZero),
                        Importe = Math.Truncate(totalImpuestosTrasladados * 100) / 100,
                        Base = Math.Truncate(baseTotal * 100) / 100,
                        TasaOCuotaSpecified = true,
                        ImporteSpecified = true
                    };

                    // Asignar la instancia de trasladoCFDI a oImpuestos.Traslados
                    ComprobanteImpuestos oImpuestos = new ComprobanteImpuestos
                    {
                        TotalImpuestosTrasladadosSpecified = true,
                        TotalImpuestosTrasladados = (Math.Truncate(totalImpuestosTrasladados * 100) / 100),
                        Traslados = new ComprobanteImpuestosTraslado[] { trasladoCFDI }
                    };

                    // Asignar oImpuestos a los impuestos del concepto o de la factura
                    // Dependiendo de donde necesites asignarlo:
                    oComprobante.Impuestos = oImpuestos;

                    //CREAR XML 
                    //CreateXML(oComprobante, txtdocumentocliente.Text.ToString(), numeroDocumento);
                    var xml = new XDocument(
                        new XElement("MOVIMIENTOS",
                            new XElement("DOCUMENTO",
                                new XAttribute("c_Moneda", "MXN"),
                                new XAttribute("FolioInterno", numeroDocumento),
                                new XAttribute("c_TipoDeComprobante", "I"),
                                new XAttribute("Fecha", DateTime.Now.ToString("yyyy-MM-dd")),
                                new XAttribute("Hora", DateTime.Now.ToString("HH:mm:ss")),
                                new XAttribute("c_UsoCFDI", cfdiSeleccionado),
                                new XAttribute("t_Usocfdi", cfdiNombre),
                                new XAttribute("Importe", "129.31"),
                                new XAttribute("Descuento", "0.00"),
                                new XAttribute("Subtotal", "129.31"),
                                new XAttribute("Iva", "20.69"),
                                new XAttribute("Total", "150.00"),
                                new XAttribute("CondicionPago", "CONTADO"),
                                new XAttribute("c_FormaPago", formaPagoSeleccionada),
                                new XAttribute("CtaPago", ""),
                                new XAttribute("t_FormaPago", formaPagoNombre),
                                new XAttribute("c_MetodoPago", "PUE"),
                                new XAttribute("t_MetodoPago", "Pago en una sola exhibición"),
                                new XAttribute("c_Exportacion", "01"),
                                new XElement("Emisor",
                                    new XAttribute("rfc", txtdocumentocliente.Text),
                                    new XAttribute("nombre", txtnombrecliente.Text),
                                    new XAttribute("FacAtrAdquiriente", ""),
                                    new XElement("DomicilioFiscal",
                                        new XAttribute("calle", "CALLE 4"),
                                        new XAttribute("noExterior", "LOCALES 5 Y 6"),
                                        new XAttribute("noInterior", ""),
                                        new XAttribute("colonia", "ICACOS"),
                                        new XAttribute("localidad", "ACAPULCO"),
                                        new XAttribute("municipio", "ACAPULCO DE JUAREZ"),
                                        new XAttribute("estado", "GUERRERO"),
                                        new XAttribute("pais", "MEXICO"),
                                        new XAttribute("codigoPostal", "39860")
                                    ),
                                    new XElement("RegimenFiscal",
                                        new XAttribute("c_RegimenFiscal", "621"),
                                        new XAttribute("Regimen", "REGIMEN DE INCORPORACION FISCAL")
                                    )
                                ),
                                new XElement("CLIENTE",
                                    new XAttribute("RefCliente", "8574"),
                                    new XAttribute("Cliente", "DISEÑO OBRAS Y MANTENIMIENTO DE INSTALACIONES PETROLERAS DEL"),
                                    new XAttribute("Mail", "brindizamalia@gmail.com"),
                                    new XAttribute("RFC", "DOM0203074Y0"),
                                    new XAttribute("DomicilioFiscalReceptor", "91110"),
                                    new XAttribute("c_RegimenFiscalReceptor", "601"),
                                    new XElement("DOMICILIO",
                                        new XAttribute("Calle", "AV LAZARO CARDENAS"),
                                        new XAttribute("NoExt", "1004"),
                                        new XAttribute("NoInt", "............."),
                                        new XAttribute("Colonia", "RAFAEL LUCIO"),
                                        new XAttribute("Poblacion", "XALAPA VERACRUZ"),
                                        new XAttribute("Municipio", "XALAPA"),
                                        new XAttribute("Estado", "XALAPA VERACRUZ"),
                                        new XAttribute("Pais", "MEXICO"),
                                        new XAttribute("CP", "91110")
                                    )
                                ),
                                detalleElement
                            )
                        )
                    );

                    string filePath = $@"C:\PARAFACTURAR\{numeroDocumento}.xml";
                    xml.Save(filePath);
                    Console.WriteLine($"XML creado exitosamente en {filePath}.");

                    //string cadenaOriginal = "";

                    //XmlUrlResolver resolver = new XmlUrlResolver();

                    ////string pathxsl = $@"C:\Users\{currentUser}\source\repos\kokeGG\FERRELABASE\CapaPresentacion\Utilidades\CFDI40\cadenaoriginal_4_0.xslt";
                    //string pathxsl = $@"C:\PARAFACTURAR\CFDI40\cadenaoriginal_4_0.xslt";

                    ////string pathxsl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utilidades", "CFDI40", "cadenaoriginal_4_0.xslt");

                    //Console.WriteLine(pathxsl);
                    //XslCompiledTransform transformador = new XslCompiledTransform(true);
                    //try
                    //{
                    //    //transformador.Load(pathxsl);
                    //    transformador.Load(pathxsl, XsltSettings.TrustedXslt, resolver);

                    //} catch (Exception ex)
                    //{
                    //    MessageBox.Show($"Error {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //using (StringWriter sw = new StringWriter())
                    //    using (XmlWriter xwo= XmlWriter.Create(sw, transformador.OutputSettings))
                    //{
                    //    transformador.Transform(pathXML(txtdocumentocliente.Text, numeroDocumento), xwo);
                    //    cadenaOriginal = sw.ToString();
                    //}

                    //SelloDigital oSelloDigital = new SelloDigital();
                    //oComprobante.Certificado = oSelloDigital.Certificado(pathCer);
                    //oComprobante.Sello = oSelloDigital.Sellar(cadenaOriginal, pathKey, clavePrivada);

                    //CreateXML(oComprobante, txtdocumentocliente.Text.ToString(), numeroDocumento);

                    //TIMBRE DEL XML

                } else if (datosCorrectos == DialogResult.No)
                {
                    return;
                }

            }

            if (respuesta) {
                var result = MessageBox.Show("Numero de venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numeroDocumento);

                var imprimirTicket = MessageBox.Show("¿Desea imprimir ticket?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (imprimirTicket == DialogResult.Yes)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += (senderPrint, ePrint) =>
                    {
                        Graphics g = ePrint.Graphics;
                        Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                        Font regularFont = new Font("Arial", 7);
                        Font boldFont = new Font("Arial", 9, FontStyle.Bold);

                        int startX = 10;
                        int startY = 10;
                        int offset = 20;


                        g.DrawString("FERRETERA LA BASE ", titleFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("CALLE 4 LOCALES 5 Y 6 COL. ICACOS", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("C.P. 39860 ACAPULCO DE JUAREZ, GRO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("KATE VIVIAN VAZQUEZ ZARATE", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("RFC VAZK980807PP2", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("7445873102", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("ACAPULCO, GRO." + DateTime.Now.ToString(), regularFont, Brushes.Black, startX, startY);

                        startY += offset;

                        string notaVenta = "NOTA DE VENTA No. ";
                        string numDocumento = numeroDocumento;

                        g.DrawString(notaVenta, regularFont, Brushes.Black, startX, startY);
                        SizeF sizeNotaVenta = g.MeasureString(notaVenta, regularFont);
                        g.DrawString(numDocumento, boldFont, Brushes.Black, startX + sizeNotaVenta.Width, startY);
                        SizeF sizeNumDocumento = g.MeasureString(numDocumento, boldFont);

                        //startY += offset;
                        //g.DrawString("NOTA DE VENTA No. B" + numeroDocumento, regularFont, Brushes.Black, startX, startY);

                        //startY += offset;
                        //g.DrawString("ACAPULCO, GRO." + DateTime.Now.ToString(), regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("** COMPROBANTE DE VENTA SIMPLIFICADO **", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("------------------------------------------------", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("CANT.     DESCRIPCION         PRECIO  IMPORTE", regularFont, Brushes.Black, startX, startY);

                        foreach (DataGridViewRow row in dgvdata.Rows)
                        {
                            string nombreProducto = row.Cells["Producto"].Value.ToString();
                            decimal precioProducto = Convert.ToDecimal(row.Cells["Precio"].Value);
                            int cantidadProducto = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            decimal subtotalProducto = Convert.ToDecimal(row.Cells["SubTotal"].Value);

                            startY += offset;
                            //g.DrawString($"{nombreProducto} x  {cantidadProducto}  ..... ${subtotalProducto.ToString("0.00")}", regularFont, Brushes.Black, startX, startY);
                            g.DrawString($"{cantidadProducto}                                     {precioProducto}  {subtotalProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);
                            startY += offset;
                            g.DrawString($"     {nombreProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);
                            //g.DrawString($"{nombreProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);

                        }

                        startY += offset;
                        g.DrawString("------------------------------------------------", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Total: ${txttotalpagar.Text}", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Pago: ${txtpagocon.Text}", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Cambio: ${txtcambio.Text}", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("GRACIAS POR PREFERIR A FERRETERA LA BASE!!!", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("NO SE ACEPTAN DEVOLUCIONES NI CAMBIOS DE", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("MATERIAL CORTADO O EMPAQUE ABIERTO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("TODA DEVOLUCIÓN CAUSARA UN CARGO DEL 20% DEL", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("IMPORTE PAGADO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("NO SE HACEN DEVOLUCIONES DE PAGOS CON", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("TARJETA DE CREDITO O DEBITO", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("Comprobante simplificado de operación", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("con público en general.", regularFont, Brushes.Black, startX, startY);
                    };

                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = pd;

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {

                        try
                        {
                            pd.Print();
                        } catch (Exception err)
                        {
                            MessageBox.Show("Error al imprimir: " + err);
                        }
                    }

                }

                txtdocumentocliente.Text = "";
                txtnombrecliente.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
                txtpagocon.Text = "";
                txtcambio.Text = "";
            }else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private static void CreateXML(Comprobante oComprobante, string doccliente, string numeroDocumento)
        {
            //SERIALIZAMOS.---------------------------------------------------------------

            //XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            //xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            //xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
            //xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");


            //XmlSerializer oXmlSerializar = new XmlSerializer(typeof(Comprobante));

            //string sXml = "";

            //using (var sww = new StringWriterWithEncoding(Encoding.UTF8))
            //{
            //    using (XmlWriter writter = XmlWriter.Create(sww))
            //    {
            //        oXmlSerializar.Serialize(writter, oComprobante, xmlNameSpace);
            //        sXml = sww.ToString();
            //    }
            //}

            ////guardamos el archivo
            //System.IO.File.WriteAllText($@"C:\PARAFACTURAR\{doccliente}_{numeroDocumento}.xml", sXml);
            DateTime now = DateTime.Now;

            
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtprecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecio.Select();
                return;
            }

            if (Convert.ToInt32(txtstock.Text) < Convert.ToInt32(txtcantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {

                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(txtidproducto.Text),
                    Convert.ToInt32(txtcantidad.Value.ToString())
                    );

                if (respuesta)
                {
                    dgvdata.Rows.Add(new object[] {
                        txtidproducto.Text,
                        txtnombreproducto.Text,
                        precio.ToString("0.00"),
                        txtcantidad.Value.ToString(),
                        (txtcantidad.Value * precio).ToString("0.00"),
                        "",
                        txtclavesat.Text,
                        txtunidadsat.Text,
                        txtpreciocompra.Text,
                        txtcodigoproducto.Text,
                    });

                    calcularTotal();
                    limpiarProducto();
                    txtcodigoproducto.Select();
                }
            }

        }

        private void txtcodigoproducto_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtcodigoproducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtcodigoproducto.BackColor = Color.Honeydew;
                    txtidproducto.Text = oProducto.IdProducto.ToString();
                    txtnombreproducto.Text = oProducto.Nombre;
                    txtprecio.Text = oProducto.Precio.ToString("0.00");
                    txtstock.Text = oProducto.Stock.ToString();
                    txtunidadsat.Text = oProducto.UnidadSat.ToString();
                    txtclavesat.Text = oProducto.ClaveSat.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodigoproducto.BackColor = Color.MistyRose;
                    txtidproducto.Text = "0";
                    txtnombreproducto.Text = "";
                    txtprecio.Text = "";
                    txtstock.Text = "";
                    txtunidadsat.Text = "";
                    txtclavesat.Text = "";
                    txtpreciocompra.Text = "";
                    txtcantidad.Value = 1;
                }
            }
        }

        private void txtrfccliente_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Cliente oCliente = new CN_Cliente().Listar().Where(c => c.RFC == txtdocumentocliente.Text && c.Estado == true).FirstOrDefault();

                if (oCliente != null)
                {
                    txtnombrecliente.Text = oCliente.NombreCompleto.ToString();
                    txtdocumentocliente.BackColor = Color.Honeydew;
                }
                else
                {
                    txtdocumentocliente.BackColor = Color.MistyRose;
                    txtnombrecliente.Text = "";
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show("Debe ingresar documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (txtnombrecliente.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la cotización", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            DataTable detalle_cotizacion = new DataTable();

            detalle_cotizacion.Columns.Add("IdProducto", typeof(int));
            detalle_cotizacion.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_cotizacion.Columns.Add("Cantidad", typeof(int));
            detalle_cotizacion.Columns.Add("SubTotal", typeof(decimal));


            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_cotizacion.Rows.Add(new object[] {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }


            int idcorrelativo = new CN_Cotizacion().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idcorrelativo);
            calcularcambio();

            Cotizacion oCotizacion = new Cotizacion()
            {

                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtdoccliente.Text,
                RFC = txtdocumentocliente.Text,
                NombreCliente = txtnombrecliente.Text,
                MontoPago = Convert.ToDecimal(txtpagocon.Text),
                MontoCambio = Convert.ToDecimal(txtcambio.Text),
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };


            string mensaje = string.Empty;
            bool respuesta = new CN_Cotizacion().Registrar(oCotizacion, detalle_cotizacion, out mensaje);
            //string usuarioActual = Environment.UserName;
            if (respuesta)
            {
                var result = MessageBox.Show("Numero de cotización generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numeroDocumento);

                var imprimirTicket = MessageBox.Show("¿Desea imprimir ticket?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (imprimirTicket == DialogResult.Yes)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += (senderPrint, ePrint) =>
                    {
                        Graphics g = ePrint.Graphics;
                        Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                        Font regularFont = new Font("Arial", 7);
                        Font boldFont = new Font("Arial", 9, FontStyle.Bold);

                        int startX = 10;
                        int startY = 10;
                        int offset = 20;

                        g.DrawString("* * *     COTIZACION      * * *", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("FERRETERA LA BASE ", titleFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("CALLE 4 LOCALES 5 Y 6 COL. ICACOS", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("C.P. 39860 ACAPULCO DE JUAREZ, GRO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("7445873102", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("ACAPULCO, GRO." + DateTime.Now.ToString(), regularFont, Brushes.Black, startX, startY);

                        startY += offset;

                        string notaVenta = "NOTA DE COTIZACION No. ";
                        string numDocumento = numeroDocumento;

                        g.DrawString(notaVenta, regularFont, Brushes.Black, startX, startY);
                        SizeF sizeNotaVenta = g.MeasureString(notaVenta, regularFont);
                        g.DrawString(numDocumento, boldFont, Brushes.Black, startX + sizeNotaVenta.Width, startY);
                        SizeF sizeNumDocumento = g.MeasureString(numDocumento, boldFont);

                        //startY += offset;
                        //g.DrawString("NOTA DE VENTA No. B" + numeroDocumento, regularFont, Brushes.Black, startX, startY);

                        //startY += offset;
                        //g.DrawString("ACAPULCO, GRO." + DateTime.Now.ToString(), regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("------------------------------------------------", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("CANT.     DESCRIPCION         PRECIO  IMPORTE", regularFont, Brushes.Black, startX, startY);

                        foreach (DataGridViewRow row in dgvdata.Rows)
                        {
                            string nombreProducto = row.Cells["Producto"].Value.ToString();
                            decimal precioProducto = Convert.ToDecimal(row.Cells["Precio"].Value);
                            int cantidadProducto = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            decimal subtotalProducto = Convert.ToDecimal(row.Cells["SubTotal"].Value);

                            startY += offset;
                            //g.DrawString($"{nombreProducto} x  {cantidadProducto}  ..... ${subtotalProducto.ToString("0.00")}", regularFont, Brushes.Black, startX, startY);
                            g.DrawString($"{cantidadProducto}                                     {precioProducto}  {subtotalProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);
                            startY += offset;
                            g.DrawString($"     {nombreProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);
                            //g.DrawString($"{nombreProducto}", new Font("Arial", 9), Brushes.Black, startX, startY);

                        }

                        startY += offset;
                        g.DrawString("------------------------------------------------", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Total: ${txttotalpagar.Text}", boldFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Pago: ${txtpagocon.Text}", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString($"Cambio: ${txtcambio.Text}", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("GRACIAS POR PREFERIR A FERRETERA LA BASE!!!", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("* PRECIOS SUJETOS A CAMBIOS SIN PREVIO AVISO *", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("NO SE ACEPTAN DEVOLUCIONES NI CAMBIOS DE", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("MATERIAL CORTADO O EMPAQUE ABIERTO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("TODA DEVOLUCIÓN CAUSARA UN CARGO DEL 20% DEL", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("IMPORTE PAGADO.", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("NO SE HACEN DEVOLUCIONES DE PAGOS CON", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("TARJETA DE CREDITO O DEBITO", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("      * PRESENTE ESTA COTIZACION *", regularFont, Brushes.Black, startX, startY);

                        startY += offset;
                        g.DrawString("      * PARA AGILIZAR SU COMPRA *.", regularFont, Brushes.Black, startX, startY);
                    };

                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = pd;

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {

                        try
                        {
                            pd.Print();
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Error al imprimir: " + err);
                        }
                    }
                }



                var imprimirA4 = MessageBox.Show("¿Generar cotizacion en tamaño carta?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (imprimirA4 == DialogResult.Yes)
                {
                    string Texto_Html = Properties.Resources.ticketCotizacion.ToString();
                    Negocio odatos = new CN_Negocio().ObtenerDatos();

                    Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
                    Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
                    Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

                    Texto_Html = Texto_Html.Replace("@tipodocumento", "COTIZACIÓN");
                    Texto_Html = Texto_Html.Replace("@numerodocumento", numeroDocumento.ToString());

                    Texto_Html = Texto_Html.Replace("@doccliente", txtdocumentocliente.Text);
                    Texto_Html = Texto_Html.Replace("@nombrecliente", txtnombrecliente.Text);
                    Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);


                    string filas = string.Empty;
                    foreach (DataGridViewRow row in dgvdata.Rows)
                    {
                        filas += "<tr>";
                        filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                        filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                        filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                        filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                        filas += "</tr>";

                    }
                    Texto_Html = Texto_Html.Replace("@filas", filas);
                    Texto_Html = Texto_Html.Replace("@montototal", txttotalpagar.Text);

                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("Cotizacion_{0}.pdf", numeroDocumento);
                    savefile.Filter = "Pdf Files|*.pdf";

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();

                            bool obtenido = true;
                            byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                            if (obtenido)
                            {
                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                                img.ScaleToFit(60, 60);
                                img.Alignment = iTextSharp.text.Image.UNDERLYING;
                                img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                                pdfDoc.Add(img);
                            }

                            using (StringReader sr = new StringReader(Texto_Html))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            }

                            pdfDoc.Close();
                            stream.Close();
                            MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }

                txtdocumentocliente.Text = "";
                txtnombrecliente.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
                txtpagocon.Text = "";
                txtcambio.Text = "";
            }
            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
