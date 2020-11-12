# Observe API

API de controle de usuários e receituários do aplicativo Observe.

## API:

A API recebe e retorna dados no formato de texto JSON e aceita os métodos GET, POST, PUT e DELETE.

Os endereços das requisições devem constar em sua URI o prefixo `/api` seguido da rota do controlador, tipo do parâmetro e o parâmetro de consulta.

ex.: http://localhost/api/usuarios - para listar a tabela de usuários;

ou http://localhost/api/usuarios/id/5 - para buscar um único usuário pelo seu `id`.

A construção da API foi realizada em maior por parte utilizando a estratégia de migração de `código para banco de dados` do EF Core, e então "documentada"
utilizando os pacotes Swagger e outras ferramentas diposníveis no pacote Swashbuckle.

### Controladores

1. Usuarios:

| Método | Rota                                | Descrição                                                                  |
| ------ | ----------------------------------- | -------------------------------------------------------------------------: |
| GET    | ...api/usuarios                     | Todos os usuários                                                          |
| GET    | ...api/usuarios/id/5                | Usuário com id 5                                                           |
| GET    | ...api/usuarios/cid/xyz5            | Usuário com cid xyz5                                                       |
| GET    | ...api/usuarios/nome/Usuário        | Usuários com nome, ou sobrenome parecidos com "Usuário"                    |
| POST   | ...api/usuarios/                    | Inserir novo usuário                                                       |
| PUT    | ...api/usuarios/id/5                | Atualizar usuário com id 5                                                 |
| DELETE | ...api/usuarios/id/5                | Deletar usuário com id 5                                                   |

2. Medicos:

| Método | Rota                                | Descrição                                                                  |
| ------ | ----------------------------------- | -------------------------------------------------------------------------: |
| GET    | ...api/medicos                      | Todos os médicos                                                           |
| GET    | ...api/medicos/id/5                 | Médico com id 5                                                            |
| GET    | ...api/medicos/uid/5                | Médico com id de usuário 5                                                 |
| GET    | ...api/medicos/cid/xyz5             | Médico com cid de usuário xyz5                                             |
| GET    | ...api/medicos/nome/Usuário         | Médicos com nome, ou sobrenome de usuário parecidos com "Usuário"          |
| GET    | ...api/medicos/id/5/receitas        | Receitas do médico com id 5                                                |
| GET    | ...api/medicos/id/5/receitas/id/5   | Receita de id 5 do médico com id 5                                         |
| POST   | ...api/medicos/                     | Inserir novo médico                                                        |
| PUT    | ...api/medicos/id/5                 | Atualizar médico com id 5                                                  |
| DELETE | ...api/medicos/id/5                 | Deletar médico com id 5                                                    |

3. Pacientes:

| Método | Rota                                | Descrição                                                                  |
| ------ | ----------------------------------- | -------------------------------------------------------------------------: |
| GET    | ...api/pacientes                    | Todos os pacientes                                                         |
| GET    | ...api/pacientes/id/5               | Paciente com id 5                                                          |
| GET    | ...api/pacientes/uid/5              | Paciente com id de usuário 5                                               |
| GET    | ...api/pacientes/cid/xyz5           | Paciente com cid de usuário xyz5                                           |
| GET    | ...api/pacientes/nome/Usuário       | Pacientes com nome, ou sobrenome de usuário parecidos com "Usuário"        |
| GET    | ...api/pacientes/id/5/receitas      | Receitas do Paciente com id 5                                              |
| GET    | ...api/pacientes/id/5/receitas/id/5 | Receita de id 5 do paciente com id 5                                       |
| POST   | ...api/pacientes/                   | Inserir novo paciente                                                      |
| PUT    | ...api/pacientes/id/5               | Atualizar paciente com id 5                                                |
| DELETE | ...api/pacientes/id/5               | Deletar paciente com id 5                                                  |

4. Receitas:

| Método | Rota                                | Descrição                                                                  |
| ------ | ----------------------------------- | -------------------------------------------------------------------------: |
| GET    | ...api/receitas                     | Todos as receitas                                                          |
| GET    | ...api/receitas/id/5                | Receita com id 5                                                           |
| GET    | ...api/receitas/id/5/detalhes       | Receita com id 5 com detalhes do médico e do paciente                      |
| POST   | ...api/receitas/                    | Inserir nova receita                                                       |
| PUT    | ...api/receitas/id/5                | Atualizar receita com id 5                                                 |
| DELETE | ...api/receitas/id/5                | Deletar receita com id 5                                                   |


### Modelos

1. Usuario

| Campo      | Tipo          | Descrição                                                                                    |
| ---------- | ------------- | -------------------------------------------------------------------------------------------: |
| id         | int           | Código de identificação do usuário na tabela                                                 |
| cid        | string        | Código de identificação do usuário na coleção do FireBase                                    |
| nome       | string        | Nome do usuário                                                                              |
| sobrenome  | string        | Sobrenome do usuário                                                                         |

2. Medico

| Campo      | Tipo          | Descrição                                                                                    |
| ---------- | ------------- | -------------------------------------------------------------------------------------------: |
| id         | int           | Código de identificação do médico na tabela                                                  |
| uid        | int           | Chave estrangeira referente ao código de identificação do usuário                            |
| crm        | string        | Código único do Conselho Regional de Medicina                                                |

3. Paciente

| Campo      | Tipo          | Descrição                                                                                    |
| ---------- | ------------- | -------------------------------------------------------------------------------------------: |
| id         | int           | Código de identificação do paciente na tabela                                                |
| uid        | int           | Chave estrangeira referente ao código de identificação do usuário                            |
| nascimento | DateTime      | Data de nascimento do paciente                                                               |
| doencas    | List<string>  | Lista de doenças crônicas do paciente                                                        |
| alergias   | List<string>  | Lista de alergias do paciente                                                                |
| remedios   | List<string>  | Lista de remédios que o paciente já toma                                                     |

4. Receita

| Campo      | Tipo          | Descrição                                                                                    |
| ---------- | ------------- | -------------------------------------------------------------------------------------------: |
| id         | int           | Código de identificação da receita na tabela                                                 |
| mid        | int           | Chave estrangeira referente ao código de identificação do médico                             |
| pid        | int           | Chave estrangeira referente ao código de identificação do paciente                           |
| remedios   | List<Remedio> | Lista de remédios e suas quantias, medidas e horários quais o paciente deverá tomar          |

5. Remedio

| Campo      | Tipo          | Descrição                                                                                    |
| ---------- | ------------- | -------------------------------------------------------------------------------------------: |
| nome       | string        | Nome do remédio                                                                              |
| medida     | int           | Tipo de medida do remédio, ex.: "ml" ou "unidade"                                            |
| quantia    | double        | Quantia em termos da medida definida, ex.: 10.0 (ml)                                         |
| horario    | string        | Hora sugerida em que o paciente deve tomar o remédio                                         |


## Ferramentas:

* ASP.NET Core 3.1
* Entity Framework Core 3.1
* SQL Server 2019


## Ambiente:

* Visual Studio 2019