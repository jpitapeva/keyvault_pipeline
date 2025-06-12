using InjecaoDependencia.Application.Interfaces;

namespace InjecaoDependencia.Application
{
    public class Transient : ITransient
    {
        private readonly Guid valor;
        public Transient()
        {
            valor = Guid.NewGuid();
        }

        public Guid ObterTransient()
        {
            return valor;
        }
    }
}
