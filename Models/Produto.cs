using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Armazenamento.Models
{
    public class Produto
    {
        [Key]
        public int Id {  get; set; }
        public int Codigo { get; set; }

        public string Descricao{ get; set; }

        public int Estoque { get; set; }

        public decimal Preco { get; set; }

    }
}
