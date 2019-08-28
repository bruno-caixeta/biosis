using Biosis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Biosis.BusinessLayer.Implementation
{
    public class CalculosTrans
    {
        DadosTrans controle = new DadosTrans();        
        public CalculosTrans(DadosTrans dadosTransControle)
        {
            controle = dadosTransControle;
        }
        public string CalcularMSPTrans(DadosTrans dadoTrans)
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

        public string CalcularMSGTrans(DadosTrans dadoTrans)
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

        public string CalcularMGTrans(DadosTrans dadoTrans)
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

        public string CalcularTotalTrans(DadosTrans dadoTrans)
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
    }
}
