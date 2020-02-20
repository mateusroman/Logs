
namespace M.Logs.Interfaces
{
    public interface ILog
    {        
        void ParaDebug();        
        void ParaInformacao();
        void ParaAlerta();
        void ParaErro();
        void ParaFatal();        
    }
}
