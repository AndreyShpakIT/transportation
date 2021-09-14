namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Автомобили : IDbTable
    {
        [Key]
        [StringLength(9)]
        [Column("Номер автомобиля")]
        public string Код_автомобиля { get; set; }

        [Column("Тип автомобиля")]
        public int Тип_автомобиля { get; set; }

        [Required]
        [StringLength(30)]
        public string Марка { get; set; }

        [Column("Код водителя")]
        public int? Код_водителя { get; set; }

        [Column("Год выпуска")]
        public int Год_выпуска { get; set; }

        public virtual Сотрудники Сотрудники { get; set; }

        public virtual Тип_автомобиля Тип_автомобиля1 { get; set; }
    }
}
