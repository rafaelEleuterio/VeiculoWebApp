using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeiculoModelsLibrary.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Aqui encontrei um probleminha que me causou confusão. A placa é um identificador ou ela é editável? Segui com a linha
        /// de raciocínio de que ela é editável.
        /// </summary>
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Montadora { get; set; } = string.Empty;
    }
}
