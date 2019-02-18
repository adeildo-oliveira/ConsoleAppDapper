using System;

namespace ConsoleApp.Domain.ModelEntity
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
