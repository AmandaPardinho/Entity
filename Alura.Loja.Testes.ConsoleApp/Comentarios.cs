class Comentarios
{
    //gerando SQL através do Entity
    //var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
    //var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
    //loggerFactory.AddProvider(SqlLoggerProvider.Create());

    //contexto herda de DbContext(classe base de toda a API do Entity) => possui o ChangeTracker => responsável por rastrear todas as mudanças que estão acontecendo numa determinada instância do contexto => possui uma lista(recuperada através do Entries) de todas as entidades que estão sendo gerenciadas num dado momento/contexto

    //Classe EntityEntry => retornada para cada um dos objetos disponíveis no banco de dados; possui uma propriedade essencial para o monitoramento de mudanças - a propriedade estado(registra o estado da entidade); caso haja uma alteração no estado, o método SaveChanges passa a ser requerido

    //ao acionar o SaveChanges, o Entity vai verificar qual estado teve alteração e acionar um comando SQL diferente de acordo com a alteração sofrida




}

