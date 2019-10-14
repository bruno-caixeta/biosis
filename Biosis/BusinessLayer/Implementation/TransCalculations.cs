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
        Document document = new Document();

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
            var HoP = Convert.ToDouble(dadoTrans.NumeroIndividuos) / (Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));
            var HaP = 2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) / (2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));

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
            var HoP = Convert.ToDouble(dadoTrans.NumeroIndividuos) / (Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));
            var HaP = 2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) / (2 * Convert.ToDouble(dadoTrans.NumeroIndividuos) + Convert.ToDouble(controle.NumeroIndividuos));

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
            var composto = "";
            document = new Document(PageSize.A4.Rotate());
            document.SetMargins(3, 2, 3, 2);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            PdfPTable table = new PdfPTable(15);
            table.WidthPercentage = 90;
            Font fontHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 18);
            Font fontText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);
            Font fontItalic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.ITALIC);

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

            var headerExplanation = new Paragraph("Manchas por indivíduo ( no. de manchas ) diag. estatístico");
            var headerExplanationCell = new PdfPCell(headerExplanation);
            headerExplanationCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            headerExplanationCell.Colspan = 12;

            var msp = new Paragraph("MSP");
            var mspCell = new PdfPCell(msp);
            mspCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            mspCell.Colspan = 3;

            var msg = new Paragraph("MSG");
            var msgCell = new PdfPCell(msg);
            msgCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            msgCell.Colspan = 3;

            var mg = new Paragraph("MG");
            var mgCell = new PdfPCell(mg);
            mgCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            mgCell.Colspan = 3;

            var tm = new Paragraph("TM");
            var tmCell = new PdfPCell(tm);
            tmCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tmCell.Colspan = 3;

            table.AddCell("Genótipos");
            table.AddCell("N. de");
            table.AddCell(headerExplanationCell);
            table.AddCell("Total");

            table.AddCell("e Conc.");
            table.AddCell("Indiv.");
            table.AddCell(mspCell);
            table.AddCell(msgCell);
            table.AddCell(mgCell);
            table.AddCell(tmCell);
            table.AddCell("manchas");

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

            var flairCell = new PdfPCell(new Paragraph("mwh/flr³", fontItalic));
            flairCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(flairCell);

            table.CompleteRow();

            var itemCompostoControleCell = new PdfPCell(new Paragraph(controle.Composto, fontText));
            itemCompostoControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemCompostoControleCell);

            var itemIndividuosControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.NumeroIndividuos), fontText));
            itemIndividuosControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemIndividuosControleCell);

            var itemMSPProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MSP) / Convert.ToDouble(controle.NumeroIndividuos), 2)), fontText));
            itemMSPProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSPProportionControleCell);

            var itemMSPNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MSP), fontText));
            itemMSPNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSPNumberControleCell);

            table.AddCell("");

            var itemMSGProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MSG) / Convert.ToDouble(controle.NumeroIndividuos), 2)), fontText));
            itemMSGProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSGProportionControleCell);

            var itemMSGNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MSG), fontText));
            itemMSGNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSGNumberControleCell);

            table.AddCell("");

            var itemMGProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MG) / Convert.ToDouble(controle.NumeroIndividuos), 2)), fontText));
            itemMGProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMGProportionControleCell);

            var itemMGNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MG), fontText));
            itemMGNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMGNumberControleCell);

            table.AddCell("");

            var itemTotalProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.TotalManchas) / Convert.ToDouble(controle.NumeroIndividuos), 2)), fontText));
            itemTotalProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemTotalProportionControleCell);

            var itemTotalNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.TotalManchas), fontText));
            itemTotalNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemTotalNumberControleCell);

            table.CompleteRow();

            foreach (var item in research.TransData)
            {
                composto = item.Composto;

                var itemCompostoCell = new PdfPCell(new Paragraph(item.Composto, fontText));
                itemCompostoCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemCompostoCell);

                var itemIndividuosCell = new PdfPCell(new Paragraph(Convert.ToString(item.NumeroIndividuos), fontText));
                itemIndividuosCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemIndividuosCell);                

                var itemMSPProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MSP)/Convert.ToDouble(item.NumeroIndividuos), 2)), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemMSPNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSP), fontText));
                itemMSPNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPNumberCell);

                var itemDiagnosticoMSPCell = new PdfPCell(new Paragraph(CalcularMSPTrans(item)));
                itemDiagnosticoMSPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSPCell);                

                var itemMSGProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MSG) / Convert.ToDouble(item.NumeroIndividuos), 2)), fontText));
                itemMSGProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSGProportionCell);

                var itemMSGNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSG), fontText));
                itemMSGNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSGNumberCell);

                var itemDiagnosticoMSGCell = new PdfPCell(new Paragraph(CalcularMSGTrans(item)));
                itemDiagnosticoMSGCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSGCell);

                var itemMGProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MG) / Convert.ToDouble(item.NumeroIndividuos), 2)), fontText));
                itemMGProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMGProportionCell);

                var itemMGNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MG), fontText));
                itemMGNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMGNumberCell);

                var itemDiagnosticoMGCell = new PdfPCell(new Paragraph(CalcularMGTrans(item)));
                itemDiagnosticoMGCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMGCell);

                var itemTotalProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.TotalManchas) / Convert.ToDouble(item.NumeroIndividuos), 2)), fontText));
                itemTotalProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTotalProportionCell);

                var itemTotalNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.TotalManchas), fontText));
                itemTotalNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTotalNumberCell);

                var itemDiagnosticoTotalCell = new PdfPCell(new Paragraph(CalcularTotalTrans(item)));
                itemDiagnosticoTotalCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoTotalCell);

                table.CompleteRow();
            }

            document.Add(new Paragraph("Frequência de manchas mutantes observadas nos descendentes trans-heterozigotos de Drosophila melanogaster, do cruzamento padrão, tratados com diferentes concentrações de " + composto + ".\n\n"));

            document.Add(table);
            document.Close();
            return memoryStream;
        }
    }
}
