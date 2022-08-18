using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frekisa.Models
{
    public class analiseCredito
    {
        double ScoreSerasa()
        {
            Random score = new Random();

            return Math.Round(score.NextDouble(), 3);
        }


        public double ScoreFrekisa(double renda)
        {
            double scoreFrekisa = 0;
            scoreFrekisa = Math.Round(Math.Log(renda, 10) + Math.Pow(10, ScoreSerasa()));

            if (renda < 1212)
            {
                //Crédito não aprovado
                return 0;

            }
            else if (renda >= 1212 && renda < 100000)
            {
                if (scoreFrekisa >= 4 && scoreFrekisa < 8)
                {
                    // Apresenta o Plano de Crédito 1
                    return 1;
                }
                else if (scoreFrekisa >= 8 && scoreFrekisa < 12)
                {
                    // Apresenta o Plano de Crédito 2
                    return 2;
                }
                else
                {
                    // Apresenta o Plano de Crédito 3
                    return 3;
                }
            }
            else
            {
                // Renda >= 100.000 --> Plano 3
                return 3;
            }
        }
    }
}