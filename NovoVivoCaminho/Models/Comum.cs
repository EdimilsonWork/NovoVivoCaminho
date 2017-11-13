using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NovoVivoCaminho.Models
{
    public class Comum
    {
        public List<string> TiposDeEnderecos
        {
            get
            {
                string[] TiposDeEnderecos = new string[] {string.Empty, "AEROPORTO", "ALAMEDA", "AREA", "AVENIDA", "CAMPO", "CHACARA", "COLONIA", "CONDOMINIO", "CONJUNTO", "DISTRITO", "ESPLANADA", "ESTACAO", "ESTRADA", "FAVELA", "FEIRA", "JARDIM", "LADEIRA", "LAGO", "LAGOA", "LARGO", "LOTEAMENTO", "MORRO", "NUCLEO", "PARQUE", "PASSARELA", "PATIO", "PRACA", "QUADRA", "RECANTO", "RESIDENCIAL", "RODOVIA", "RUA", "SETOR", "SITIO", "TRAVESSA", "TRECHO", "TREVO", "VALE", "VEREDA", "VIA", "VIADUTO", "VIELA", "VILA" };
                return TiposDeEnderecos.ToList();
            }
        }
    }
}