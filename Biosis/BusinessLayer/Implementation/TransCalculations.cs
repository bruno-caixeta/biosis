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
            var HoP = dadoTrans.NumeroIndividuos / dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;
            var HaP = 2*dadoTrans.NumeroIndividuos / 2*dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;

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
            var HoP = dadoTrans.NumeroIndividuos / dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;
            var HaP = 2 * dadoTrans.NumeroIndividuos / 2 * dadoTrans.NumeroIndividuos + controle.NumeroIndividuos;

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

        public MemoryStream GeneratePdfReport(TransData controle)
        {
            this.controle = controle;
            Document document = new Document(PageSize.A4);
            document.SetMargins(3, 2, 3, 2);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            PdfPTable table = new PdfPTable(15);
            Font fontHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 18);
            Font fontText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);

            Paragraph column1 = new Paragraph("( mM )", fontHeader);
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
            var headerCell2 = new PdfPCell(column2);
            var headerCell3 = new PdfPCell(column3);
            headerCell3.Colspan = 3;
            var headerCell4 = new PdfPCell(column4);
            headerCell4.Colspan = 3;
            var headerCellempty = new PdfPCell(emptyParagraph);
            headerCellempty.Colspan = 3;            
            var headerCell5 = new PdfPCell(column5);
            var headerCellempty2 = new PdfPCell(emptyParagraph);            
            var headerCell6 = new PdfPCell(m2);
            headerCell6.Colspan = 3;
            var headerCell7 = new PdfPCell(m5);
            headerCell7.Colspan = 3;
            var headerCell8 = new PdfPCell(n);

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

            document.Add(table);
            document.Close();
            return memoryStream;
        }
    }
}
