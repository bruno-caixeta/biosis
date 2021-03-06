﻿using Biosis.Model;
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
            double HoP = Convert.ToDouble(dadoTrans.PopulationNumber) / (Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));
            double HaP = 2*Convert.ToDouble(dadoTrans.PopulationNumber) / (2*Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));

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
            var HoP = Convert.ToDouble(dadoTrans.PopulationNumber) / (Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));
            var HaP = 2 * Convert.ToDouble(dadoTrans.PopulationNumber) / (2 * Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));

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
            var HoP = Convert.ToDouble(dadoTrans.PopulationNumber) / (Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));
            var HaP = 2 * Convert.ToDouble(dadoTrans.PopulationNumber) / (2 * Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));

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
            var HoP = Convert.ToDouble(dadoTrans.PopulationNumber) / (Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));
            var HaP = 2 * Convert.ToDouble(dadoTrans.PopulationNumber) / (2 * Convert.ToDouble(dadoTrans.PopulationNumber) + Convert.ToDouble(controle.PopulationNumber));

            var HoProb = 1 - Binomial.CDF(HoP, dadoTrans.TaintTotal + controle.TaintTotal, dadoTrans.TaintTotal) + Binomial.PMF(HoP, dadoTrans.TaintTotal + controle.TaintTotal, dadoTrans.TaintTotal);

            var HaProb = Binomial.CDF(HaP, dadoTrans.TaintTotal + controle.TaintTotal, dadoTrans.TaintTotal);

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

        public double CalculateTaintInductionFrequencyWithoutCorrectionBySizeAndCorrectionOnData(TransData dadoTrans)
        {
            var taintNumberClass1 = dadoTrans.Class1 - (controle.Class1 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass2 = dadoTrans.Class2 - (controle.Class2 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass3 = dadoTrans.Class3 - (controle.Class3 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass4 = dadoTrans.Class4 - (controle.Class4 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass5 = dadoTrans.Class5 - (controle.Class5 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass6 = dadoTrans.Class6 - (controle.Class6 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass7 = dadoTrans.Class7 - (controle.Class7 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass8 = dadoTrans.Class8 - (controle.Class8 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass9 = dadoTrans.Class9 - (controle.Class9 * dadoTrans.PopulationNumber / controle.PopulationNumber);
            var taintNumberClass10 = dadoTrans.Class10 - (controle.Class10 * dadoTrans.PopulationNumber / controle.PopulationNumber);

            var taintNumberTotal = taintNumberClass1 + taintNumberClass2 + taintNumberClass3 + taintNumberClass4 + taintNumberClass5 + taintNumberClass6 + taintNumberClass7 + taintNumberClass8 + taintNumberClass9 + taintNumberClass10;

            return Convert.ToDouble(taintNumberTotal / ((float)dadoTrans.PopulationNumber * 48800) * 100000);
        }

        public double CalculateTaintInductionFrequencyWithCorrectionBySizeAndCorrectionOnData(TransData dadoTrans)
        {
            var mediumSize = CalculateTaintFrequency(dadoTrans);
            var cloneInductionFrequencyWithCorrection = CalculateTaintInductionFrequencyWithoutCorrectionBySizeAndCorrectionOnData(dadoTrans);
            return Convert.ToDouble(Math.Exp(mediumSize * Math.Log(2) / 4 * (float)cloneInductionFrequencyWithCorrection));
        }

        public double CalculateTaintFrequencyCorrection(TransData dadoTrans)
        {
            var controlClass1Frequency = Convert.ToDouble((float)controle.Class1 / controle.PopulationNumber);
            var controlClass2Frequency = Convert.ToDouble((float)controle.Class2 / controle.PopulationNumber);
            var controlClass3Frequency = Convert.ToDouble((float)controle.Class3 / controle.PopulationNumber);
            var controlClass4Frequency = Convert.ToDouble((float)controle.Class4 / controle.PopulationNumber);
            var controlClass5Frequency = Convert.ToDouble((float)controle.Class5 / controle.PopulationNumber);
            var controlClass6Frequency = Convert.ToDouble((float)controle.Class6 / controle.PopulationNumber);
            var controlClass7Frequency = Convert.ToDouble((float)controle.Class7 / controle.PopulationNumber);
            var controlClass8Frequency = Convert.ToDouble((float)controle.Class8 / controle.PopulationNumber);
            var controlClass9Frequency = Convert.ToDouble((float)controle.Class9 / controle.PopulationNumber);
            var controlClass10Frequency = Convert.ToDouble((float)controle.Class10 / controle.PopulationNumber);

            var dadoTransClass1Frequency = Convert.ToDouble((float)dadoTrans.Class1 / dadoTrans.PopulationNumber);
            var dadoTransClass2Frequency = Convert.ToDouble((float)dadoTrans.Class2 / dadoTrans.PopulationNumber);
            var dadoTransClass3Frequency = Convert.ToDouble((float)dadoTrans.Class3 / dadoTrans.PopulationNumber);
            var dadoTransClass4Frequency = Convert.ToDouble((float)dadoTrans.Class4 / dadoTrans.PopulationNumber);
            var dadoTransClass5Frequency = Convert.ToDouble((float)dadoTrans.Class5 / dadoTrans.PopulationNumber);
            var dadoTransClass6Frequency = Convert.ToDouble((float)dadoTrans.Class6 / dadoTrans.PopulationNumber);
            var dadoTransClass7Frequency = Convert.ToDouble((float)dadoTrans.Class7 / dadoTrans.PopulationNumber);
            var dadoTransClass8Frequency = Convert.ToDouble((float)dadoTrans.Class8 / dadoTrans.PopulationNumber);
            var dadoTransClass9Frequency = Convert.ToDouble((float)dadoTrans.Class9 / dadoTrans.PopulationNumber);
            var dadoTransClass10Frequency = Convert.ToDouble((float)dadoTrans.Class10 / dadoTrans.PopulationNumber);

            var correctionClass1Frequency = Convert.ToDouble((float)dadoTransClass1Frequency - controlClass1Frequency);
            var correctionClass2Frequency = Convert.ToDouble((float)dadoTransClass2Frequency - controlClass2Frequency);
            var correctionClass3Frequency = Convert.ToDouble((float)dadoTransClass3Frequency - controlClass3Frequency);
            var correctionClass4Frequency = Convert.ToDouble((float)dadoTransClass4Frequency - controlClass4Frequency);
            var correctionClass5Frequency = Convert.ToDouble((float)dadoTransClass5Frequency - controlClass5Frequency);
            var correctionClass6Frequency = Convert.ToDouble((float)dadoTransClass6Frequency - controlClass6Frequency);
            var correctionClass7Frequency = Convert.ToDouble((float)dadoTransClass7Frequency - controlClass7Frequency);
            var correctionClass8Frequency = Convert.ToDouble((float)dadoTransClass8Frequency - controlClass8Frequency);
            var correctionClass9Frequency = Convert.ToDouble((float)dadoTransClass9Frequency - controlClass9Frequency);
            var correctionClass10Frequency = Convert.ToDouble((float)dadoTransClass10Frequency - controlClass10Frequency);

            var correctionFrequencySum = correctionClass1Frequency + correctionClass2Frequency + correctionClass3Frequency + correctionClass4Frequency + correctionClass5Frequency + correctionClass6Frequency + correctionClass7Frequency + correctionClass8Frequency + correctionClass9Frequency + correctionClass10Frequency;

            var frequencyXClass1 = correctionClass1Frequency * 1;
            var frequencyXClass2 = correctionClass2Frequency * 2;
            var frequencyXClass3 = correctionClass3Frequency * 3;
            var frequencyXClass4 = correctionClass4Frequency * 4;
            var frequencyXClass5 = correctionClass5Frequency * 5;
            var frequencyXClass6 = correctionClass6Frequency * 6;
            var frequencyXClass7 = correctionClass7Frequency * 7;
            var frequencyXClass8 = correctionClass8Frequency * 8;
            var frequencyXClass9 = correctionClass9Frequency * 9;
            var frequencyXClass10 = correctionClass10Frequency * 10;

            var correctionFrequencyXClassSum = frequencyXClass1 + frequencyXClass2 + frequencyXClass3 + frequencyXClass4 + frequencyXClass5 + frequencyXClass6 + frequencyXClass7 + frequencyXClass8 + frequencyXClass9 + frequencyXClass10;

            return correctionFrequencyXClassSum / correctionFrequencySum;
        }

        public double CalculateTaintFrequency(TransData dadoTrans)
        {
            var dadoTransClass1Frequency = Convert.ToDouble((float)dadoTrans.Class1 / dadoTrans.PopulationNumber);
            var dadoTransClass2Frequency = Convert.ToDouble((float)dadoTrans.Class2 / dadoTrans.PopulationNumber);
            var dadoTransClass3Frequency = Convert.ToDouble((float)dadoTrans.Class3 / dadoTrans.PopulationNumber);
            var dadoTransClass4Frequency = Convert.ToDouble((float)dadoTrans.Class4 / dadoTrans.PopulationNumber);
            var dadoTransClass5Frequency = Convert.ToDouble((float)dadoTrans.Class5 / dadoTrans.PopulationNumber);
            var dadoTransClass6Frequency = Convert.ToDouble((float)dadoTrans.Class6 / dadoTrans.PopulationNumber);
            var dadoTransClass7Frequency = Convert.ToDouble((float)dadoTrans.Class7 / dadoTrans.PopulationNumber);
            var dadoTransClass8Frequency = Convert.ToDouble((float)dadoTrans.Class8 / dadoTrans.PopulationNumber);
            var dadoTransClass9Frequency = Convert.ToDouble((float)dadoTrans.Class9 / dadoTrans.PopulationNumber);
            var dadoTransClass10Frequency = Convert.ToDouble((float)dadoTrans.Class10 / dadoTrans.PopulationNumber);

            var dadoTransFrequencySum = dadoTransClass1Frequency + dadoTransClass2Frequency + dadoTransClass3Frequency + dadoTransClass4Frequency + dadoTransClass5Frequency + dadoTransClass6Frequency + dadoTransClass7Frequency + dadoTransClass8Frequency + dadoTransClass9Frequency + dadoTransClass10Frequency;

            var frequencyXClass1 = dadoTransClass1Frequency * 1;
            var frequencyXClass2 = dadoTransClass2Frequency * 2;
            var frequencyXClass3 = dadoTransClass3Frequency * 3;
            var frequencyXClass4 = dadoTransClass4Frequency * 4;
            var frequencyXClass5 = dadoTransClass5Frequency * 5;
            var frequencyXClass6 = dadoTransClass6Frequency * 6;
            var frequencyXClass7 = dadoTransClass7Frequency * 7;
            var frequencyXClass8 = dadoTransClass8Frequency * 8;
            var frequencyXClass9 = dadoTransClass9Frequency * 9;
            var frequencyXClass10 = dadoTransClass10Frequency * 10;

            var dadoTransSumFrequencyXClass = frequencyXClass1 + frequencyXClass2 + frequencyXClass3 + frequencyXClass4 + frequencyXClass5 + frequencyXClass6 + frequencyXClass7 + frequencyXClass8 + frequencyXClass9 + frequencyXClass10;

            return dadoTransSumFrequencyXClass / dadoTransFrequencySum;

        }

        public double CalculateCloneInductionFrequencyWithoutCorrection(TransData dadoTrans)
        {
            return Convert.ToDouble(dadoTrans.TaintTotal / ((float)dadoTrans.PopulationNumber * 48800) * 100000);
        }

        public double CalculateCloneInductionFrequencyWithCorrection(TransData dadoTrans)
        {
            var mediumSize = CalculateTaintFrequency(dadoTrans);
            var cloneInductionFrequencyWithoutCorrection = CalculateCloneInductionFrequencyWithoutCorrection(dadoTrans);
            return Convert.ToDouble(Math.Exp(mediumSize * Math.Log(2) / 4 * (float)cloneInductionFrequencyWithoutCorrection));
        }

        public MemoryStream GeneratePdfReport(TransData controle, Research research)
        {
            this.controle = controle;
            var composto = "";
            document = new Document(PageSize.A3.Rotate());
            document.SetMargins(3, 2, 3, 2);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            PdfPTable table = new PdfPTable(17);
            PdfPTable tableComplete = new PdfPTable(5);
            table.WidthPercentage = 90;
            tableComplete.WidthPercentage = 40;
            Font fontHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 18);
            Font fontText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);
            Font fontItalic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.ITALIC);

            Font small = new Font(Font.FontFamily.HELVETICA, 6);
            Chunk a = new Chunk("a", small);
            a.SetTextRise(7);
            Chunk b = new Chunk("b", small);
            b.SetTextRise(7);
            Chunk c = new Chunk("c", small);
            c.SetTextRise(7);
            Chunk d = new Chunk("d", small);
            d.SetTextRise(7);
            Chunk e = new Chunk("e", small);
            e.SetTextRise(7);
            Chunk f = new Chunk("f", small);
            f.SetTextRise(7);
            Chunk comma = new Chunk(",", small);
            comma.SetTextRise(7);

            Paragraph column1 = new Paragraph("(mM)", fontHeader);
            Paragraph column2 = new Paragraph("( N )", fontHeader);
            Paragraph column3 = new Paragraph("(1-2 céls)", fontHeader);
            column3.Add(b);
            Paragraph column4 = new Paragraph("(>2 céls)", fontHeader);
            column4.Add(b);
            Paragraph column5 = new Paragraph("mwh", fontHeader);
            column5.Add(c);
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
            headerExplanation.Add(a);
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

            var media = new Paragraph("Media das");
            var mediaCell = new PdfPCell(media);
            mediaCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            mediaCell.Colspan = 2;

            var classTam = new Paragraph("classes de tam.");
            var classTamCell = new PdfPCell(classTam);
            classTamCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            classTamCell.Colspan = 2;

            var clonesMwh = new Paragraph("clones mwh");
            clonesMwh.Add(c);
            clonesMwh.Add(comma);
            clonesMwh.Add(d);
            var clonesMwhCell = new PdfPCell(clonesMwh);
            clonesMwhCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clonesMwhCell.Colspan = 2;

            var i = new Paragraph("(î)");
            var iCell = new PdfPCell(i);
            iCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            iCell.Colspan = 2;


            table.AddCell("Genótipos");
            table.AddCell("N. de");
            table.AddCell(headerExplanationCell);
            table.AddCell("Total");
            table.AddCell(mediaCell);
            

            table.AddCell("e Conc.");
            table.AddCell("Indiv.");
            table.AddCell(mspCell);
            table.AddCell(msgCell);
            table.AddCell(mgCell);
            table.AddCell(tmCell);
            table.AddCell("manchas");
            table.AddCell(classTamCell);

            table.AddCell(headerCell1);
            table.AddCell(headerCell2);
            table.AddCell(headerCell3);
            table.AddCell(headerCell4);
            table.AddCell(headerCellempty);
            table.AddCell(headerCellempty);
            table.AddCell(headerCell5);
            table.AddCell(clonesMwhCell);

            table.AddCell(headerCellempty2);
            table.AddCell(headerCellempty2);
            table.AddCell(headerCell6);
            table.AddCell(headerCell7);
            table.AddCell(headerCell7);
            table.AddCell(headerCell6);
            table.AddCell(headerCell8);
            table.AddCell(iCell);

            var flairCell = new PdfPCell(new Paragraph("mwh/flr³", fontItalic));
            flairCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(flairCell);

            table.CompleteRow();

            var itemCompostoControleCell = new PdfPCell(new Paragraph(controle.Compound, fontText));
            itemCompostoControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemCompostoControleCell);

            var itemIndividuosControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.PopulationNumber), fontText));
            itemIndividuosControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemIndividuosControleCell);

            var itemMSPProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MSP) / Convert.ToDouble(controle.PopulationNumber), 2)), fontText));
            itemMSPProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSPProportionControleCell);

            var itemMSPNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MSP), fontText));
            itemMSPNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSPNumberControleCell);

            table.AddCell("");

            var itemMSGProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MSG) / Convert.ToDouble(controle.PopulationNumber), 2)), fontText));
            itemMSGProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSGProportionControleCell);

            var itemMSGNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MSG), fontText));
            itemMSGNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMSGNumberControleCell);

            table.AddCell("");

            var itemMGProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.MG) / Convert.ToDouble(controle.PopulationNumber), 2)), fontText));
            itemMGProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMGProportionControleCell);

            var itemMGNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.MG), fontText));
            itemMGNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemMGNumberControleCell);

            table.AddCell("");

            var itemTotalProportionControleCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(controle.TaintTotal) / Convert.ToDouble(controle.PopulationNumber), 2)), fontText));
            itemTotalProportionControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemTotalProportionControleCell);

            var itemTotalNumberControleCell = new PdfPCell(new Paragraph(Convert.ToString(controle.TaintTotal), fontText));
            itemTotalNumberControleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemTotalNumberControleCell);
            table.AddCell("");

            table.AddCell(itemTotalNumberControleCell);

            var itemTaintFrequencyControlCell = new PdfPCell(new Paragraph(CalculateTaintFrequency(controle).ToString("n2")));
            itemTaintFrequencyControlCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(itemTaintFrequencyControlCell);

            table.CompleteRow();

            foreach (var item in research.TransData)
            {
                var itemCompostoCell = new PdfPCell(new Paragraph(item.Compound, fontText));
                itemCompostoCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemCompostoCell);

                var itemIndividuosCell = new PdfPCell(new Paragraph(Convert.ToString(item.PopulationNumber), fontText));
                itemIndividuosCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemIndividuosCell);                

                var itemMSPProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MSP)/Convert.ToDouble(item.PopulationNumber), 2)), fontText));
                itemMSPProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPProportionCell);

                var itemMSPNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSP), fontText));
                itemMSPNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSPNumberCell);

                var itemDiagnosticoMSPCell = new PdfPCell(new Paragraph(CalcularMSPTrans(item)));
                itemDiagnosticoMSPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSPCell);                

                var itemMSGProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MSG) / Convert.ToDouble(item.PopulationNumber), 2)), fontText));
                itemMSGProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSGProportionCell);

                var itemMSGNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MSG), fontText));
                itemMSGNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMSGNumberCell);

                var itemDiagnosticoMSGCell = new PdfPCell(new Paragraph(CalcularMSGTrans(item)));
                itemDiagnosticoMSGCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMSGCell);

                var itemMGProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.MG) / Convert.ToDouble(item.PopulationNumber), 2)), fontText));
                itemMGProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMGProportionCell);

                var itemMGNumberCell = new PdfPCell(new Paragraph(Convert.ToString(item.MG), fontText));
                itemMGNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemMGNumberCell);

                var itemDiagnosticoMGCell = new PdfPCell(new Paragraph(CalcularMGTrans(item)));
                itemDiagnosticoMGCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoMGCell);

                var itemTotalProportionCell = new PdfPCell(new Paragraph(Convert.ToString(Math.Round(Convert.ToDouble(item.TaintTotal) / Convert.ToDouble(item.PopulationNumber), 2)), fontText));
                itemTotalProportionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTotalProportionCell);

                var itemTotalNumberCell = new PdfPCell(new Paragraph(item.TaintTotal.ToString(), fontText));
                itemTotalNumberCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTotalNumberCell);

                var itemDiagnosticoTotalCell = new PdfPCell(new Paragraph(CalcularTotalTrans(item)));
                itemDiagnosticoTotalCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemDiagnosticoTotalCell);

                var itemTaintTotalCell = new PdfPCell(new Paragraph(item.TaintTotal.ToString()));
                itemTaintTotalCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTaintTotalCell);

                var itemTaintFrequency = new PdfPCell(new Paragraph(CalculateTaintFrequency(item).ToString("n2")));
                itemTaintFrequency.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTaintFrequency);

                var itemTaintCorrectionFrequency = new PdfPCell(new Paragraph("{" + CalculateTaintFrequencyCorrection(item).ToString("n2") + "}"));
                itemTaintCorrectionFrequency.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(itemTaintCorrectionFrequency);

                table.CompleteRow();
            }

            //Tabela Frequencia

            var frequencyTitle = new Paragraph("Frequência de indução de manchas");
            var frequencyTitleCell = new PdfPCell(frequencyTitle);
            frequencyTitleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            frequencyTitleCell.Colspan = 4;

            var frequencyTitleObs = new Paragraph("(por 10⁵ células por divisão celular)");
            frequencyTitleObs.Add(f);
            var frequencyTitleObsCell = new PdfPCell(frequencyTitleObs);
            frequencyTitleObsCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            frequencyTitleObsCell.Colspan = 4;

            var noCorrectionTitle = new Paragraph("S/ correção por tam.");
            noCorrectionTitle.Add(d);
            noCorrectionTitle.Add(comma);
            noCorrectionTitle.Add(e);
            var noCorrectionTitleCell = new PdfPCell(noCorrectionTitle);
            noCorrectionTitleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            noCorrectionTitleCell.Colspan = 2;

            var withCorrectionTitle = new Paragraph("C/ correção por tam.");
            withCorrectionTitle.Add(d);
            withCorrectionTitle.Add(comma);
            withCorrectionTitle.Add(e);
            var withCorrectionTitleCell = new PdfPCell(withCorrectionTitle);
            withCorrectionTitleCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            withCorrectionTitleCell.Colspan = 2;

            tableComplete.AddCell("Genótipos");

            var nc = new Paragraph("n/NC");
            var ncCell = new PdfPCell(nc);
            ncCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            ncCell.Colspan = 2;

            var ncx2 = new Paragraph("(2ⁱ⁻²) X (n/NC)");
            var ncx2Cell = new PdfPCell(ncx2);
            ncx2Cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            ncx2Cell.Colspan = 2;

            tableComplete.AddCell(frequencyTitleCell);
            tableComplete.AddCell("");
            tableComplete.AddCell(frequencyTitleObsCell);
            tableComplete.AddCell("");
            tableComplete.AddCell(noCorrectionTitleCell);
            tableComplete.AddCell(withCorrectionTitleCell);
            tableComplete.AddCell("");
            tableComplete.AddCell(ncCell);
            tableComplete.AddCell(ncx2Cell);

            foreach (var item in research.TransData)
            {
                var itemCompostoCell = new PdfPCell(new Paragraph(item.Compound, fontText));
                itemCompostoCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableComplete.AddCell(itemCompostoCell);

                var itemFrequencyNoCorrectionCell = new PdfPCell(new Paragraph(CalculateCloneInductionFrequencyWithoutCorrection(item).ToString("n2")));
                itemFrequencyNoCorrectionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableComplete.AddCell(itemFrequencyNoCorrectionCell);

                var itemFrequencyCorrectionOnDataCell = new PdfPCell(new Paragraph("{" + CalculateTaintInductionFrequencyWithoutCorrectionBySizeAndCorrectionOnData(item).ToString("n2") + "}"));
                itemFrequencyCorrectionOnDataCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableComplete.AddCell(itemFrequencyCorrectionOnDataCell);

                var itemFrequencyWithCorrectionCell = new PdfPCell(new Paragraph(CalculateCloneInductionFrequencyWithCorrection(item).ToString("n2")));
                itemFrequencyWithCorrectionCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableComplete.AddCell(itemFrequencyWithCorrectionCell);

                var itemFrequencyWithCorrectionAndOnDataCell = new PdfPCell(new Paragraph("{" + CalculateTaintInductionFrequencyWithCorrectionBySizeAndCorrectionOnData(item).ToString("n2") + "}"));
                itemFrequencyWithCorrectionAndOnDataCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableComplete.AddCell(itemFrequencyWithCorrectionAndOnDataCell);

            }

            document.Add(new Paragraph("Frequência de manchas mutantes observadas nos descendentes trans-heterozigotos de Drosophila melanogaster, do cruzamento padrão, tratados com diferentes concentrações de " + research.Compound + ".\n\n"));

            document.Add(table);
            document.Add(Chunk.NEWLINE);
            document.Add(tableComplete);
            document.Add(Chunk.NEWLINE);
            
            Paragraph obsA = new Paragraph();
            obsA.Add(a);
            obsA.Add("Diagnóstico estatístico de acordo com Frei e Würgler (1988): +, positivo; -, negativo; i, inconclusivo. m, fator de multiplicação para a avaliação de resultados significativamente negativos. Níveis de significância α = β = 0,05.");
            document.Add(obsA);

            Paragraph obsB = new Paragraph();
            obsB.Add(b);
            obsB.Add("Incluindo manchas simples flr³ raras.");
            document.Add(obsB);

            Paragraph obsC = new Paragraph();
            obsC.Add(c);
            obsC.Add("Considerando os clones mwh para as manchas simples mwh e para as manchas gêmeas.");
            document.Add(obsC);

            Paragraph obsD = new Paragraph();
            obsD.Add(d);
            obsD.Add("Números entre chaves são as freqüências de indução corrigidas em relação a incidência espontânea estimada do controle negativo.");
            document.Add(obsD);

            Paragraph obsE = new Paragraph();
            obsE.Add(e);
            obsE.Add("C = 48.000, isto é, número aproximado de células examinadas por indivíduo.");
            document.Add(obsE);

            Paragraph obsF = new Paragraph();
            obsF.Add(f);
            obsF.Add("Calculado de acordo com Frei et al. (1992).");
            document.Add(obsF);
            document.Add(new Paragraph("*Apenas manchas simples mwh podem ser observadas nos indivíduos heterozigotos mwh/TM3, já que o cromossomo balanceador TM3 não contém o gene mutante flr³."));
            document.Close();
            return memoryStream;
        }
    }
}
