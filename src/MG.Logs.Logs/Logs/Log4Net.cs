using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using MG.Logs.Interfaces;
using MG.Logs.Extensoes;

namespace MG.Log.Logs
{
    public class Log4Net : ILog, ILogConstrutor
    {        
        private log4net.ILog log;
        private List<Expression> parametros;
        private List<string> mensagens;
        private List<Exception> exceptions;
        private string classe;
        private string metodo;

        public Log4Net() {            
            log = log4net.LogManager.GetLogger("", "Bob");
            parametros = new List<Expression>();
            mensagens = new List<string>();
            exceptions = new List<Exception>();
        }

        public ILogConstrutor Excecao(Exception ex)
        {
            exceptions.Add(ex);
            return this;
        }

        public ILogConstrutor Mensagem(string mensagem)
        {
            mensagens.Add(mensagem);
            return this;
        }

        public ILogConstrutor Parametro<T>(Expression<Func<bool, T>> parametro)
        {
            parametros.Add(parametro);
            return this;           
        }

        public ILogConstrutor Classe(string nome)
        {
            classe = nome;
            return this;
        }

        public ILogConstrutor Metodo(string nome)
        {
            metodo = nome;
            return this;
        }

        public void ParaAlerta()
        {
            try
            {
                if (!log.IsWarnEnabled)
                    return;

                log.Warn(MontarMensagem());
            }
            finally {
                LimparValores();
            }
        }

        public void ParaDebug()
        {
            try
            {
                if (!log.IsDebugEnabled)
                    return;

                log.Debug(MontarMensagem());
            }
            finally {
                LimparValores();
            }
        }        

        public void ParaErro()
        {
            try
            {
                if (!log.IsErrorEnabled)
                    return;

                log.Error(MontarMensagem());
            }
            finally
            {
                LimparValores();
            }
        }

        public void ParaFatal()
        {
            try
            {
                if (!log.IsFatalEnabled)
                    return;

                log.Fatal(MontarMensagem());
            }
            finally
            {
                LimparValores();
            }
        }

        public void ParaInformacao()
        {
            try
            {
                if (!log.IsInfoEnabled)
                    return;

                log.Info(MontarMensagem());
            }
            finally
            {
                LimparValores();
            }
        }

        public string ObterTexto() {
            return MontarMensagem();
        }

        public ILog Construir()
        {
            return this;
        }

        private string MontarMensagem() {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append(ProcessarClasseMetodo());

                foreach (var mensagem in mensagens)
                    stringBuilder.Append(mensagem).Append(mensagem == mensagens[mensagens.Count - 1] ? "" : " ");
                foreach (var parametro in parametros)
                {
                    stringBuilder
                        .Append(" ")
                        .Append(parametro.ObterNomeUnico())
                        .Append(": ")
                        .Append(parametro.ObterValorUnico());
                }

                foreach (var exception in exceptions)
                    stringBuilder.Append(ProcessarMensagemExcecao(exception));
                return stringBuilder.ToString();
            }
            catch
            {
                throw;
            }            
        }

        private string ProcessarClasseMetodo() {
            var retorno = new StringBuilder();
            if (!string.IsNullOrEmpty(classe))
            {
                retorno.Append(classe);

                if (!string.IsNullOrEmpty(metodo))
                    retorno.Append(".").Append(metodo);
                
                retorno.Append(" - ");
            }
            else if (!string.IsNullOrEmpty(metodo))
                retorno.Append(metodo).Append(" - ");

            return retorno.ToString();
        }

        private string ProcessarMensagemExcecao(Exception ex) {
            if (ex == null)
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append("Exceção: ").Append(ex.Message);
            MontarMensagemInnerException(ex, sb);
            if(ex.StackTrace != null)
                sb.AppendLine("StackTrace: ").Append(ex.StackTrace);
            return sb.ToString();
        }

        private void MontarMensagemInnerException(Exception ex, StringBuilder sb) {
            if (ex.InnerException == null)
                return;

            sb.AppendLine("InnerException: ").Append(ex.InnerException.Message);
            MontarMensagemInnerException(ex.InnerException, sb);
        }

        private void LimparValores() {
            parametros.Clear();
            exceptions.Clear();
            mensagens.Clear();
            metodo = string.Empty;
        }        
    }
}
