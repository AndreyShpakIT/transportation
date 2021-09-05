namespace db_course_project.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Должности : IDbTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Должности()
        {
            Сотрудники = new HashSet<Сотрудники>();
        }

        [Key]
        [Column("Код должности")]
        public int Код_должности { get; set; }

        [Column("Название должности")]
        [StringLength(10)]
        public string Название_должности { get; set; }
        
        [ScaffoldColumn(false)]
        public virtual ICollection<Сотрудники> Сотрудники { get; set; }
    }
}
