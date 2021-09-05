namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Услуги
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Услуги()
        {
            Заявки = new HashSet<Заявки>();
        }

        [Key]
        [Column("Код услуги")]
        public int Код_услуги { get; set; }

        [Column("Название услуги")]
        [Required]
        [StringLength(30)]
        public string Название_услуги { get; set; }

        [Column("Описание услуги")]
        [StringLength(100)]
        public string Описание_услуги { get; set; }

        [Column("Код тарифа")]
        public int Код_тарифа { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заявки> Заявки { get; set; }

        public virtual Тарифы Тарифы { get; set; }
    }
}
