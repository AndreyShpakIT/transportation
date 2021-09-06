namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Тип автомобиля")]
    public partial class Тип_автомобиля : IDbTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Тип_автомобиля()
        {
            Автомобили = new HashSet<Автомобили>();
        }

        [Key]
        [Column("Код типа")]
        public int Код_типа { get; set; }

        [Column("Название типа")]
        [Required]
        [StringLength(50)]
        public string Название_типа { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Автомобили> Автомобили { get; set; }
    }
}
