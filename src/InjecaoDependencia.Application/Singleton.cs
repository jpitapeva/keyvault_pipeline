using InjecaoDependencia.Application.Interfaces;

namespace InjecaoDependencia.Application
{
    public class Singleton : ISingleton
    {
        private readonly Guid valor;
        public Singleton()
        {
            valor = Guid.NewGuid();
        }

        public Guid ObterSingleton()
        {
            return valor;
        }
    }
}
