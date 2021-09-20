namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Заказы : IDbTable
    {
        [Key]
        [Column("Код заказа", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Код_заказа { get; set; }

        [Column("Код заявки", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_заявки { get; set; }

        [Column("Код менеджера", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_менеджера { get; set; }

        [Column("Код автомобиля", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Код_автомобиля { get; set; }

        public virtual Заявки Заявки { get; set; }

        public virtual Сотрудники Сотрудники { get; set; }
    }
}
