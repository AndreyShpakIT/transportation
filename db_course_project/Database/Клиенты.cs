namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Клиенты : IDbTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиенты()
        {
            Заявки = new HashSet<Заявки>();
        }

        [Key]
        [Column("Код клиента")]
        public int Код_клиента { get; set; }

        [Required]
        [StringLength(50)]
        public string ФИО { get; set; }

        [Column("Номер телефона")]
        [Required]
        [StringLength(50)]
        public string Номер_телефона { get; set; }

        [Column("Адрес проживания")]
        [Required]
        [StringLength(100)]
        public string Адрес_проживания { get; set; }

        [Column("Дата рождения", TypeName = "date")]
        public DateTime Дата_рождения { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заявки> Заявки { get; set; }
    }
}
