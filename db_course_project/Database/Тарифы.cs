namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Тарифы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Тарифы()
        {
            Услуги = new HashSet<Услуги>();
        }

        [Key]
        [Column("Код тарифа")]
        public int Код_тарифа { get; set; }

        [Column("Название тарифа")]
        [Required]
        [StringLength(30)]
        public string Название_тарифа { get; set; }

        [Column("Стоимость тарифа")]
        public decimal Стоимость_тарифа { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Услуги> Услуги { get; set; }
    }
}
