namespace db_course_project.Database.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ПредставлениеЗаявки
    {
        [Key]
        [Column("Код заявки", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_заявки { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ФИО { get; set; }

        [Key]
        [Column("Название услуги", Order = 2)]
        [StringLength(30)]
        public string Название_услуги { get; set; }

        [Key]
        [Column("Адрес доставки", Order = 3)]
        [StringLength(50)]
        public string Адрес_доставки { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal Расстояние { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal Масса { get; set; }

        [Key]
        [Column("Дата создания заявки", Order = 6)]
        public DateTime Дата_создания_заявки { get; set; }

        [Key]
        [Column("Дата перевозки", Order = 7, TypeName = "date")]
        public DateTime Дата_перевозки { get; set; }
    }
}
