namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Сотрудники : IDbTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Сотрудники()
        {
            Автомобили = new HashSet<Автомобили>();
            Заказы = new HashSet<Заказы>();
        }

        [Key]
        [Column("Код сотрудника")]
        public int Код_сотрудника { get; set; }

        [Required]
        [StringLength(50)]
        public string ФИО { get; set; }

        [Column("Номер телефона")]
        [Required]
        [StringLength(9)]
        public string Номер_телефона { get; set; }

        [Column("Дата рождения", TypeName = "date")]
        public DateTime Дата_рождения { get; set; }

        [Column("Код должности")]
        public int Код_должности { get; set; }

        [Column("Адрес проживания")]
        [Required]
        [StringLength(100)]
        public string Адрес_проживания { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Автомобили> Автомобили { get; set; }

        public virtual Должности Должности { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}
