class Comentarios
{
    //ORM => mapeamento objeto relacional
    //DDL => Data Definition Language => é um subconjunto da linguagem SQL que define os dados do modelo relacional (mudança na estrutura dos dados;
    //DML => Data Manipulation Language => subconjunto da linguagem SQL responsável pela manipulação de dados;

    //gerando SQL através do Entity
    //var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
    //var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
    //loggerFactory.AddProvider(SqlLoggerProvider.Create());

    //contexto herda de DbContext(classe base de toda a API do Entity) => possui o ChangeTracker => responsável por rastrear todas as mudanças que estão acontecendo numa determinada instância do contexto => possui uma lista(recuperada através do Entries) de todas as entidades que estão sendo gerenciadas num dado momento/contexto

    //Classe EntityEntry => retornada para cada um dos objetos disponíveis no banco de dados; possui uma propriedade essencial para o monitoramento de mudanças - a propriedade estado(registra o estado da entidade); caso haja uma alteração no estado, o método SaveChanges passa a ser requerido

    //ao acionar o SaveChanges, o Entity vai verificar qual estado teve alteração e acionar um comando SQL diferente de acordo com a alteração sofrida

    //o Entity vai incluir por conta própria o produto "Pão Francês" => ele percebe que há uma referência não nula a um produto; vai incluir o objeto paoFrances sob a supervisão do change tracker

    //o Entity e outras ORMs não recuperam as entidades relacionadas junto com SELECT que foi feito(comportamento padrão) => melhora a performance, já que impede que muitos objetos sejam recuperados de uma única vez



}

