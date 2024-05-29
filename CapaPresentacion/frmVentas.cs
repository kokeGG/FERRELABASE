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
        static private string path = $@"C:\Users\{currentUser}\source\repos\kokeGG\FERRELABASE";
        static string pathXML = path + @"miPrimerXML.xml";

        private Usuario _Usuario;
        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Venta_General", Texto = "Venta General" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            foreach (c_FormaPago formaPago in Enum.GetValues(typeof(c_FormaPago)))
            {
                string nombreFormaPago = ObtenerNombreFormaPago(formaPago);

                cboformapago.Items.Add(new OpcionCombo() { Valor = formaPago, Texto = nombreFormaPago });
                cboformapago.DisplayMember = "Texto";
                cboformapago.ValueMember = "Valor";
                cboformapago.SelectedIndex = 0;

            }


            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidproducto.Text = "0";

            txtpagocon.Text = "";
            txtcambio.Text = "";
            txttotalpagar.Text = "0";
        }
        private string ObtenerNombreFormaPago(c_FormaPago formaPago)
        {
            switch (formaPago)
            {
                case c_FormaPago.Item01:
                    return "Efectivo";
                case c_FormaPago.Item02:
                    return "Cheque nominativo";
                case c_FormaPago.Item03:
                    return "Transferencia electrónica de fondos";
                case c_FormaPago.Item04:
                    return "Tarjeta de crédito";
                case c_FormaPago.Item05:
                    return "Monedero electrónico";
                case c_FormaPago.Item06:
                    return "Dinero electrónico";
                case c_FormaPago.Item08:
                    return "Vales de despensa";
                case c_FormaPago.Item12:
                    return "Dación en pago";
                case c_FormaPago.Item13:
                    return "Pago por subrogación";
                case c_FormaPago.Item14:
                    return "Pago por consignación";
                case c_FormaPago.Item15:
                    return "Condonación";
                case c_FormaPago.Item17:
                    return "Compensación";
                case c_FormaPago.Item23:
                    return "Novación";
                case c_FormaPago.Item24:
                    return "Confusión";
                case c_FormaPago.Item25:
                    return "Remisión de deuda";
                case c_FormaPago.Item26:
                    return "Prescripción o caducidad";
                case c_FormaPago.Item27:
                    return "A satisfacción del acreedor";
                case c_FormaPago.Item28:
                    return "Tarjeta de débito";
                case c_FormaPago.Item29:
                    return "Tarjeta de servicios";
                case c_FormaPago.Item30:
                    return "Aplicación de anticipos";
                case c_FormaPago.Item31:
                    return "Intermediario pagos";
                case c_FormaPago.Item99:
                    return "Por definir";
                default:
                    return "";
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
                    txtnombrecliente.Text = modal._Cliente.NombreCompleto;
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
                    txtprecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
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
                    row.Cells["SubTotal"].Value.ToString(),
                });
            }


            int idcorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idcorrelativo);
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
                if (cboformapago.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una forma de pago");
                    return;
                }
                //Obtener numero de certificado ............................

                //string pathCer = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\RFC para la autenticación de Certificación\CSD_Pruebas_CFDI_SPR190613I52\CSD_Pruebas_CFDI_SPR190613I52.cer";
                //string pathKey = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\RFC para la autenticación de Certificación\CSD_Pruebas_CFDI_SPR190613I52\CSD_Pruebas_CFDI_SPR190613I52.key";

                string pathCer = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\Personas Fisicas\FIEL_KICR630120NX3_20190528153320\kicr630120nx3.cer";
                string pathKey = $@"C:\Users\{currentUser}\Downloads\Certificados_de_Prueba\Certificados_de_Prueba\RFC-PAC-SC (2019)\Personas Fisicas\FIEL_KICR630120NX3_20190528153320\Claveprivada_FIEL_KICR630120NX3_20190528_153320.key";
                string clavePrivada = "12345678a";

                //Obtenemos el numero
                string numeroCertificado, aa, b, c;
                SelloDigital.leerCER(pathCer, out aa, out b, out c, out numeroCertificado);

                //LLENAR CLASE DE COMPROBANTEE
                Comprobante oComprobante = new Comprobante();
                oComprobante.Version = "4.1";
                oComprobante.Serie = "B";
                oComprobante.Folio = "1";
                oComprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                //oComprobante.Sello = ""; //PROCESO XML SAT
                //oComprobante.FormaPago = c_FormaPago.Item27;
                OpcionCombo opcionSeleccionada = (OpcionCombo)cboformapago.SelectedItem;
                c_FormaPago formaPagoSeleccionada = (c_FormaPago)opcionSeleccionada.Valor;
                oComprobante.FormaPago = formaPagoSeleccionada;
                oComprobante.NoCertificado = numeroCertificado; //PROCESO XML SAT
                //oComprobante.Certificado = ""; //PROCESO XML SAT
                oComprobante.SubTotal = 10m;
                oComprobante.Descuento = 1;
                oComprobante.Moneda = c_Moneda.MXN;
                oComprobante.Total = 9;
                oComprobante.TipoDeComprobante = c_TipoDeComprobante.I;
                oComprobante.MetodoPago = c_MetodoPago.PUE;
                oComprobante.LugarExpedicion = "39860"; //cpdigo postal
                ComprobanteEmisor oEmisor = new ComprobanteEmisor();
                oEmisor.Rfc = "VAZK980807PP2";
                oEmisor.Nombre = "KATE VIVIAN VAZQUEZ ZARATE";
                oEmisor.RegimenFiscal = c_RegimenFiscal.Item605;

                ComprobanteReceptor oReceptor = new ComprobanteReceptor();
                oReceptor.Nombre = txtnombrecliente.Text.ToString();
                oReceptor.Rfc = txtdocumentocliente.Text.ToString();
                oReceptor.UsoCFDI = c_UsoCFDI.G01;
                oReceptor.ResidenciaFiscal = c_Pais.MEX;
                //ASIGNO EMISOR Y RECEPTOR
                oComprobante.Emisor = oEmisor;
                oComprobante.Receptor = oReceptor;

                List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();
                //TODO: ITERAR SOBRE LOS PRODUTOS SELECCIONADOS
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.IsNewRow)
                        continue;
                    ComprobanteConcepto oConcepto = new ComprobanteConcepto();

                    oConcepto.Importe = Convert.ToDecimal(row.Cells["SubTotal"].Value);
                    oConcepto.ClaveProdServ = row.Cells["ClaveSat"].Value.ToString();
                    oConcepto.Cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value);
                    oConcepto.ClaveUnidad = row.Cells["UnidadSat"].Value.ToString();
                    oConcepto.Descripcion = row.Cells["Producto"].Value.ToString();
                    oConcepto.ValorUnitario = Convert.ToDecimal(row.Cells["Precio"].Value.ToString());
                    oConcepto.Descuento = 0;

                    lstConceptos.Add(oConcepto);

                }


                //ComprobanteConcepto oConcepto = new ComprobanteConcepto();
                //oConcepto.Importe = 10m;
                //oConcepto.ClaveProdServ = "cf3f3";
                //oConcepto.Cantidad = 1;
                //oConcepto.ClaveUnidad = "H87";
                //oConcepto.Descripcion = "Un misil para la guerra";
                //oConcepto.ValorUnitario = 10m;
                //oConcepto.Descuento = 1;
                //lstConceptos.Add(oConcepto);
                oComprobante.Conceptos = lstConceptos.ToArray();

                //CREAR XML 
                CreateXML(oComprobante);

                string cadenaOriginal = "";

                XmlUrlResolver resolver = new XmlUrlResolver();

                string pathxsl = $@"C:\Users\{currentUser}\source\repos\kokeGG\FERRELABASE\CapaPresentacion\Utilidades\CFDI40\cadenaoriginal_4_0.xslt";
                //string pathxsl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utilidades", "CFDI40", "cadenaoriginal_4_0.xslt");

                Console.WriteLine(pathxsl);
                XslCompiledTransform transformador = new XslCompiledTransform(true);
                try
                {
                    //transformador.Load(pathxsl);
                    transformador.Load(pathxsl, XsltSettings.TrustedXslt, resolver);

                } catch (Exception ex)
                {
                    MessageBox.Show($"Error {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (StringWriter sw = new StringWriter())
                    using (XmlWriter xwo= XmlWriter.Create(sw, transformador.OutputSettings))
                {
                    transformador.Transform(pathXML, xwo);
                    cadenaOriginal = sw.ToString();
                }

                SelloDigital oSelloDigital = new SelloDigital();
                oComprobante.Certificado = oSelloDigital.Certificado(pathCer);
                oComprobante.Sello = oSelloDigital.Sellar(cadenaOriginal, pathKey, clavePrivada);

                CreateXML(oComprobante);

                //TIMBRE DEL XML
                ServiceReferenceFC.RespuestaCFDi respuestaCFDI = new ServiceReferenceFC.RespuestaCFDi();

                byte[] bXML = System.IO.File.ReadAllBytes(pathXML);

                ServiceReferenceFC.TimbradoClient oTimbrado = new ServiceReferenceFC.TimbradoClient();

                respuestaCFDI = oTimbrado.TimbrarTest("KICR630120NX3", "E494fC9e4A1d83Aa0100", bXML);

                if (respuestaCFDI.Documento == null)
                {
                    Console.WriteLine(respuestaCFDI.Mensaje);
                } else
                {
                    System.IO.File.WriteAllBytes(pathXML, respuestaCFDI.Documento);
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

                    try
                    {
                        pd.Print();
                    } catch (Exception err)
                    {
                        MessageBox.Show("Error al imprimir: " + err);
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

        private static void CreateXML(Comprobante oComprobante)
        {
            //SERIALIZAMOS.---------------------------------------------------------------
            
            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");


            XmlSerializer oXmlSerializar = new XmlSerializer(typeof(Comprobante));

            string sXml = "";

            using (var sww = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter writter = XmlWriter.Create(sww))
                {
                    oXmlSerializar.Serialize(writter, oComprobante, xmlNameSpace);
                    sXml = sww.ToString();
                }
            }

            //guardamos el archivo
            System.IO.File.WriteAllText(pathXML, sXml);
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
                    txtprecio.Text = oProducto.PrecioVenta.ToString("0.00");
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
                DocumentoCliente = txtdocumentocliente.Text,
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

                    try
                    {
                        pd.Print();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error al imprimir: " + err);
                    }
                }



                var imprimirA4 = MessageBox.Show("¿Imprimir cotizacion en tamaño carta?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
