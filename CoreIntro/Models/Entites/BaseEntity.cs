using System;

namespace CoreIntro.Models.Entites
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
