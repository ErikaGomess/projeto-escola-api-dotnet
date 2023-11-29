using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEscola_API.Models{
    public class Produto{

        public int id {get; set;}
        public string? nome{get; set;}

        public string? imagem{get;set;}

        public float valor{get;set;}
        public string? descricao{get;set;}

        public int quant{get;set;}
        
        
    }
}

/*
 id      INT AUTO_INCREMENT NOT NULL,
    nome    VARCHAR (20) NULL,
    imagem  VARCHAR (20) NULL,
    valor   REAL NULL,
    descricao VARCHAR (30) NULL,
    quant	INT NULL,
*/