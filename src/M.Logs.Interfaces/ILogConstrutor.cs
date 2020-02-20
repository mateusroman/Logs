using System;
using System.Linq.Expressions;

namespace M.Logs.Interfaces
{
    public interface ILogConstrutor
    {
        /// <summary>
        /// Escreve uma mensagem simples no log.
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        ILogConstrutor Mensagem(string mensagem);

        /// <summary>
        /// Escreve a excecao gerada no log.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        ILogConstrutor Excecao(Exception ex);

        /// <summary>
        /// Permite escrever qualquer objeto no log, precedido de se nome. Ex: .Parametro(x => valor)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parametro"></param>
        /// <returns></returns>
        ILogConstrutor Parametro<T>(Expression<Func<bool, T>> parametro);

        /// <summary>
        /// Define a classe em que o log esta sendo executado. Pode ser definido apenas uma vez, no construtor da classe.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        ILogConstrutor Classe(string nome);

        /// <summary>
        /// Define o metodo em que o log esta sendo executado. Deve ser definido a cada log gravado.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        ILogConstrutor Metodo(string nome);

        string ObterTexto();

        /// <summary>
        /// Termina a construção do log, permitindo gravar a informação.
        /// </summary>
        /// <returns></returns>
        ILog Construir();
    }
}
