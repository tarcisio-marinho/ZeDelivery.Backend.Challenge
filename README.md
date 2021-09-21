# Ze Delivery Backend Challenge

Repositório referente ao desafio de backend da Zé Delivery. O desafio pode ser encontrado aqui: https://github.com/ZXVentures/ze-code-challenges/blob/master/backend.md



# Decisões arquiteturais

  Para o pilar de *Performance*, as buscas necessitam ser rápidas, é fundamental realizar tais buscas no banco e utilizando o [Redis](https://redis.io) como ferramenta de Caching para não bater no banco toda hora, utilizando [MsgPack](https://msgpack.org) para serializar os dados de forma mais otimizada.

  Para o pilar *Testability*, decidi testar o código de forma unitária utilizando [BDD](https://pt.wikipedia.org/wiki/Behavior_Driven_Development) e a biblioteca [SpecFlow](https://specflow.org), para diminuir a quantidade de código de teste, aumentando assim a reusabilidade, facilitando na leitura e escrita de novos testes.

  Para o pilar *Maintainability* e *Separation of concerns* decidi estruturar a arquitetura do código seguindo o padrão arquitetural [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) seguindo bem o desenho abaixo: 
  ![Alt text](images/cleanarch.jpg "Title")
  Facilitando na reutilização e extensão de componentes, e também o desacoplamento de casos de uso.
  O projeto ficou separado entre as camadas de `Api`, `Application`, `Domain` e `Infrastructure`. Cada camada tendo sua responsabilidade, evitando que tecnologias impactem no core do negócio (domínio).

# Banco de dados

Como o challenge é um problema de geolocalização, seguindo o padrão GeoJson, procurei uma lista de bancos que lidam com esse tipo de dados [Spatial database](https://en.wikipedia.org/wiki/Spatial_database) 

"A spatial database is a database optimized for storing and querying data that represents objects defined in a geometric space." - [Wikipedia](https://en.wikipedia.org/wiki/Spatial_database)

Contendo a lista de bancos: 

![Alt text](images/databases.png "Title")

Decidi então utilizar o MySql, pois ele já contém suporte nativo para o tipo de dados `geometry` seguindo o padrão [`OpenGIS`](https://web.archive.org/web/20130430004440/http://dev.mysql.com/doc/refman/5.5/en/gis-introduction.html)

Escolhi utilizar esse esquema de dados, utilizando a seguinte estrutura e os tipos [`Polygon` e `Point`](https://dev.mysql.com/doc/refman/5.7/en/gis-polygon-property-functions.html):

![Alt text](images/emr.png "Title")

Representando em [DDL](scripts/01_CREATE_DATABASE_SCRIPT.sql) para o MySql, a tabela ficou da seguinte forma:
```
CREATE TABLE `partner` (
  `Id` varchar(255) PRIMARY KEY,
  `TradingName` varchar(255),
  `OwnerName` varchar(255),
  `Document` varchar(18) UNIQUE,
  `CoverageAreaType` varchar(255),
  `CoverageAreaCoordinates` POLYGON,
  `AddressType` varchar(255),
  `AddressCoordinates` POINT
);
```

Para realiza as queries de forma mais otimizada, decidi utilizar o framework [Dapper](https://dapper-tutorial.net).

E para realizar as buscas geométricas, utilizei as funções: [`st_contains` e `st_distance_sphere`](https://dev.mysql.com/doc/refman/5.6/en/spatial-relation-functions-object-shapes.html)

# Caching

  Decidi utilizar o Redis como caching, para evitar idas repetidas no banco de dados. Utilizei nos casos de uso: `SearchNearestPartner` e `FindPartner`.
  Para facilitar a utilização do caching, construi o serviço genérico que armazena qualquer objeto:

![Alt text](images/cacheservice.png "Title")

Para a serialização de dados, decidi utilizar o [MsgPack](https://msgpack.org)


[Para comparação com outros serializadores](https://medium.com/@maximn/serialization-performance-comparison-xml-binary-json-p-ad737545d227):
![Alt text](images/msgpack.png "Title")

# Cross Plataform

Para cumprir com o requisito de ser cross plataform, decidi utilizar o Docker e DockerCompose para subir a aplicação e os serviços necessários (Caching e Banco de dados). Bastando apenas executar o comando em ambientes Windows/Linux:
```
docker-compose up -d
```

## Lista de padrões de projeto utilizados:
 - Dependency Injection
 - Fluent Builder ([Fluent Validation](https://fluentvalidation.net))
 - Notification
 - Generics


 # Problemas encontrados