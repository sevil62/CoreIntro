namespace CoreIntro.Models.Entites
{
    public class EmployeeProfile:BaseEntity
    {
        public string SpecialDetail { get; set; }

        //Relation Properties
        public virtual Employee Employee { get; set; }

    }
}
