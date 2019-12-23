# Logs
Permite a gravação de logs pelo aplicativo

###Exemplo:

```csharp
public static void Main()
{
  var exemplo = new Exemplo(LogFabrica.Criar());
}

public class Exemplo
{
  public Exemplo(ILogConstrutor log)
  {
    log.Classe(nameof(Exemplo));
  }
  
  public void Executar(int numero){
    log.Metodo(nameof(Executar)).Parametro(x => numero).Construir().ParaDebug();
    try
    {
      //Faz qualquer coisa
      log.Metodo(nameof(Executar)).Mensagem("Terminou").Construir().ParaDebug();
    }
    catch(Exception e)
    {
      log.Metodo(nameof(Executar)).Excecao(e).Construir().ParaErro();
    }
  }
}
```


###Configuração
