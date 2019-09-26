using Biosis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Biosis.BusinessLayer.Interface;

namespace Biosis.BusinessLayer.Implementation
{
    public class TransCalculations: ITransCalculations
    {
        private readonly ITransDataBusinessLayer _transDataBusinessLayer;

        TransData controle = new TransData();
        public TransCalculations(ITransDataBusinessLayer transDataBusinessLayer)
        {
            _transDataBusinessLayer = transDataBusinessLayer;
        }
        public string CalcularMSPTrans(TransData dadoTrans)
        {
            double HoP = Convert.ToDouble(dadoTrans.NumeroIndividuos) / (Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));
            double HaP = 2*Convert.ToDouble(dadoTrans.NumeroIndividuos) / (2*Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));

            var HoProb = 1 - Binomial.CDF(HoP, dadoTrans.MSP + controle.MSP, dadoTrans.MSP) + Binomial.PMF(HoP, dadoTrans.MSP + controle.MSP, dadoTrans.MSP);

            var HaProb = Binomial.CDF(HaP, dadoTrans.MSP + controle.MSP, dadoTrans.MSP);

            if(HoProb > 0.05 && HaProb <= 0.05)
            {
                return "-";
            }

            else if (HoProb > 0.05 && HaProb > 0.05)
            {
                return "i";
            }

            else if (HoProb <= 0.05 && HaProb <= 0.05)
            {
                return "f+";
            }

