namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Заявки : IDbTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Заявки()
        {
            Заказы = new HashSet<Заказы>();
        }

        [Key]
        [Column("Код заявки")]
        public int Код_заявки { get; set; }

        [Column("Код клиента")]
        public int Код_клиента { get; set; }

        [Column("Код услуги")]
        public int Код_услуги { get; set; }

        [Column("Дата создания заявки", TypeName = "date")]
        public DateTime Дата_создания_заявки { get; set; }

        [Column("Дата перевозки", TypeName = "date")]
        public DateTime Дата_перевозки { get; set; }


        [Column("Адрес доставки")]
        [Required]
        [StringLength(100)]
        public string Адрес_доставки { get; set; }

        [Column("Км")]
        [Required]
        public int Км { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }

        public virtual Клиенты Клиенты { get; set; }

        public virtual Услуги Услуги { get; set; }
    }
}
