using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace M.Logs.Extensoes
{
    public static class EspressaoExtensao
    {
        public static object ObterValorUnico(this Expression parametro) {
            Expression outerMember = ProcessarExpressao(parametro);
            var valor = Expression.Lambda(outerMember).Compile().DynamicInvoke();
            if (outerMember.Type.GetTypeInfo().IsPrimitive)
                return valor;
            else if (outerMember.Type == typeof(decimal))
                return valor;
            else if (outerMember.Type == typeof(DateTime))
                return valor;
            else if (outerMember.Type.GetTypeInfo().IsClass)
                return JsonConvert.SerializeObject(valor, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            else if (outerMember.Type.GetTypeInfo().IsInterface)
                return JsonConvert.SerializeObject(valor, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            else if (outerMember.Type.GetTypeInfo().IsEnum)
                return valor;
            else
                return null;
        }

        public static string ObterNomeUnico(this Expression parametro)
        {
            return ObterNome(parametro);
        }

        private static Expression ProcessarExpressao(Expression expressao) {
            if (expressao is LambdaExpression)
                return ProcessarExpressao((expressao as LambdaExpression).Body);
            else if (expressao is MemberExpression)
                return (MemberExpression)expressao;
            else if (expressao is MethodCallExpression)
                return expressao;
            else if (expressao is ConstantExpression)
                return expressao;
            else
                throw new Exception("Expressão não reconhecida");
        }

        private static string ObterNome(Expression expression) {
            Expression container = ProcessarExpressao(expression);
            if (container is MemberExpression)
                return (container as MemberExpression).Member.Name;
            else if (container is MethodCallExpression)
                return (container as MethodCallExpression).Method.Name;
            else if (container is ConstantExpression)
                return container.Type.ToString();
            else
                throw new Exception("Expressão não reconhecida");
        }
    }
}
