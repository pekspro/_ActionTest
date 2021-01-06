using System;

namespace Sample.Services
{
    public class MyDatabaseContext
    {
        public Guid  Id { get; set; }

        public MyDatabaseContext()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

}