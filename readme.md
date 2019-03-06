# Projeto com DOCKER  e Testes Automatizados
Este projeto é um exemplo utilizando um banco de dados com o padrão domain notification. Para executar, deixei abaixo alguns comandos úteis.

Para facilitar o exemplo, execute o comando ``docker run --rm -it --net=host  appdapper``, será exibido o modo interativo para digitar **nome** e **sobre nome** do cliente.

Para visualizar a notificação de validação, basta deixar o **nome** ou **sobre nome** vazio.

>### Builda a imagem a partir do docker file
```docker
docker build . -t appdapper
`````
>### Cria e remove o container no modo interativo e adiciona um host ao container:
```docker
docker run --rm -it --net=host  appdapper
```

>### Lista os containers que estão rodando:
```docker
docker ps
```

>### Lista todos os containers criados:
```docker
docker ps -a
```

>### Incia um **containr já criado** no modo interativo:
```docker
docker start id_container -i -a
```

>### Remove um container parado:
```docker
docker rm id_container
```

>### Remove um container mesmo estando em execução:
```docker
docker rm id_container -f
```

>### Lista todas images criadas:
```docker
docker images
```

>### Remove uma imagem sem um container vinculado:
```docker
docker rmi id_image
```

>### Remove uma imagem com um container vinculado e em execução:
```docker
docker rmi id_image -f
```

>## Banco de Dados
Para esse projeto, será precisa criar um banco de dados dentro do docker.

[SQL Server Docker](https://docs.microsoft.com/pt-br/sql/linux/sql-server-linux-configure-docker?view=sql-server-2017)

* Logo abaixo segue alguns comandos. As configuraçõe de connnection string devem ser alterdas para a senha que for definida nos camando abaixo, bem como a porta que desejar configurar.

```
docker pull microsoft/mssql-server-linux:2017-latest
```
```
docker run --name SQLServer2017 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD={SENHA}" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p {PORTA EXTERNA}:{PORTA INTERNA} -d microsoft/mssql-server-linux:2017-latest
```

* Nas configurações da connectionstirng, basta substituir as variáveis **{PORTA}**, **{USUARIO}** e **{SENHA}**.
* Dexei também a connection dos testes de integração, ela segue o mesmo exemplo da connection da camada InfraData