            else if (HoProb <= 0.05 && HaProb > 0.05)
            {
                return "+";
            }
            else
            {
                return "";
            }

        }

        public string CalcularMSGTrans(TransData dadoTrans)
        {
            var HoP = Convert.ToDouble(dadoTrans.NumeroIndividuos) / (Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));
            var HaP = 2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) / (2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));

            var HoProb = 1 - Binomial.CDF(HoP, dadoTrans.MSG + controle.MSG, dadoTrans.MSG) + Binomial.PMF(HoP, dadoTrans.MSG + controle.MSG, dadoTrans.MSG);

            var HaProb = Binomial.CDF(HaP, dadoTrans.MSG + controle.MSG, dadoTrans.MSG);

            if (HoProb > 0.05 && HaProb <= 0.05)
            {
                return "-";
            }

            else if (HoProb > 0.05 && HaProb > 0.05)
            {
                return "i";
            }

            else if (HoProb <= 0.05 && HaProb <= 0.05)
            {
                return "f+";
            }

            else if (HoProb <= 0.05 && HaProb > 0.05)
            {
                return "+";
            }
            else
            {
                return "";
            }

        }

        public string CalcularMGTrans(TransData dadoTrans)
        {
            var HoP = dadoTrans.NumeroIndividuos / dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;
            var HaP = 2 * dadoTrans.NumeroIndividuos / 2 * dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;

            var HoProb = 1 - Binomial.CDF(HoP, dadoTrans.MG + controle.MG, dadoTrans.MG) + Binomial.PMF(HoP, dadoTrans.MG + controle.MG, dadoTrans.MG);

            var HaProb = Binomial.CDF(HaP, dadoTrans.MG + controle.MG, dadoTrans.MG);

            if (HoProb > 0.05 && HaProb <= 0.05)
            {
                return "-";
            }

            else if (HoProb > 0.05 && HaProb > 0.05)
            {
                return "i";
            }

            else if (HoProb <= 0.05 && HaProb <= 0.05)
            {
                return "f+";
            }

            else if (HoProb <= 0.05 && HaProb > 0.05)
            {
                return "+";
            }
            else
            {
                return "";
            }

        }

        public string CalcularTotalTrans(TransData dadoTrans)
        {
            var HoP = dadoTrans.NumeroIndividuos / dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;
            var HaP = 2 * dadoTrans.NumeroIndividuos / 2 * dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;

            var HoProb = 1 - Binomial.CDF(HoP, dadoTrans.TotalManchas + controle.TotalManchas, dadoTrans.TotalManchas) + Binomial.PMF(HoP, dadoTrans.TotalManchas + controle.TotalManchas, dadoTrans.TotalManchas);

            var HaProb = Binomial.CDF(HaP, dadoTrans.TotalManchas + controle.TotalManchas, dadoTrans.TotalManchas);

            if (HoProb > 0.05 && HaProb <= 0.05)
            {
                return "-";
            }

            else if (HoProb > 0.05 && HaProb > 0.05)
            {
                return "i";
            }

            else if (HoProb <= 0.05 && HaProb <= 0.05)
            {
                return "f+";
            }

            else if (HoProb <= 0.05 && HaProb > 0.05)
            {
                return "+";
            }
            else
            {
                return "";
            }

        }

        public MemoryStream GeneratePdfReport(TransData controle, Research research)
        {
            this.controle = controle;
            Document document = new Document(PageSize.A4.Rotate());
            document.SetMargins(3, 2, 3, 2);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            PdfPTable table = new PdfPTable(15);
            table.WidthPercentage = 90;
            Font fontHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 18);
            Font fontText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);

            Paragraph column1 = new Paragraph("(mM)", fontHeader);
            Paragraph column2 = new Paragraph("( N )", fontHeader);
            Paragraph column3 = new Paragraph("(1-2 céls)b", fontHeader);
            Paragraph column4 = new Paragraph("(>2 céls)b", fontHeader);
            Paragraph column5 = new Paragraph("mwhc", fontHeader);
            Paragraph m2 = new Paragraph("M = 2", fontHeader);
            Paragraph m5 = new Paragraph("M = 5", fontHeader);
            Paragraph n = new Paragraph("( n )", fontHeader);
            Paragraph controleParagraph = new Paragraph("Controle", fontText);
            Paragraph emptyParagraph = new Paragraph("");

            var headerCell1 = new PdfPCell(column1);
            headerCell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell2 = new PdfPCell(column2);
            headerCell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell3 = new PdfPCell(column3);
            headerCell3.Colspan = 3;
            headerCell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell4 = new PdfPCell(column4);
            headerCell4.Colspan = 3;
            headerCell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCellempty = new PdfPCell(emptyParagraph);
            headerCellempty.Colspan = 3;
            headerCellempty.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell5 = new PdfPCell(column5);
            headerCell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCellempty2 = new PdfPCell(emptyParagraph);
            headerCellempty2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell6 = new PdfPCell(m2);
            headerCell6.Colspan = 3;
            headerCell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell7 = new PdfPCell(m5);
            headerCell7.Colspan = 3;
            headerCell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            var headerCell8 = new PdfPCell(n);
            headerCell8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

            table.AddCell(headerCell1);
            table.AddCell(headerCell2);
            table.AddCell(headerCell3);
            table.AddCell(headerCell4);
            table.AddCell(headerCellempty);
            table.AddCell(headerCellempty);
            table.AddCell(headerCell5);

            table.AddCell(headerCellempty2);
            table.AddCell(headerCellempty2);
            table.AddCell(headerCell6);
            table.AddCell(headerCell7);
            table.AddCell(headerCell7);
            table.AddCell(headerCell6);
            table.AddCell(headerCell8);

            foreach(var item in research.TransData)
            {
                var itemCompostoCell = new PdfPCell(new Paragraph(item.Composto, fontText));
                itemCompostoCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemCompostoCell);

                var itemIndividuosCell = new PdfPCell(new Paragraph(Convert.ToString(item.NumeroIndividuos), fontText));
                itemIndividuosCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemIndividuosCell);

                var itemMSPProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Convert.ToDouble(item.MSP)/Convert.ToDouble(item.NumeroIndividuos)), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemMSPNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSP), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemDiagnosticoMSPCell = new PdfPCell(new Paragraph(CalcularMSPTrans(item)));
                itemDiagnosticoMSPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSPCell);                

                var itemMSGProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Convert.ToDouble(item.MSG) / Convert.ToDouble(item.NumeroIndividuos)), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemMSGNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSG), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemDiagnosticoMSGCell = new PdfPCell(new Paragraph(CalcularMSGTrans(item)));
                itemDiagnosticoMSGCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSGCell);
                table.CompleteRow();
            }

            document.Add(table);
            document.Close();
            return memoryStream;
        }
    }
}
