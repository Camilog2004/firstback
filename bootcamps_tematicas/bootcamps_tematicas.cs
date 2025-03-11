using System.ComponentModel.DataAnnotations.Schema;
using firstback.bootcamps;
using firstback.Tematicas;

namespace firstback.BootcampsTematicas
{
    public class BootcampTematica
    {
        [ForeignKey("Bootcamp")]
        public int IdBootcamp { get; set; }

        [ForeignKey("Tematica")]
        public int IdTematica { get; set; }

        public Bootcamp Bootcamp { get; set; } = null!;
        public Tematica Tematica { get; set; } = null!;
    }
}