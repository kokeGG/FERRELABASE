﻿using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleCotizacion : Form
    {
        public frmDetalleCotizacion()
        {
            InitializeComponent();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Cotizacion oCotizacion = new CN_Cotizacion().ObtenerCotizacion(txtbusqueda.Text);
            if (oCotizacion.IdCotizacion != 0)
            {
                txtnumerodocumento.Text = oCotizacion.NumeroDocumento;
                txttipodocumento.Text = oCotizacion.TipoDocumento;
                txtusuario.Text = oCotizacion.oUsuario.NombreCompleto;
                txtfecha.Text = oCotizacion.FechaRegistro;
                txtid.Text = oCotizacion.IdCotizacion.ToString();

                txtdoccliente.Text = oCotizacion.DocumentoCliente;
                txtnombrecliente.Text = oCotizacion.NombreCliente;

                dgvdata.Rows.Clear();
                foreach (Detalle_Cotizacion dv in oCotizacion.oDetalleCotizacion)
                {
                    dgvdata.Rows.Add(new object[] { dv.oProducto.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                }

                txtmontototal.Text = oCotizacion.MontoTotal.ToString("0.00");
                txtmontopago.Text = oCotizacion.MontoPago.ToString("0.00");
                txtmontocambio.Text = oCotizacion.MontoCambio.ToString("0.00");
            }
        }

        private void clean_Click(object sender, EventArgs e)
        {
            txtfecha.Text = "";
            txttipodocumento.Text = "";
            txtmontopago.Text = "";
            txtusuario.Text = "";
            txtdoccliente.Text = "";
            txtnombrecliente.Text = "";
            dgvdata.Rows.Clear();
            txtmontototal.Text = "";
            txtmontocambio.Text = "";
            txtnumerodocumento.Text = "";
            txtid.Text = "";
        }

        private void btndescargar_Click(object sender, EventArgs e)
        {
            if (txttipodocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.ticketCotizacion.ToString();
            Negocio odatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", "COTIZACIÓN");
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtnumerodocumento.Text);

            Texto_Html = Texto_Html.Replace("@doccliente", txtdoccliente.Text);
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
            Texto_Html = Texto_Html.Replace("@montototal", txtmontototal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", txtmontopago.Text);
            Texto_Html = Texto_Html.Replace("@cambio", txtmontocambio.Text);


            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Cotizacion{0}.pdf", txtnumerodocumento.Text);
            savefile.Filter = "Pdf Files | *.pdf";

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

        private void btncancelar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                var eliminarDialog = MessageBox.Show("¿Desea eliminar la cotización?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (eliminarDialog == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cotizacion objcotizacion = new Cotizacion
                    {
                        IdCotizacion = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Cotizacion().EliminarCotizacion(objcotizacion, out mensaje);
                    if (respuesta)
                    {
                        MessageBox.Show("Cotización eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfecha.Text = "";
                        txttipodocumento.Text = "";
                        txtmontopago.Text = "";
                        txtusuario.Text = "";
                        txtdoccliente.Text = "";
                        txtnombrecliente.Text = "";
                        dgvdata.Rows.Clear();
                        txtmontototal.Text = "";
                        txtmontocambio.Text = "";
                        txtnumerodocumento.Text = "";
                        txtid.Text = "";
                    } else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
